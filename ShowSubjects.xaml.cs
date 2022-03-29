using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIEvE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowSubjects : ContentPage
    {
        string urlMaterias = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectMaterias.php";
        public ShowSubjects()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Subjects();
        }

        private async void Regresar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            Navigation.RemovePage(this);
        }

        public void Subjects()
        {
            Uri sM = new Uri(urlMaterias);
            string sMResult = new WebClient().DownloadString(sM);
            string[] sM_div = sMResult.Split(',');
            ListSubject.ItemsSource = sM_div;
            /*int c = 0;
            for(int i = 1;i<10;i++)
            {
                ListSubject.ItemsSource = sM_div;
                c++;
            }*/


        }
    }
}