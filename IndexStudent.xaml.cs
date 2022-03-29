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
    public partial class IndexStudent : ContentPage
    {
        string urlStudent = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectAllStudent.php?";
        string urlMedica = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectAllMedica.php?";
        string urlContacto = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectAllContato.php?";
        public IndexStudent()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            InformationShow();
        }
        private async void SchedulePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScheduleStudent());
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

        public void InformationShow()
        {
            string mtr = DataUsers.Matricula;
            
            Uri uS = new Uri(urlStudent + "Matricula=" + mtr);
            string uSResult = new WebClient().DownloadString(uS);
            string[] uSR_div = uSResult.Split(',');
            Uri uM = new Uri(urlMedica + "Matricula=" + mtr);
            string uMResult = new WebClient().DownloadString(uM);
            string[] uMR_DIV = uMResult.Split(',');
            Uri uC = new Uri(urlContacto + "Matricula=" + mtr);
            string uCResult = new WebClient().DownloadString(uC);
            string[] uCR_div = uCResult.Split(',');

            

            Nombre.Text = uSR_div[3] + " " + uSR_div[4] + " " + uSR_div[5];
            Identificacion.Text = uSR_div[1];
            Periodo.Text = "1";
            Grupo.Text = uSR_div[6];

            Sangre.Text = uMR_DIV[1];
            Alergias.Text = uMR_DIV[2];
            Donante.Text = uMR_DIV[4];
            Medicinas.Text = uMR_DIV[5];
            Emergencias.Text = uMR_DIV[7];

            Correo.Text = uCR_div[2];
            Celular.Text = uCR_div[3];
            Telefono.Text = uCR_div[4];
            DataUsers.Periodo = Periodo.Text;
            DataUsers.Grupo = uSR_div[6];
        }
    }
}