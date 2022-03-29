using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;

namespace SIEvE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkStudent : ContentPage
    {
        string urlMaterias = "https://webmanualpha.000webhostapp.com/sieve/SelectStudenMaterias.php?";
        string urlTrabajos = "https://webmanualpha.000webhostapp.com/sieve/SelectAllWorks.php?";
        string urlNombres = "https://webmanualpha.000webhostapp.com/sieve/SelectNameWorks.php?";
        string urlCalificar = "https://webmanualpha.000webhostapp.com/sieve/SelectCalificar.php?";
        string Materia, Unidad, Trabajo;
        public WorkStudent()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //loadinformation();
        }

        private async void SendWork_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SendWorkStudent());
            Navigation.RemovePage(this);
        }

        private async void ViewWork_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewWorkStudent());
            Navigation.RemovePage(this);
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        public void loadinformation()
        {
            Uri sM = new Uri(urlMaterias + "Grado=" + DataUsers.Periodo);
            string sMResult = new WebClient().DownloadString(sM);
            string[] sMR_div = sMResult.Split(',');
            int c = 0;
            List<string> subjects = new List<string>();
            for (int i = 1; i < sMR_div.Length; i++)
            {
                subjects.Add(sMR_div[c]);
                c++;
            }
            Materias.ItemsSource = subjects;
        }

        private void Materias_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* Materia = Materias.SelectedItem.ToString();

            if(unidad.SelectedIndex == -1)
            {
                DisplayAlert("si funciono wey", "yes", "OK");
            }
            Unidad = unidad.SelectedItem.ToString();

            Uri uN = new Uri(urlNombres + "Unidad=" + Unidad + "&Grupo=" + DataUsers.Grupo + "&Periodo=" + DataUsers.Periodo + "&Materia=" + Materia);
            string uNResult = new WebClient().DownloadString(uN);
            if(uNResult!="No existe")
            {
                string[] uNR_div = uNResult.Split(',');
                int c = 0;
                List<string> works = new List<string>();
                for(int i = 0;i<uNR_div.Length;i++)
                {
                    works.Add(uNR_div[c]);
                    c++;
                }
                trabajo.ItemsSource = works;
            }*/

        }

        private async void IndexPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IndexStudent());
            Navigation.RemovePage(this);
        }

        private async void SchedulePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScheduleStudent());
            Navigation.RemovePage(this);
        }

        private async void GradesPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GradesStudent());
            Navigation.RemovePage(this);
        }

        private void unidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Materia = Materias.SelectedItem.ToString();
            Unidad = unidades.SelectedItem.ToString();

            Uri uN = new Uri(urlNombres + "Unidad=" + Unidad + "&Grupo=" + DataUsers.Grupo + "&Periodo=" + DataUsers.Periodo + "&Materia=" + Materia);
            string uNResult = new WebClient().DownloadString(uN);
            if (uNResult != "No existe")
            {
                string[] uNR_div = uNResult.Split(',');
                int c = 0;
                List<string> works = new List<string>();
                for (int i = 0; i < uNR_div.Length; i++)
                {
                    works.Add(uNR_div[c]);
                    c++;
                }
                trabajo.ItemsSource = works;
            }
        }

        private void trabajo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Materia = Materias.SelectedItem.ToString();
            Unidad = unidades.SelectedItem.ToString();
            Trabajo = trabajo.SelectedItem.ToString();
            Uri sW = new Uri(urlTrabajos + "Unidad=" + Unidad + "&Grupo=" + DataUsers.Grupo + "&Periodo=" + DataUsers.Periodo + "&Materia=" + Materia + "&Titulo=" + Trabajo);
            string sWResult = new WebClient().DownloadString(sW);
            
            if(sWResult!="No existe")
            {
                string[] sWR_div = sWResult.Split(',');
                tmateria.Text = sWR_div[6];
                ttitulo.Text = sWR_div[1];
                tfecha.Text = sWR_div[3];
                thora.Text = sWR_div[4];
                testado.Text = sWR_div[5];

                Uri sC = new Uri(urlCalificar + "Trabajo=" + sWR_div[0] + "&Matricula=" + DataUsers.Matricula);
                string sCResult = new WebClient().DownloadString(sC);
                if(sCResult=="No existe")
                {
                    tcalificacion.Text = "NC";
                }
                else
                {
                    tcalificacion.Text = sCResult;
                }

                WorkView.titulo = sWR_div[1];
                WorkView.descripcion = sWR_div[2];
                WorkView.fecha = sWR_div[3];
                WorkView.hora = sWR_div[4];
                WorkView.id = sWR_div[0];
            }
        }
    }
}