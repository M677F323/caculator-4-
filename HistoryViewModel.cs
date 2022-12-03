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
        public async void Add(String calculation)
        {
            var historyModel = new CalculationHistory();
            historyModel.calculatedItem = calculation;
            await SqlConnection.SaveItemAsync(historyModel);
            await Init();
            OnPropertyChanged("history");
            OnPropertyChanged("history");
            OnPropertyChanged();
            OnPropertyChanged();
        }







}
