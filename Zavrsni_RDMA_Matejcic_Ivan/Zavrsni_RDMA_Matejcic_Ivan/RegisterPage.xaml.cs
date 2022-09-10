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
using Zavrsni_RDMA_Matejcic_Ivan;

namespace Zavrsni_RDMA_Matejcic_Ivan
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {

        public string APIKey = "AIzaSyD4ODNc8RLMUemzQ8OWp43FEeg9Nxjrnhk";

        public RegisterPage()
        {
            InitializeComponent();
        }

        async void UserRegistrationButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(APIKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(EmailEntry.Text, PasswordEntry.Text);
                string getToken = auth.FirebaseToken;
                await App.Current.MainPage.DisplayAlert("Successful registration!", "Welcome to LanGuide!", "OK");
                await Navigation.PushAsync(new DashboardPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }
        }
    }
}