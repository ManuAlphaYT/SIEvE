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
    public partial class ViewWorkTeacher : ContentPage
    {
        string urlMateria = "https://webmanualpha.000webhostapp.com/sieve/SelectMateriasTeacher.php?";
        string urlNameWorks = "https://webmanualpha.000webhostapp.com/sieve/SelectWorksTeacherId.php?";
        string urlTrabajos = "https://webmanualpha.000webhostapp.com/sieve/SelectWorkIdTeacher.php?";
        string urlStudentId = "https://webmanualpha.000webhostapp.com/sieve/SelectCalificarStudentId.php?";
        string urlStudentName = "https://webmanualpha.000webhostapp.com/sieve/SelectStudentNameCalificar.php?";
        string urlEstudiante = "https://webmanualpha.000webhostapp.com/sieve/SelectMatriculaStudentName.php?";
        string urlCalificar = "https://webmanualpha.000webhostapp.com/sieve/UpdateCalificar.php?";
        string IDTRABAJO, MATRICULA;
        public ViewWorkTeacher()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Regresar_Clicked(object sender, EventArgs e)
        {
            Regresar.Source = "regresarpress";

            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                Regresar.Source = "regresar";
                return false;
            });
            await Navigation.PushAsync(new WorkTeacher());
            Navigation.RemovePage(this);
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

        private void tmateria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tunidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gyg = tgrupo.SelectedItem.ToString();
            string[] gradogrupo = gyg.Split('-');
            string GRADO = gradogrupo[0];
            string GRUPO = gradogrupo[1]; 
            string MATERIA = tmateria.SelectedItem.ToString();
            string UNIDAD = tunidad.SelectedItem.ToString();
            Uri sNW = new Uri(urlNameWorks + "Unidad=" + UNIDAD + "&Grupo=" + GRUPO + "&Periodo=" + GRADO + "&Materia=" + MATERIA);
            string sNWResult = new WebClient().DownloadString(sNW);
            if(sNWResult != "No existe")
            {
                string[] sNWR_div = sNWResult.Split(',');
                int c = 0;
                List<string> works = new List<string>();
                for (int i = 0; i < sNWR_div.Length; i++)
                {
                    works.Add(sNWR_div[c]);
                    c++;
                }
                ttrabajo.ItemsSource = works;
            }
            else
            {
                List<string> clear = new List<string>();
                ttrabajo.ItemsSource = clear;
            }
        }

        private void ttrabajo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gyg = tgrupo.SelectedItem.ToString();
            string[] gradogrupo = gyg.Split('-');
            string GRADO = gradogrupo[0];
            string GRUPO = gradogrupo[1];
            string UNIDAD = tunidad.SelectedItem.ToString();
            string MATERIA = tmateria.SelectedItem.ToString();
            string TRABAJO = ttrabajo.SelectedItem.ToString();
            Uri sIT = new Uri(urlTrabajos + "Unidad=" + UNIDAD + "&Grupo=" + GRUPO + "&Periodo=" + GRADO + "&Materia=" + MATERIA + "&Titulo=" + TRABAJO);
            IDTRABAJO = new WebClient().DownloadString(sIT);
            Uri sSI = new Uri(urlStudentId + "Id=" + IDTRABAJO);
            string sSIResult = new WebClient().DownloadString(sSI);
            if(sSIResult!="No existe")
            {
                string[] sSIR_div = sSIResult.Split(',');
                int c = 0;
                List<string> students = new List<string>();
                for (int i = 1;i<sSIR_div.Length;i++)
                {
                    Uri sS = new Uri(urlStudentName + "Matricula=" + sSIR_div[c]);
                    string sSResult = new WebClient().DownloadString(sS);
                    string[] sSR_div = sSResult.Split(',');
                    students.Add(sSR_div[1] + " " + sSR_div[2] + " "+sSR_div[3]);
                }
                testudiante.ItemsSource = students;
            }
            else
            {
                List<string> vacio = new List<string>();
                testudiante.ItemsSource = vacio;
            }
            
        }

        private void testudiante_SelectedIndexChanged(object sender, EventArgs e)
        {
            string student = testudiante.SelectedItem.ToString();
            string[] s_div = student.Split(' ');
            Uri sSM = new Uri(urlEstudiante + "Nombre=" + s_div[0] + "&ApellidoP=" + s_div[1] + "&ApellidoM=" + s_div[2]);
            MATRICULA = new WebClient().DownloadString(sSM);
            

        }

        

        private void tcalificar_Clicked(object sender, EventArgs e)
        {
            string CALIFICACION = tcalificacion.SelectedItem.ToString();
            Uri uC = new Uri(urlCalificar + "Matricula=" + MATRICULA + "&Trabajo=" + IDTRABAJO + "&Calificacion=" + CALIFICACION);
            string final = new WebClient().DownloadString(uC);
            DisplayAlert("Exito", "Trabajo Calificado", "OK");
        }
    }
}