using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography;

namespace SIEvE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkTeacher : ContentPage
    {
        string urlMateria = "https://webmanualpha.000webhostapp.com/sieve/SelectMateriasTeacher.php?";
        string urlWork = "https://webmanualpha.000webhostapp.com/sieve/InsertWork.php?";
        public WorkTeacher()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            
        }

        private void Make_Clicked(object sender, EventArgs e)
        {
            string gyg = tgrupo.SelectedItem.ToString();
            string[] gradogrupo = gyg.Split('-');

            string UNIDAD = tunidad.SelectedItem.ToString();
            string MATERIA = tmateria.SelectedItem.ToString();
            string GRUPO = gradogrupo[1];
            string PERIODO = gradogrupo[0];
            string DESCRIPCION = tdescription.Text;
            string TITULO = ttitulo.Text;
            string FECHA = tdate.Date.ToShortDateString().ToString();
            string HORA = ttime.Time.ToString();

            Uri iW = new Uri(urlWork + "Titulo=" + TITULO + "&Descripcion=" + DESCRIPCION + "&Fecha=" + FECHA + "&Hora=" + HORA + "&Unidad=" + UNIDAD + "&Grupo=" + GRUPO + "&Periodo=" + PERIODO + "&Materia=" + MATERIA);
            string iWResult = new WebClient().DownloadString(iW);
            DisplayAlert("Creado", "Trabajo Creado", "OK");
        }

        private void tunidad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tmateria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tgrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gyg = tgrupo.SelectedItem.ToString();
            string[] gradogrupo = gyg.Split('-');
            Uri sM = new Uri(urlMateria + "Grado=" + gradogrupo[0] + "&Matricula=" + DataUsers.Matricula);
            string sMResult = new WebClient().DownloadString(sM);
            string[] sMR_div = sMResult.Split(',');

            List<string> subjects = new List<string>();
            subjects.Add(sMR_div[0]);
            tmateria.ItemsSource = subjects;
        }

        private async void regresarpage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IndexTeacher());
            Navigation.RemovePage(this);
        }

        private async void tcalificar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewWorkTeacher());
        }
    }
}