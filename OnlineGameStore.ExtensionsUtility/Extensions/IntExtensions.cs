using System;
using OnlineGameStore.ExtensionsUtility.Utils;

namespace OnlineGameStore.ExtensionsUtility.Extensions
{
    public static class IntExtensions
    {
        public static Guid ToGuid(this int value)
        {
            const int guidSize = 16;
            
            var bytes = Randomizer.GetRandomByteArray(guidSize);
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            var guid = new Guid(bytes);
            
            return guid;
        }
    }
}