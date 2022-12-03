namespace Calculator;

public partial class History : ContentPage
{
	public History()
	{
		InitializeComponent();
		BindingContext = App.historyViewModel;
	}
    private async void Clear_Clicked(object sender, EventArgs e)
    {
        await App.historyViewModel.clearDatabase();
        
    }
}