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
    public partial class GradesTeacher : ContentPage
    {
        string urlMaterias = "https://webmanualpha.000webhostapp.com/sieve/SelectMateriasTeacher.php?";
        string urlStudent = "https://webmanualpha.000webhostapp.com/sieve/SelectStudentsGroup.php?";
        string urlEstudiante = "https://webmanualpha.000webhostapp.com/sieve/SelectMatriculaStudentName.php?";
        string urlCalificaciones = "https://webmanualpha.000webhostapp.com/sieve/UpdateCalificaciones.php?";
        string urlSubject = "https://webmanualpha.000webhostapp.com/sieve/SelectIdMaterias.php?";
        string urlAsistencia = "https://webmanualpha.000webhostapp.com/sieve/InsertAsistencia.php?";
        public GradesTeacher()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void CC_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(CC.IsChecked)
            {
                tunidad.IsVisible = true;
                tcalf.IsVisible = true;
                register.IsVisible = true;

                tasistencia.IsVisible = false;
                registera.IsVisible = false;
            }
            else if(CA.IsChecked)
            {
                tunidad.IsVisible = false;
                tcalf.IsVisible = false;
                register.IsVisible = false;

                tasistencia.IsVisible = true;
                registera.IsVisible = true;
            }
        }

        private void CA_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (CC.IsChecked)
            {
                tunidad.IsVisible = true;
                tcalf.IsVisible = true;
                register.IsVisible = true;

                tasistencia.IsVisible = false;
                registera.IsVisible = false;
            }
            else if (CA.IsChecked)
            {
                tunidad.IsVisible = false;
                tcalf.IsVisible = false;
                register.IsVisible = false;

                tasistencia.IsVisible = true;
                registera.IsVisible = true;
            }
        }

        private void tgr_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gyg = tgr.SelectedItem.ToString();
            string[] gradogrupo = gyg.Split('-');
            Uri sM = new Uri(urlMaterias + "Grado=" + gradogrupo[0] + "&Matricula=" + DataUsers.Matricula);
            string sMResult = new WebClient().DownloadString(sM);
            string[] sMR_div = sMResult.Split(',');
            
            List<string> subjects = new List<string>();
            subjects.Add(sMR_div[0]);
            tsubject.ItemsSource = subjects;

            Uri sS = new Uri(urlStudent + "Grupo=" + gradogrupo[1] + "&Periodo=" + gradogrupo[0]);
            string sSResult = new WebClient().DownloadString(sS);
            if (sSResult != "No existe")
            {
                string[] sSR_div = sSResult.Split(',');
                int c = 1;
                List<string> names = new List<string>();
                for (int i = 0; i < Convert.ToInt32(sSR_div[0]); i++)
                {
                    names.Add(sSR_div[c] + " " + sSR_div[c + 1] + " " + sSR_div[c + 2]);
                    c = c + 3;
                }
                testudiante.ItemsSource = names;
            }
        }

        private void testudiante_SelectedIndexChanged(object sender, EventArgs e)
        {
            string alumno = testudiante.SelectedItem.ToString();
            tnombre.Text = alumno;
            string[] al_div = alumno.Split(' ');
            Uri sM = new Uri(urlEstudiante + "Nombre=" + al_div[0] + "&ApellidoP=" + al_div[1] + "&ApellidoM=" + al_div[2]);
            string sMResult = new WebClient().DownloadString(sM);
            matricula.Text = sMResult;

        }

        private void tsubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            string m = tsubject.SelectedItem.ToString();
            tmateria.Text = m;        
        }
        private void register_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert("c", "", "OK");
            string materia = tsubject.SelectedItem.ToString();
            Uri U = new Uri(urlSubject + "Materia=" + materia);
            string materia_id = new WebClient().DownloadString(U);
            string unidad = tunidad.SelectedItem.ToString();
            if(unidad == "I")
            {
                unidad = "u1";
            }
            else if(unidad == "II")
            {
                unidad = "u2";
            }
            else if (unidad == "III")
            {
                unidad = "u3";
            }
            string ide = matricula.Text;
            string califi = tcalf.SelectedItem.ToString();
            Uri uC = new Uri(urlCalificaciones + "Materia=" + materia_id + "&Unidad=" + unidad + "&Estudiante=" + ide + "&Calificacion=" + califi);
            string uCResult = new WebClient().DownloadString(uC);
            DisplayAlert("Exito", "Calificación capturada", "OK");
        }

        private void registera_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert("A", "", "OK");
            string materia = tsubject.SelectedItem.ToString();
            Uri U = new Uri(urlSubject + "Materia=" + materia);
            string materia_id = new WebClient().DownloadString(U);
            string ide = matricula.Text;
            string asistencia = tasistencia.SelectedItem.ToString();
            Uri iA = new Uri(urlAsistencia + "Asistencia=" + asistencia + "&Materia=" + materia_id + "&Matricula=" + ide);
            string iAResult = new WebClient().DownloadString(iA);
            DisplayAlert("Exito", "Asistencia capturada", "OK");
        }

        private async void IndexPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IndexStudent());
        }
        private async void SchedulePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScheduleTeacher());
        }
        private async void WorksPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkTeacher());
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}