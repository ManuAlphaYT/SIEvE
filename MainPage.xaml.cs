using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SIEvE
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        string urlStudent = "https://proyectosalpha.000webhostapp.com/SIEvE/SessionStudent.php?";
        string urlTeacher = "https://proyectosalpha.000webhostapp.com/SIEvE/SessionTeacher.php?";
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            WelcomeMessage();
        }
        public async void WelcomeMessage()
        {
           await DisplayAlert("Bienvendido(a)", "Bienvenido(a) a SIEvE la aplicacion de evaluación de estudiantes", "OK");   
        }
        private async void SessionStart_Clicked(object sender, EventArgs e)
        {

            
            string id = Matricula.Text;
            string ps = Contrasena.Text;
            Uri uS = new Uri(urlStudent + "Matricula=" + id + "&Contrasena=" + ps);
            string student = new WebClient().DownloadString(uS);
            string[] student_div = student.Split(',');
            Uri uT = new Uri(urlTeacher + "Matricula=" + id + "&Contrasena=" + ps);
            string teacher = new WebClient().DownloadString(uT);
            string[] teacher_div = teacher.Split(',');
            DataUsers.Matricula = id;
            if(student_div[1] == "Activo")
            {
                await Navigation.PushAsync(new IndexStudent());
                Navigation.RemovePage(this);
            }

            if(teacher_div[1] == "Activo")
            {
                await Navigation.PushAsync(new IndexTeacher());
                Navigation.RemovePage(this);
            }

            if (student_div[1] == "Baja" || teacher_div[1] == "Baja")
            {
                await DisplayAlert("Sin acceso", "Tu cuenta se encuentra dada de baja", "OK");
            }
            else if (student == "Contrasena Invalida" || teacher == "Contrasena Invalida")
            {
                await DisplayAlert("Error", "Contraseña Invalida", "OK");
            }
            else if (student == "Matricula Invalida" && teacher == "Matricula Invalida")
            {
                await DisplayAlert("Error", "Esta identificacion no esta registrada", "OK");
            }
        }

        private async void RegisterUsers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterUsers());
            Navigation.RemovePage(this);
        }
    }
}
