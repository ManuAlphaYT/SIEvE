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
    public partial class ScheduleStudent : ContentPage
    {
        string urlHorario = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectHorarioStudent.php?";
        public ScheduleStudent()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            tgrupo.Text = DataUsers.Grupo;
            tperiodo.Text = DataUsers.Periodo;
        }

        private async void IndexPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IndexStudent());
        }
        private async void GradesPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GradesStudent());
        }
        private async void WorksPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkStudent());
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        private void ldia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DIA = ldia.SelectedItem.ToString();
            string PERIODO = DataUsers.Periodo;
            string GRUPO = DataUsers.Grupo;
            Uri sH= new Uri(urlHorario + "Dia=" + DIA + "&Periodo=" + PERIODO + "&Grupo=" + GRUPO);
            string sHResult = new WebClient().DownloadString(sH);
            string[] sHR_div = sHResult.Split(',');
            h1.Text = sHR_div[1];
            m1.Text = sHR_div[2];
            s1.Text = "Salón: "+sHR_div[3];
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