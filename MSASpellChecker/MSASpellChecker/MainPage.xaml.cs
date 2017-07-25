using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MSASpellChecker
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnClick(object sender, System.EventArgs e)
        {
            SpellModel data = await spellCheck();
            display(data);
        }

        public void display(SpellModel data)
        {
            string apiResult = "";
            foreach (var item in data.flaggedTokens)
            {
                apiResult += "Wrong word: " + item.token + "\n" + "Suggested word: " + item.suggestions[0].suggestion + "\n\n";
            }
            Result.Text = apiResult;
        }

        async Task<SpellModel> spellCheck()
        {
            List<SpellModel> spellModel = new List<SpellModel>();
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "6daabdf7ea5a4d56baa2bacfbd950ca5");
            string text = text1.Text;
            string mode = "proof";
            string mkt = "en-us";
            var url = "https://api.cognitive.microsoft.com/bing/v5.0/spellcheck/?";
            string apiCall = url + "text=" + text + "&mode=" + mode + "&mkt=" + mkt;
            
            HttpResponseMessage result = await client.GetAsync(apiCall);
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SpellModel>(json);
                return data;
            }
            else
            {
                Result.Text = "!@#$%^&*()";
                return null;
            }
        }
    }
}
