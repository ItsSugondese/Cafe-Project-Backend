using System.ComponentModel;

namespace BisleriumCafeBackend.constants
{
    public class FileNameEnum
    {
        public enum FileName
        {
            [Description("login")]
            LOGIN_DETAILS,
            [Description("add-in")]
            ADD_IN,
            [Description("coffee")]
            COFFEE,
            [Description("member")]
            MEMBER,
            [Description("order")]
            ORDER,
            [Description("coffee-redeem")]
            COFFEE_REDEEM,
            [Description("order-add-in")]
            ORDER_ADD_IN,
            [Description("transaction")]
            TRANSACTION ,
            [Description("temporary-attachments")]
            TEMPORARY_ATTACHMENTS
        }


        public static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;

        }
    }
}
