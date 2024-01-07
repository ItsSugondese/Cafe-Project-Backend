using BisleriumCafeBackend.Model.TemporaryAttachments;

namespace BisleriumCafeBackend.Repository.TemporaryAttachmentsRepo
{
    public interface ITemporaryAttachmentsRepo
    {
        TemporaryAttachments Save(TemporaryAttachments temporaryAttachments);

        List<TemporaryAttachments> GetAll();
    }
}
