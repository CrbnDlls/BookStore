using DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayValue(Genre value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }
    }
}
