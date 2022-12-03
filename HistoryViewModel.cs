using Calculator.Models;
 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Android.Content.ClipData;

namespace Calculator
{
    public class HistoryViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<CalculationHistory> calcualtionHistory;
        private SQLliteConnection SqlConnection;


        public HistoryViewModel()
        {
            calcualtionHistory = new ObservableCollection<CalculationHistory>();
            SqlConnection=new SQLliteConnection();

            Init();
        }









        public async Task Init()
        {
            var res = await SqlConnection.GetHistoryAsync();
            calcualtionHistory = new ObservableCollection<CalculationHistory>();
            res.ForEach(calcualtionHistory.Add);
        }

        public async Task clearDatabase()
        {
            calcualtionHistory = new ObservableCollection<CalculationHistory>();
            OnPropertyChanged("history");
            await SqlConnection.DeleteAllAsync();
            await Init();
            OnPropertyChanged("history");
            OnPropertyChanged();
            OnPropertyChanged();
        }



        public ObservableCollection<CalculationHistory> historyExpressions
        {
            get => calcualtionHistory;
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
