using Calculator.Services;

namespace Calculator.Pages;

public partial class APITestPage : ContentPage
{
    public int questionNumber = 0;
    public string correctOptionValue;
    public APITestPage()
	{
		InitializeComponent();
        loadSavedQuestionNumber();
        updateQuestionData();
	}

    public void loadSavedQuestionNumber()
    {
        questionNumber = StorageService.currNum;
    }

    public void goToNextQuestion()
    {
        ++questionNumber;
        updateQuestionData();
    }

    public async void updateQuestionData()
    {
        TestAPIService api = new TestAPIService();
        var questionData = await api.getTestQuestion(questionNumber);
        questionText.Text = questionData.questionText;
        optionOneBtn.Text = questionData.optionOne;
        optionTwoBtn.Text = questionData.optionTwo;
        optionThreeBtn.Text = questionData.optionThree;
        correctOptionValue = questionData.correctOptionValue;
    }

    private async void renderResults(String actualAns)
    {
        if(optionOneBtn.Text == actualAns)
        {
            optionOneBtn.BackgroundColor = Colors.Green;
            optionTwoBtn.BackgroundColor = Colors.Red;
            optionThreeBtn.BackgroundColor = Colors.Red;
        }
        if (optionTwoBtn.Text == actualAns)
        {
            optionTwoBtn.BackgroundColor = Colors.Green;
            optionThreeBtn.BackgroundColor = Colors.Red;
            optionOneBtn.BackgroundColor = Colors.Red;

        }
        if (optionThreeBtn.Text == actualAns)
        {
            optionThreeBtn.BackgroundColor = Colors.Green;
            optionOneBtn.BackgroundColor = Colors.Red;
            optionTwoBtn.BackgroundColor = Colors.Red;

        }
    }

    private async void onOptionClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        var tempColor = button.BackgroundColor;
        string buttonValue = button.Text;
        //renderResults(correctOptionValue);
        await Task.Delay(1000);
        if (buttonValue == correctOptionValue)
        {
            await DisplayAlert("Correct", buttonValue + " is the Correct Answer", "Next Question");
            optionOneBtn.BackgroundColor = tempColor;
            optionTwoBtn.BackgroundColor = tempColor;
            optionThreeBtn.BackgroundColor = tempColor;
            goToNextQuestion();
            return;
        }
        if(await DisplayAlert("Wrong", "Would you like to play a game", "Try Again", "Skip to Next Question"))
        {
            return;
        }
        else
        {          
            goToNextQuestion();
        }
        return;
    }
}