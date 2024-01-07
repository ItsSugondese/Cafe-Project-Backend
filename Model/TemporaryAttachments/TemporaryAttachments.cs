using BisleriumCafeBackend.enums;

namespace BisleriumCafeBackend.Model.TemporaryAttachments
{
    public class TemporaryAttachments
    {

            public int? Id { get; set; }

            public string Name { get; set; }

            public string Location { get; set; }

            public double? FileSize { get; set; }

            public FileType FileType { get; set; }
        

    }
}
