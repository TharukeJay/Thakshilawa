using System;
using System.ComponentModel;
using System.Linq;

namespace XYZLaundry.Helpers
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Fetch Description from enum value attribute.
        /// </summary>            
        public static string GetDescription(this Enum value)
        {
            // get attributes  
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Any())
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }
    }
}
