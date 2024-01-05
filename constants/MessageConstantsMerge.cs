namespace BisleriumCafeBackend.constants
{
    public class MessageConstantsMerge
    {
        public static string requetMessage(string requestType, string moduleName) 
        {
            return moduleName + " has been " + requestType + " successfully.";
        }
        public static string notExist(string attribue, string moduleName) 
        {
            return moduleName + " with that " + attribue + " doesn't exists.";
        }
        
        public static string alreadyExist(string attribue, string moduleName) 
        {
            return moduleName + " with that " + attribue + " already exists.";
        }
    }
}
