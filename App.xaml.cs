using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIEvE
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            Device.SetFlags(new string[] { "RadioButton_Experimental" });
        }

        protected override void OnStart()
        {
             
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
