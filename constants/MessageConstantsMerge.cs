namespace BisleriumCafeBackend.constants
{
    public class MessageConstantsMerge
    {
        public static string requetMessage(string requestType, string moduleName) 
        {
            return moduleName + " has been " + requestType + " successfully.";
        }
    }
}
