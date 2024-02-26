using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Microsoft.Maui.Storage;

namespace TicketApprentice.CsvFileComponents;

public partial class GridPage : ContentPage
{
    public ObservableCollection<GridDataItem> GridData { get; set; } = new ObservableCollection<GridDataItem>();

    public GridPage()
    {
        InitializeComponent();

        // Initialize with 100 empty rows
        for(int i = 0; i < 100; i++)
        {
            GridData.Add(new GridDataItem());
        }

        this.BindingContext = this;
    }

    private async void OnShareAsCsvClicked(object sender, EventArgs e)
    {
        // Export GridData to CSV format
        var csvData = ExportToCsv(GridData);

        // Saving the CSV content to a temporary file (for sharing)
        var fileName = Path.Combine(FileSystem.CacheDirectory, "gridData.csv");
        await File.WriteAllTextAsync(fileName, csvData);

        // Sharing the file
        await Share.RequestAsync(new ShareFileRequest
        {
            Title = "Share CSV Data",
            File = new ShareFile(fileName)
        });
    }
    
    private string ExportToCsv(ObservableCollection<GridDataItem> data)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Data1,Data2,Data3,Data4,Data5"); // Header

        foreach(var item in data)
        {
            stringBuilder.AppendLine($"\"{item.Data1}\",\"{item.Data2}\",\"{item.Data3}\",\"{item.Data4}\",\"{item.Data5}\"");
        }

        return stringBuilder.ToString(); 
    }
    
    // Event handler for clearing data
    private async void OnClearDataClicked(object sender, EventArgs e)
    {
        bool isConfirmed = await DisplayAlert("Confirm", "Are you sure you want to clear all data?", "Yes", "No");
        if (isConfirmed)
        {
            foreach (var item in GridData)
            {
                item.Data1 = "";
                item.Data2 = "";
                item.Data3 = "";
                item.Data4 = "";
                item.Data5 = "";
            }
            // This line ensures the UI gets updated after clearing the data
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = GridData;
        }
    }
}