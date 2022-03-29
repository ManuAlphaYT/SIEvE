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
    public partial class SendWorkStudent : ContentPage
    {
        string urlWork = "https://webmanualpha.000webhostapp.com/sieve/SendWork.php?";
        public SendWorkStudent()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            DateandTime();
            loadinfo();
        }

        private async void Regresar_Clicked(object sender, EventArgs e)
        {
            Regresar.Source = "regresarpress";

            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                Regresar.Source = "regresar";
                return false;
            });
            await Navigation.PushAsync(new WorkStudent());
            Navigation.RemovePage(this);
        }

        public void DateandTime()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Uri U = new Uri("https://webmanualpha.000webhostapp.com/sieve/DateandTime.php");
                string DandT = new WebClient().DownloadString(U);
                string[] dateandtime = DandT.Split(',');
                SendTime.Text = dateandtime[0];
                SendDate.Text = dateandtime[1];
                DandT = "";
                return true;
            });
        }

        public void loadinfo()
        {
            ttrabajo.Text = WorkView.titulo;
        }

        private void SendWork_Clicked(object sender, EventArgs e)
        {
            string all = tcontenido.Text;
            Uri sW = new Uri(urlWork + "Contenido=" + all + "&Hora=" + SendTime.Text + "&Fecha=" + SendDate.Text + "&Id=" + WorkView.id + "&Matricula=" + DataUsers.Matricula);
            string sWResult = new WebClient().DownloadString(sW);

            if(sWResult == "Registro exitoso")
            {
                DisplayAlert("Exitoso", "La tarea fue enviada", "OK");
                tcontenido.Text = "";
                ttrabajo.Text = "";
            }
        }
    }
}