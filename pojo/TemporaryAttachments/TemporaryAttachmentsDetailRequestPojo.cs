using System.Text.Json.Serialization;

namespace BisleriumCafeBackend.pojo.TemporaryAttachments
{
    public class TemporaryAttachmentsDetailRequestPojo
    {
        public int? Id { get; set; }
        public List<IFormFile> Attachments { get; set; }

        public List<string>? FilePaths { get; set; }

        public List<string>? Name { get; set; }

    }
}
