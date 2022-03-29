using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIEvE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewWorkStudent : ContentPage
    {
        public ViewWorkStudent()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
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

        public void loadinfo()
        {
            ttitulo.Text = WorkView.titulo;
            tfecha.Text = WorkView.fecha;
            thora.Text = WorkView.hora;
            tdescripcion.Text = WorkView.descripcion;
        }
    }
}