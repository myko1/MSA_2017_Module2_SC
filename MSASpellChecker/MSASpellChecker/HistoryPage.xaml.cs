using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSASpellChecker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        async void SeeHistory(object sender, System.EventArgs e)
        {
            List<kahawaiModel> history = await AzureManager.AzureManagerInstance.GetHistory();

            SentenceList.ItemsSource = history;
        }

        async void ClearHistory(object sender, System.EventArgs e)
        {
            List<kahawaiModel> history = await AzureManager.AzureManagerInstance.GetHistory();

            foreach (kahawaiModel element in history)
            {
                await AzureManager.AzureManagerInstance.ClearHistory(element);
            }

            List<kahawaiModel> cleared = await AzureManager.AzureManagerInstance.GetHistory();

            SentenceList.ItemsSource = cleared;
        }
    }
}