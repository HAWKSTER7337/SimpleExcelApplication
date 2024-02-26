namespace TicketApprentice;

/**
 * This is the main and only page of the application.
 * On the bottom of the screen will be an excel window
 * On the top of the page will be a clear button and a send button
 *      - The clear button will restart the screen making the excel sheet clean again
 *      - The Send button will allow you to send the Excel file over your cell number
 */
public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}