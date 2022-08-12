using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace OnlineGameStore.BLL.Utils
{
    public static class StringExtensions
    {
        public static string ToKebabCase(this string value)
        {
            value = Regex.Replace(value, @"[^0-9a-zA-Z']", "-");

            value = Regex.Replace(value, @"[-]{2,}", "-");

            value = Regex.Replace(value, @"-+$", string.Empty);

            if (value.StartsWith("-")) value = value[1..];

            return value.ToLower();
        }
        
        public static Guid ToGuid(this string value)
        {
            const int guidSize = 16;
            const int keyNumber = 1;
            const int allowLength = guidSize - keyNumber;
            
            Guid guid;

            if (value.Length <= allowLength)
            {
                var bytes = new List<byte>(guidSize);
                bytes.AddRange(Encoding.UTF8.GetBytes(value));
                var arrayNeedLength = allowLength - bytes.Count;

                if (arrayNeedLength > 0)
                {
                    var randomArray = Randomizer.GetRandomByteArray(arrayNeedLength);
                    bytes = bytes.Concat(randomArray).ToList();
                }

                bytes.Add((byte)value.Length);

                guid = new Guid(bytes.ToArray());
            }
            else
            {
                throw new ArgumentException($"String contains more than {allowLength} characters.");
            }
		
            return guid;
        }
    }
}