using System;

namespace OnlineGameStore.BLL.Utils
{
    public static class GuidExtensions
    {
        public static int ToInt(this Guid value)
        {
            var bytes = value.ToByteArray();
            var convertResult = BitConverter.ToInt32(bytes, 0);
            
            return convertResult;
        }
    }
}