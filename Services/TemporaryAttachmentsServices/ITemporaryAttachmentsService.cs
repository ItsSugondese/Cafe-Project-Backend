using BisleriumCafeBackend.pojo.TemporaryAttachments;

namespace BisleriumCafeBackend.Services.TemporaryAttachmentsServices
{
    public interface ITemporaryAttachmentsService
    {
        List<int> SaveTemporaryAttachment(TemporaryAttachmentsDetailRequestPojo requestPojo);
    }
}
