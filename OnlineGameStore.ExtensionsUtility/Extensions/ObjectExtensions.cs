using Newtonsoft.Json;

namespace OnlineGameStore.ExtensionsUtility.Extensions
{
    public static class ObjectExtensions
    {
        public static T DeepClone<T>(this T source)
        {
            if (ReferenceEquals(source, null))
            {
                return default;
            }

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            var serializedObject = JsonConvert.SerializeObject(source, jsonSerializerSettings);

            return JsonConvert.DeserializeObject<T>(serializedObject, jsonSerializerSettings);
        }
    }
}