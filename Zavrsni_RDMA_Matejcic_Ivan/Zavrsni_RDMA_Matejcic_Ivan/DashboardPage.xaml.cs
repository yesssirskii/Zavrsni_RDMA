using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zavrsni_RDMA_Matejcic_Ivan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public string APIKey = "AIzaSyD4ODNc8RLMUemzQ8OWp43FEeg9Nxjrnhk";

        public DashboardPage()
        {
            InitializeComponent();

            GetProfileInformationAndRefreshToken();
        }

        private async void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(APIKey));

            try
            {
                // Deserializing saved Firebase authentication details from login:
                var savedFirebaseAuth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                // Refreshing token:
                var refreshedContent = await authProvider.RefreshAuthAsync(savedFirebaseAuth);
                Preferences.Set("MyFirebaserefreshToken", JsonConvert.SerializeObject(refreshedContent));
                // Getting user email:
                MyUserName.Text = savedFirebaseAuth.User.Email;
            }

            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
                await App.Current.MainPage.DisplayAlert("Alert", "Token expired.", "OK");
            }
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("MyFirebaserefreshToken");
            App.Current.MainPage = new NavigationPage(new MainPage());
        }

        private void userButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UsersPage());
        }

        private void resultsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResultsPage());
        }

        private void languagesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LanguagesPage());
        }
    }
}