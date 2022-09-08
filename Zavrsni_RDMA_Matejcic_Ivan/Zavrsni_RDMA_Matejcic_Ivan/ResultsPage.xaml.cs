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

        bool UserIdDescending = false;
        bool TestId = false;
        bool Skill = false;
        bool Percent = false;
        bool Score = false;
        bool MaxV = false;

        private void SortByUserID(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            if (UserIdDescending)
                sortedList = modelList.OrderBy(o => o.id_user).ToList();
            else
                sortedList = modelList.OrderByDescending(o => o.id_user).ToList();

            UserIdDescending = !UserIdDescending;
            resultsList.ItemsSource = null;
            resultsList.ItemsSource = sortedList;
        }
        private void SortByExcerciseID(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            if (TestId)
                sortedList = modelList.OrderBy(o => o.id_exercise).ToList();
            else
                sortedList = modelList.OrderByDescending(o => o.id_exercise).ToList();

            TestId = !TestId;
            resultsList.ItemsSource = null;
            resultsList.ItemsSource = sortedList;
        }
        private void SortBySkill(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            if (Skill)
                sortedList = modelList.OrderBy(o => o.skill).ToList();
            else
                sortedList = modelList.OrderByDescending(o => o.skill).ToList();

            Skill = !Skill;
            resultsList.ItemsSource = null;
            resultsList.ItemsSource = sortedList;
        }
        private void SortByPercent(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            if (Percent)
                sortedList = modelList.OrderBy(o => o.result_percent).ToList();
            else
                sortedList = modelList.OrderByDescending(o => o.result_percent).ToList();

            Percent = !Percent;
            resultsList.ItemsSource = null;
            resultsList.ItemsSource = sortedList;
        }
        private void SortByScore(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            if (Score)
                sortedList = modelList.OrderBy(o => o.result_score).ToList();
            else
                sortedList = modelList.OrderByDescending(o => o.result_score).ToList();

            Score = !Score;
            resultsList.ItemsSource = null;
            resultsList.ItemsSource = sortedList;
        }
        private void SortByMaxScore(object sender, EventArgs e)
        {
            List<ResultsModel> sortedList = new List<ResultsModel>();
            if (MaxV)
                sortedList = modelList.OrderBy(o => o.result_max).ToList();
            else
                sortedList = modelList.OrderByDescending(o => o.result_max).ToList();

            MaxV = !MaxV;
            resultsList.ItemsSource = null;
            resultsList.ItemsSource = sortedList;
        }




    }
}