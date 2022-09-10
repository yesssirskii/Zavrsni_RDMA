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
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage()
        {
            InitializeComponent();
            GetResultsData();
        }

        List<ResultsModel> modelList = new List<ResultsModel>();


        public async Task GetResultsData()
        {
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=results");

            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();

                var jsonObject = JObject.Parse(json);
                var status = jsonObject["error"];
                var msg = jsonObject["msg"];
                var data = jsonObject["data"];

                var jsonArray = JArray.Parse(data.ToString());

                foreach (var userObject in jsonArray)
                {
                    ResultsModel result = new ResultsModel();

                    result.id_user = userObject["id_user"].ToString();
                    result.id_exercise = userObject["id_exercise"].ToString();
                    result.skill = userObject["skill"].ToString();
                    result.result_percent = userObject["result_percent"].ToString();
                    result.result_score = userObject["result_score"].ToString();
                    result.result_max = userObject["result_max"].ToString();

                    modelList.Add(result);
                }
            }

            resultsList.ItemsSource = modelList;
        }

        private void SortByUserID(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            sortedList = modelList.OrderByDescending(o => o.id_user).ToList();
            resultsList.ItemsSource = sortedList;
        }
        private void SortByExcerciseID(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            sortedList = modelList.OrderByDescending(o => o.id_exercise).ToList();
            resultsList.ItemsSource = sortedList;
        }
        private void SortBySkill(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            sortedList = modelList.OrderByDescending(o => o.skill).ToList();
            resultsList.ItemsSource = sortedList;
        }
        private void SortByPercent(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            sortedList = modelList.OrderByDescending(o => o.result_percent).ToList();
            resultsList.ItemsSource = sortedList;
        }
        private void SortByScore(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            sortedList = modelList.OrderByDescending(o => o.result_score).ToList();
            resultsList.ItemsSource = sortedList;
        }
        private void SortByMaxScore(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            sortedList = modelList.OrderByDescending(o => o.result_max).ToList();
            resultsList.ItemsSource = sortedList;
        }




    }
}