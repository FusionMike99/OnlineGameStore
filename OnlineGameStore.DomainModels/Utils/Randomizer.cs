using System;

namespace OnlineGameStore.DomainModels.Utils
{
    public static class Randomizer
    {
        public static byte[] GetRandomByteArray(int length)
        {
            var rnd = new Random();
            var bytes = new byte[length];
            rnd.NextBytes(bytes);
            
            return bytes;
        }
    }
}