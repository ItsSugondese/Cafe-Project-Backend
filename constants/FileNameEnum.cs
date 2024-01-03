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
            ADD_IN
        }


        public static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? value.ToString() : attribute.Description;

        }
    }
}
