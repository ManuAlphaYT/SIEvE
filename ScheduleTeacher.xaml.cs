using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIEvE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleTeacher : ContentPage
    {
        string urlHorario = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectHorarioTeacher.php?";
        public ScheduleTeacher()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void IndexPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IndexStudent());
        }
        private async void GradesPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GradesTeacher());
        }
        private async void WorksPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkTeacher());
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        private void ldia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MATRICULA = DataUsers.Matricula;
            string DIA = ldia.SelectedItem.ToString();
            Uri sH = new Uri(urlHorario + "Dia=" + DIA + "&Matricula=" + MATRICULA);
            string sHResult = new WebClient().DownloadString(sH);
            string[] sHR_div = sHResult.Split(',');
            h1.Text = sHR_div[1];
            m1.Text = sHR_div[2];
            s1.Text = "Salón: " + sHR_div[3];
            h2.Text = sHR_div[4];
            m2.Text = sHR_div[5];
            s2.Text = "Salón: " + sHR_div[6];
            h3.Text = sHR_div[7];
            m3.Text = sHR_div[8];
            s3.Text = "Salón: " + sHR_div[9];
            h4.Text = sHR_div[10];
            m4.Text = sHR_div[11];
            s4.Text = "Salón: " + sHR_div[12];

        }
    }
}