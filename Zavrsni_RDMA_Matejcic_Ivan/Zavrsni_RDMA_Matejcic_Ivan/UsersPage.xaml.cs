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
    public partial class UsersPage : ContentPage
    {
        List<UsersModel> list = new List<UsersModel>();
        public UsersPage()
        {
            InitializeComponent();
            _ = DownloadUserData();
        }

        public async Task DownloadUserData()
        {
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=users");
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                string jsonString = json.ToString();
                var jObject = JObject.Parse(jsonString);
                var jsonDataArray = JArray.Parse(jObject["data"].ToString());

                foreach (var userObject in jsonDataArray)
                {
                    UsersModel user = new UsersModel();
                    user.userID = userObject["id_user"].ToString();
                    user.email = userObject["email"].ToString();
                    user.time = userObject["create_time"].ToString();
                    list.Add(user);
                }
            }
            allUsers.ItemsSource = list;
        }
        public void SortByID(object sender, System.EventArgs e)
        {
            allUsers.ItemsSource = list;
        }

        public void SortByTime(object sender, System.EventArgs e)
        {
            List<UsersModel> sortedListTime = list.OrderByDescending(x => x.time).ToList();
            allUsers.ItemsSource = sortedListTime;
        }

        public void SortByEMail(object sender, System.EventArgs e)
        {
            List<UsersModel> sortedListEMail = list.OrderBy(x => x.email).ToList();
            allUsers.ItemsSource = sortedListEMail;
        }
    }
}
