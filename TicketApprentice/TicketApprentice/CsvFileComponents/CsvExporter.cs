using System.Text;

namespace TicketApprentice.CsvFileComponents;

public static class CsvExporter
{
    public static string ExportToCsv<T>(IEnumerable<T> data)
    {
        var csvBuilder = new StringBuilder();
        var properties = typeof(T).GetProperties();
        
        foreach(var prop in properties)
        {
            csvBuilder.Append(prop.Name + ",");
        }
        
        csvBuilder.Remove(csvBuilder.Length - 1, 1).AppendLine();
        
        foreach (var item in data)
        {
            foreach(var prop in properties)
            {
                var value = prop.GetValue(item, null)?.ToString();
                csvBuilder.Append(value + ",");
            }
            csvBuilder.Remove(csvBuilder.Length - 1, 1).AppendLine();
        }

        return csvBuilder.ToString();
    }

    public static void SaveCsvToFile(string filePath, string csvContent)
    {
        File.WriteAllText(filePath, csvContent);
    }
    
    public static async Task ShareCsvFile(string filePath)
    {
        await Share.RequestAsync(new ShareFileRequest
        {
            Title = "Share CSV",
            File = new ShareFile(filePath)
        });
    }
}
