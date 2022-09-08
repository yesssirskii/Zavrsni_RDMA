using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Zavrsni_RDMA_Matejcic_Ivan
{
    public partial class MainPage : ContentPage
    {
        public string APIKey = "AIzaSyD4ODNc8RLMUemzQ8OWp43FEeg9Nxjrnhk";

        public MainPage()
        {
            InitializeComponent();
            var assembly = typeof(MainPage);
            MainPageImage.Source = ImageSource.FromResource("Zavrsni_RDMA_Matejcic_Ivan.Assets.Images.3.jpg", assembly);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(APIKey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(EmailIDEntry.Text, PasswordEntry.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedContent);
                await Navigation.PushAsync(new DashboardPage());
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid username or password.", "OK");
            }
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
