using BisleriumCafeBackend.enums;
using BisleriumCafeBackend.Model.Coffee;
using BisleriumCafeBackend.Model.TemporaryAttachments;
using BisleriumCafeBackend.pojo.TemporaryAttachments;
using BisleriumCafeBackend.Repository.CoffeeRepo;
using BisleriumCafeBackend.Repository.TemporaryAttachmentsRepo;
using BisleriumCafeBackend.utils.GenericFile;

namespace BisleriumCafeBackend.Services.TemporaryAttachmentsServices
{
    public class TemporaryAttachmentsServiceImpl : ITemporaryAttachmentsService
    {
        private readonly GenericFileUtils _genericFileUtils;
        private readonly ITemporaryAttachmentsRepo temporaryAttachmentsRepo;
        public TemporaryAttachmentsServiceImpl(ITemporaryAttachmentsRepo temporaryAttachmentsRepo)
        {
            this.temporaryAttachmentsRepo = temporaryAttachmentsRepo;
            _genericFileUtils = new GenericFileUtils();
        }
        
        
            public List<int> SaveTemporaryAttachment(TemporaryAttachmentsDetailRequestPojo detailRequestPojo)
            {
                List<int> savedTemporaryAttachmentId = new List<int>();
            int i = 0;
                if (detailRequestPojo.Attachments != null)
                {
                int tempId = 0;
                List<TemporaryAttachments> attachmentsList = temporaryAttachmentsRepo.GetAll();

               
                    if (attachmentsList.Count() > 0)
                    {
                        TemporaryAttachments lastAttachments = attachmentsList.Last();
                        tempId = (int) lastAttachments.Id + 1;
                    }
                    else
                    {
                        tempId = 1;
                    }
                    foreach (var ticketAttachment in detailRequestPojo.Attachments)
                    {
                        Dictionary<string, object> ticketAttachments = _genericFileUtils.SaveTempFile(ticketAttachment,
                            new List<FileType> { FileType.IMAGE, FileType.DOC, FileType.PDF, FileType.EXCEL });
                        string filename = ticketAttachment.FileName;

                        float fileSize = (float)ticketAttachment.Length / (1024 * 1024);
                        SaveTemporaryFile(savedTemporaryAttachmentId, filename, fileSize, (FileType)ticketAttachments["fileType"],
                            (string)ticketAttachments["location"], detailRequestPojo.Name != null ? detailRequestPojo.Name[i] : filename, tempId);
                        i++;
                    tempId++;
                    }
                }

                return savedTemporaryAttachmentId;
            }

        private void SaveTemporaryFile(List<int> savedTemporaryAttachmentId, string fileName, float? fileSize, FileType fileType, string ticketAttachments, string originalFileName, int id)
        {
            TemporaryAttachments temporaryAttachments = new TemporaryAttachments
            {
                Id = id,
                Location = ticketAttachments,
                Name = originalFileName ?? fileName,
                FileSize = fileSize,
                FileType = fileType
            };

            TemporaryAttachments savedTemporaryAttachments = temporaryAttachmentsRepo.Save(temporaryAttachments);
            savedTemporaryAttachmentId.Add((int)savedTemporaryAttachments.Id);
        }


    }

}
