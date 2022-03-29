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
    public partial class GradesStudent : ContentPage
    {
        string urlMaterias = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectStudentMaterias.php?";
        string urlMateriasId = "https://webmanualpha.000webhostapp.com/sieve/SelectIdMaterias.php?";
        string urlCalificaciones = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectAllCalificaciones.php?";
        string urlMateriaTeacher = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectTeacherMaterias.php?";
        string urlTeacher = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectAllTeacher.php?";
        string urlPromedio = "https://webmanualpha.000webhostapp.com/sieve/SelectPromedio.php?";
        public GradesStudent()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            LoadInformation();
            
        }
        private async void SchedulePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScheduleStudent());   
        }
        private async void IndexPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IndexStudent());
        }
        private async void WorksPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkStudent());
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        public void LoadInformation()
        {
            Uri sM = new Uri(urlMaterias + "Matricula=" + DataUsers.Matricula);
            string sMResult = new WebClient().DownloadString(sM);
            string[] sMR_div = sMResult.Split(',');
            int c = 1;
            List<string> subjects = new List<string>();
            for(int i = 1;i<sMR_div.Length;i++)
            {
                subjects.Add(sMR_div[c]);
                c++;
            }
            Materias.ItemsSource = subjects;
            grupo.Text = DataUsers.Grupo;
        }

        private void Materias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string subject = Materias.SelectedItem.ToString();
            
            Uri sC = new Uri(urlCalificaciones + "Materia=" + subject);
            string sCResult = new WebClient().DownloadString(sC);
            string[] sCR_div = sCResult.Split(',');
            
            Uri sMT = new Uri(urlMateriaTeacher + "Materia=" + subject);
            string sMTResult = new WebClient().DownloadString(sMT);
            string[] sMTResult_div = sMTResult.Split(',');
            
            
            Uri sT = new Uri(urlTeacher + "Matricula=" + sMTResult_div[1]);
            string sTResult = new WebClient().DownloadString(sT);
            string[] sTR_div = sTResult.Split(',');
            
            
            
            promedio.Text = "0";
            
            materia.Text = subject;

            
            docente.Text = sTR_div[2] + " " + sTR_div[3] + " " + sTR_div[4];
            

            
            if(sCR_div[1]=="0")
            {
                unidad1.Text = "NC";
            }
            else
            {
                unidad1.Text = sCR_div[1];
            }

            if (sCR_div[2] == "0")
            {
                unidad2.Text = "NC";
            }
            else
            {
                unidad2.Text = sCR_div[2];
            }

            if (sCR_div[3] == "0")
            {
                unidad3.Text = "NC";
            }
            else
            {
                unidad3.Text = sCR_div[3];
            }

            promediom.Text = sCR_div[4];
        }
    }
}