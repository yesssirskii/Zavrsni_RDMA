using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zavrsni_RDMA_Matejcic_Ivan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguagesPage : ContentPage
    {
        List<LanguagesModel> list = new List<LanguagesModel>();
        List<LanguagesModel> listDistinct = new List<LanguagesModel>();
        public LanguagesPage()
        {
            InitializeComponent();
            DownloadLanguageData();
        }

        public async Task DownloadLanguageData()
        {
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=results");
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                string jsonString = json.ToString();
                var jObject = JObject.Parse(jsonString);
                var jsonDataArray = JArray.Parse(jObject["data"].ToString());

                foreach (var resultObject in jsonDataArray)
                {
                    LanguagesModel language = new LanguagesModel();
                    language.language = resultObject["language"].ToString();
                    list.Add(language);
                }
            }

            listOfLanguages.ItemsSource = list.GroupBy(x => x.language).Select(x => x.First());
        }
    }
}