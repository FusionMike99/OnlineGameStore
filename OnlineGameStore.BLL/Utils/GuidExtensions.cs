using System;
using System.Linq;
using System.Text;

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
        
        public static string ToString(this Guid value)
        {
            const int guidSize = 16;
            const int keyNumber = 1;
            const int allowLength = guidSize - keyNumber;
            
            var bytes = value.ToByteArray();
            var stringLength = bytes[allowLength];
		
            if(stringLength > allowLength)
            {
                throw new ArgumentException("Invalid string length. Maybe you use function not for reverse convert of string.");
            }
		
            bytes = bytes.Take(stringLength).ToArray();
            var str = Encoding.UTF8.GetString(bytes);
		
            return str;
        }
    }
}