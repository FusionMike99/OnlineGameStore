using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineGameStore.MVC.Infrastructure
{
    public static class EnumExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum obj)
            where TEnum : Enum
        {
            const string dataValueField = "Value";
            const string dataTextField = "Text";
            
            return new SelectList(Enum.GetValues(typeof(TEnum))
                .OfType<Enum>()
                .Select(x => new SelectListItem
                {
                    Text = x.AddSpaceBetweenUpperCase(),
                    Value = Convert.ToInt32(x).ToString()
                }), dataValueField, dataTextField, Convert.ToInt32(obj));
        }

        private static string AddSpaceBetweenUpperCase(this Enum value)
        {
            const string pattern = "[A-Z]";
            const string replacement = " $0";
            
            var newValue = Regex.Replace(value.ToString(), pattern, replacement)
                .Trim();

            return newValue;
        }
    }
}