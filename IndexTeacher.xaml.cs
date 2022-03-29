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
    public partial class IndexTeacher : ContentPage
    {
        string urlTeacher = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectAllTeacher.php?";
        string urlMedica = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectAllMedica.php?";
        string urlContacto = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectAllContato.php?";
        public IndexTeacher()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            InformationShow();
        }
        private async void SchedulePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScheduleTeacher());
        }
        private async void GradesPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GradesTeacher());
        }
        private async void WorksPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkTeacher());
        }
        public void InformationShow()
        {
            Uri uT = new Uri(urlTeacher + "Matricula=" + DataUsers.Matricula);
            string uTResult = new WebClient().DownloadString(uT);
            string[] uTR_div = uTResult.Split(',');
            Uri uM = new Uri(urlMedica + "Matricula=" + DataUsers.Matricula);
            string uMResult = new WebClient().DownloadString(uM);
            string[] uMR_DIV = uMResult.Split(',');
            Uri uC = new Uri(urlContacto + "Matricula=" + DataUsers.Matricula);
            string uCResult = new WebClient().DownloadString(uC);
            string[] uCR_div = uCResult.Split(',');

            Nombre.Text = uTR_div[2] + " " + uTR_div[3] + " " + uTR_div[4];
            Identificacion.Text = uTR_div[1];

            Sangre.Text = uMR_DIV[1];
            Alergias.Text = uMR_DIV[2];
            Donante.Text = uMR_DIV[4];
            Medicinas.Text = uMR_DIV[5];
            Emergencias.Text = uMR_DIV[7];

            Correo.Text = uCR_div[2];
            Celular.Text = uCR_div[3];
            Telefono.Text = uCR_div[4];
            
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}