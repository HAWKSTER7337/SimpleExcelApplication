using TicketApprentice.CsvFileComponents;

namespace TicketApprentice;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new GridPage();
    }
}