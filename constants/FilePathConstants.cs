namespace BisleriumCafeBackend.constants
{
    public class FilePathConstants
    {
        public static readonly string FileSeparator = Path.DirectorySeparatorChar.ToString();
        public static readonly string ProjectPath = Directory.GetCurrentDirectory();
        public static readonly string ProjectName = new DirectoryInfo(ProjectPath).Name;
        public static readonly string PresentDir = ProjectPath.Substring(0, ProjectPath.LastIndexOf(FileSeparator, StringComparison.Ordinal));

        public static readonly string UploadDir = Path.Combine(PresentDir, "fyp-document", "fyp");
        public static readonly string TempPath = Path.Combine(PresentDir, "fyptempdocument", "doc");
    }
}

