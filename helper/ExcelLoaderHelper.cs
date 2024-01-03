using ExcelDataReader;
using System.Data;
using System.Text;

namespace BisleriumCafeBackend.helper
{
    public class ExcelLoaderHelper
    {
        public static List<Dictionary<string, object>> GetExcelService(String fileName)
        {
            string filePath = @fileName + ".xlsx";

            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

            // Specify the encoding when creating the ExcelReader
            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding(1252); // You can adjust this encoding as needed

                // Create the ExcelDataReader with specified encoding
                using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration { FallbackEncoding = encoding }))
                {
                    // Choose one of the ExcelDataReader methods to read the data

                    DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });

                    // The result is a DataSet containing one or more DataTables
                    // You can access the data from the DataTables as needed
                    DataTable dataTable = result.Tables[0];

                    foreach (DataRow row in dataTable.Rows)
                    {
                        Dictionary<string, object> rowData = new Dictionary<string, object>();

                        foreach (DataColumn column in dataTable.Columns)
                        {
                            rowData.Add(column.ColumnName, row[column]); // Add column name and corresponding value to the dictionary
                        }

                        data.Add(rowData); // Add the row data to the list
                    }
                }
            }

            return data;
        }
    }
}
