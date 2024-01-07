using BisleriumCafeBackend.constants;
using BisleriumCafeBackend.enums;
using BisleriumCafeBackend.helper;
using BisleriumCafeBackend.Model.Member;
using BisleriumCafeBackend.Model.TemporaryAttachments;
using OfficeOpenXml;
using System.Xml.Linq;

namespace BisleriumCafeBackend.Repository.TemporaryAttachmentsRepo
{
    public class TemporaryAttachmentsRepoImpl : ITemporaryAttachmentsRepo
    {
        List<Dictionary<string, object>> getFromDictionary;
        string fileName;

        public TemporaryAttachmentsRepoImpl()
        {
            fileName = FileNameEnum.GetEnumDescription((FileNameEnum.FileName.TEMPORARY_ATTACHMENTS));
            getFromDictionary = ExcelLoaderHelper.GetExcelService(fileName: fileName);
        }

        public List<TemporaryAttachments> GetAll()
        {
            return getFromDictionary.Select(e => new TemporaryAttachments
            {
                Id = Convert.ToInt32(e["Id"]),
                Name = (e["Name"]).ToString(),
                Location = (e["Location"]).ToString(),
                FileSize = Convert.ToDouble(e["FileSize"]),
                FileType = (FileType)Enum.Parse(typeof(FileType), e["FileType"].ToString(), true)
            }).ToList();
        }

        public TemporaryAttachments Save(TemporaryAttachments temporaryAttachments)
        {
            string filePath = @fileName + ".xlsx";

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Access the first worksheet
                var worksheet = package.Workbook.Worksheets[0];

                // Determine the last used row
                int lastUsedRow = worksheet.Dimension.End.Row;

                // Increment the last used row to add a new row
                int newRow = lastUsedRow + 1;

                // Assuming 'id', 'Name', and 'Price' are variables with appropriate values
                worksheet.Cells[newRow, 1].Value = temporaryAttachments.Id;         // Assuming 'Id' is in column A
                worksheet.Cells[newRow, 2].Value = temporaryAttachments.Name;  // Assuming 'Name' is in column B
                worksheet.Cells[newRow, 3].Value = temporaryAttachments.Location; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 4].Value = temporaryAttachments.FileSize; // Assuming 'Price' is in column C
                worksheet.Cells[newRow, 5].Value = temporaryAttachments.FileType; // Assuming 'Price' is in column C

                // Save the changes to the Excel file
                package.Save();
            }
            return temporaryAttachments;
        }
    }
}
