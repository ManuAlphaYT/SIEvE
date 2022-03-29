using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SIEvE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUsers : ContentPage
    {
        string urlStudent = "https://proyectosalpha.000webhostapp.com/SIEvE/InsertStudent.php?";
        string urlTeacher = "https://proyectosalpha.000webhostapp.com/SIEvE/InsertTeacher.php?";
        string urlMedica = "https://proyectosalpha.000webhostapp.com/SIEvE/InsertMedica.php?";
        string urlContacto = "https://proyectosalpha.000webhostapp.com/SIEvE/InsertContacto.php?";
        string urlCalificaciones = "https://proyectosalpha.000webhostapp.com/SIEvE/InsertCalificaciones.php?";
        string urlCodigo = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectCodeAccess.php?";
        string urlMatriculaS = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectMatriculaStudent.php?";
        string urlMatriculaT = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectMatriculaTeacher.php?";
        string urlMaterias = "https://proyectosalpha.000webhostapp.com/SIEvE/SelectCalificaciones.php?";
        string urlMateria = "https://proyectosalpha.000webhostapp.com/SIEvE/UpdateMaterias.php?";
        string urlCodigoUpdate = "https://proyectosalpha.000webhostapp.com/SIEvE/UpdateCodeAccess.php?";
        string alergias,contraseña, iCFResult, materia,alergiac;
        string organos;
        string medicina;
        string medicinac;
        string emergencia;
        string correo;
        string celular;
        string telefono;
        string nombre;
        string apellidop;
        string apellidom;
        string nacimineto;
        string edad;
        string grupo;
        string sangre,matricula;
        public RegisterUsers()
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
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }

        private async void ShowSubject_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowSubjects());
        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            string cr = CodigoRegistro.Text;
            Uri sC = new Uri(urlCodigo + "Codigo=" + cr);
            string answersC = new WebClient().DownloadString(sC);
            string[] answersC_div = answersC.Split(',');
            if (answersC != "Codigo Invalido")
            {
                if (answersC_div[1] == "Activo")
                {
                    Random R = new Random();
                    matricula = R.Next(100000, 999999).ToString();

                    Uri smS = new Uri(urlMatriculaS + "Matricula=" + matricula);
                    string smSResult = new WebClient().DownloadString(smS);
                    string[] smSResult_div = smSResult.Split(',');
                    Uri smT = new Uri(urlMatriculaT + "Matricula=" + matricula);
                    string smTResult = new WebClient().DownloadString(smT);
                    string[] smTResult_div = smSResult.Split(',');

                    while (smSResult_div[1] == "Si existe" || smTResult_div[1] == "Si existe")
                    {
                        matricula = R.Next(100000, 999999).ToString();

                        smS = new Uri(urlMatriculaS + "Matricula=" + matricula);
                        smSResult = new WebClient().DownloadString(smS);
                        smSResult_div = smSResult.Split(',');
                        smT = new Uri(urlMatriculaT + "Matricula=" + matricula);
                        smTResult = new WebClient().DownloadString(smT);
                        smTResult_div = smSResult.Split(',');
                    }

                    if (smSResult_div[1] == "No existe" && smTResult_div[1] == "No existe")
                    {
                        nombre = Nombre.Text;
                        apellidop = APaterno.Text;
                        apellidom = AMaterno.Text;
                        nacimineto = Nacimiento.Date.ToShortDateString().ToString();
                        edad = Edad.Text;
                        sangre = Sangre.SelectedItem.ToString();
                        if (AlergiasSI.IsChecked)
                        {
                            alergias = "Si";
                        }
                        else if (AlergiasNO.IsChecked)
                        {
                            alergias = "No";
                        }
                        if(AlergiasC.IsVisible)
                        {
                            alergiac = ", " + AlergiasC.Text;
                        }
                        else
                        {
                            alergiac = "";
                        }
                        if (OrganosSI.IsChecked)
                        {
                            organos = "Si";
                        }
                        else if (OrganosNO.IsChecked)
                        {
                            organos = "No";
                        }
                        if (MedicinasSI.IsChecked)
                        {
                            medicina = "Si";
                        }
                        else if (MedicinasNO.IsChecked)
                        {
                            medicina = "No";
                        }
                        if (MedicinasC.IsVisible)
                        {
                            medicinac = ", " + MedicinasC.Text;
                        }
                        else
                        {
                            medicinac = "";
                        }
                        emergencia = Emergencia.Text;
                        correo = Correo.Text;
                        celular = Celular.Text;
                        telefono = Telefono.Text;
                        materia = Materias.Text;
                        String[] mat_div = materia.Split(',');
                        contraseña = Convert.ToString((char)R.Next('A', 'Z')) + Convert.ToString((char)R.Next('A', 'Z')) + R.Next(0, 9).ToString() + Convert.ToString((char)R.Next('A', 'Z')) + Convert.ToString((char)R.Next('A', 'Z')) + R.Next(0, 9).ToString();


                        Uri iS = new Uri(urlTeacher + "Matricula=" + matricula + "&Contrasena=" + contraseña + "&Nombre=" + nombre + "&Apellidop=" + apellidop + "&Apellidom=" + apellidom + "&Nacimiento=" + nacimineto + "&Edad=" + edad);
                        string iSResult = new WebClient().DownloadString(iS);
                        Uri iM = new Uri(urlMedica + "Sangre=" + sangre + "&Alergia=" + alergias + alergiac + "&Donante=" + organos + "&Medicina=" + medicina + medicinac + "&Emergencia=" + emergencia + "&Matricula=" + matricula);
                        string iMResult = new WebClient().DownloadString(iM);
                        Uri iC = new Uri(urlContacto + "Correo=" + correo + "&Celular=" + celular + "&Telefono=" + telefono + "&Matricula=" + matricula);
                        string iCResult = new WebClient().DownloadString(iC);


                        for (int i = 0; i < mat_div.Length; i++)
                        {
                            Uri uM = new Uri(urlMateria + "Materia=" + mat_div[i] + "&Matricula=" + matricula);
                            string uMResult = new WebClient().DownloadString(uM);
                        }

                        if (iSResult == "Registro exitoso" && iMResult == "Registro exitoso" && iCResult == "Registro exitoso")
                        {
                            DisplayAlert("Registro exitoso", "Se a mandado a su correo su matricula y contraseña", "OK");

                            Uri uC = new Uri(urlCodigoUpdate + "Codigo=" + cr);
                            string uCResult = new WebClient().DownloadString(uC);

                            MailMessage msg = new MailMessage();
                            msg.To.Add(correo);
                            msg.Subject = "Registro de usuario";
                            msg.SubjectEncoding = Encoding.UTF8;

                            msg.Body = "<html>";
                            msg.Body += "<head>";
                            msg.Body += "<body>";
                            msg.Body += "<h1>Su registro ha sido exitoso " + nombre + " " + apellidop + " " + apellidom + "</h1><br>";
                            msg.Body += "Recuerde no compartir su matricula ni contraseña<br>";
                            msg.Body += "Se a registrado como: <b>Docente</b><br>";
                            msg.Body += "<b>Matricula = <b>" + matricula + "</b><br>";
                            msg.Body += "Contrase&ntilde;a = <b>" + contraseña + "</b>";
                            msg.Body += "</body>";
                            msg.Body += "</head>";
                            msg.Body += "<html>";
                            msg.BodyEncoding = Encoding.UTF8;
                            msg.IsBodyHtml = true;
                            msg.From = new MailAddress("contactosieve@gmail.com", "SIEvE");
                            SmtpClient cliente = new SmtpClient("smtp.gmail.com", 587);
                            cliente.EnableSsl = true;
                            NetworkCredential credentials = new NetworkCredential("contactosieve@gmail.com", "al18311477");
                            cliente.Credentials = credentials;
                            try
                            {
                                cliente.Send(msg);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            matricula = "";
                            contraseña = "";

                            CodigoRegistro.Text = "";
                            Nombre.Text = "";
                            APaterno.Text = "";
                            AMaterno.Text = "";
                            Nacimiento.Date = new DateTime(1999,01,01);
                            Edad.Text = "";
                            Grupo.SelectedItem = "";
                            Sangre.SelectedItem = "";
                            AlergiasNO.IsChecked = false;
                            AlergiasSI.IsChecked = false;
                            AlergiasC.Text = "";
                            OrganosNO.IsChecked = false;
                            OrganosSI.IsChecked = false;
                            MedicinasNO.IsChecked = false;
                            MedicinasSI.IsChecked = false;
                            MedicinasC.Text = "";
                            Emergencia.Text = "";
                            Correo.Text = "";
                            Celular.Text = "";
                            Telefono.Text = "";

                        }
                        else
                        {
                            DisplayAlert("Registro no exitoso", "Registro fallido, Revise la informacion y vuelva a intentarlo", "OK");
                        }
                    }
                }
                else if (answersC_div[1] == "Usado")
                {
                    DisplayAlert("Error", "Este código de registro ya a sido usado", "OK");
                }
            }
        }

        private void CodigoRegistro_TextChanged(object sender, TextChangedEventArgs e)
        {
            string cr = CodigoRegistro.Text;
            Uri sC = new Uri(urlCodigo + "Codigo=" + cr);
            string answersC = new WebClient().DownloadString(sC);
            string[] answersC_div = answersC.Split(',');
           
            if (answersC != "Codigo Invalido")
            {
                if (answersC_div[1]=="Activo")
                {
                    if (answersC_div[2]=="Estudiante")
                    {
                        FramePersonal.IsVisible = true;
                        Personal.IsVisible = true;
                        FNacimiento.IsVisible = true;
                        Nombre.IsVisible = true;
                        APaterno.IsVisible = true;
                        AMaterno.IsVisible = true;
                        Nacimiento.IsVisible = true;
                        Edad.IsVisible = true;
                        Grupo.IsVisible = true;

                        FrameMedica.IsVisible = true;
                        Alergias.IsVisible = true;
                        ASI.IsVisible = true;
                        ANO.IsVisible = true;
                        Organos.IsVisible = true;
                        OSI.IsVisible = true;
                        ONO.IsVisible = true;
                        Medicinas.IsVisible = true;
                        MSI.IsVisible = true;
                        MNO.IsVisible = true;
                        Sangre.IsVisible = true;
                        AlergiasNO.IsVisible = true;
                        AlergiasSI.IsVisible = true;
                        AlergiasC.IsVisible = true;
                        MedicinasC.IsVisible = true;
                        OrganosNO.IsVisible = true;
                        OrganosSI.IsVisible = true;
                        MedicinasNO.IsVisible = true;
                        MedicinasSI.IsVisible = true;
                        
                        Emergencia.IsVisible = true;

                        FrameContacto.IsVisible = true;
                        Contaco.IsVisible = true;
                        Correo.IsVisible = true;
                        Celular.IsVisible = true;
                        Telefono.IsVisible = true;

                        RegisterE.IsVisible = true;

                        FrameMaterias.IsVisible = false;
                        Materia.IsVisible = false;
                        Materias.IsVisible = false;
                        ShowSubject.IsVisible = false;
                        Register.IsVisible = false;

                        DisplayAlert("Validación","Código de registro validado: Estudiante", "OK");
                        
                    }
                    else if(answersC_div[2]=="Docente")
                    {
                        FramePersonal.IsVisible = true;
                        Personal.IsVisible = true;
                        FNacimiento.IsVisible = true;
                        Nombre.IsVisible = true;
                        APaterno.IsVisible = true;
                        AMaterno.IsVisible = true;
                        Nacimiento.IsVisible = true;
                        Edad.IsVisible = true;
                        Grupo.IsVisible = false;

                        FrameMedica.IsVisible = true;
                        Alergias.IsVisible = true;
                        ASI.IsVisible = true;
                        ANO.IsVisible = true;
                        Organos.IsVisible = true;
                        OSI.IsVisible = true;
                        ONO.IsVisible = true;
                        Medicinas.IsVisible = true;
                        MSI.IsVisible = true;
                        MNO.IsVisible = true;
                        Sangre.IsVisible = true;
                        AlergiasNO.IsVisible = true;
                        AlergiasSI.IsVisible = true;
                        OrganosNO.IsVisible = true;
                        OrganosSI.IsVisible = true;
                        MedicinasNO.IsVisible = true;
                        MedicinasSI.IsVisible = true;
                        Emergencia.IsVisible = true;

                        AlergiasC.IsVisible = true;
                        MedicinasC.IsVisible = true;

                        FrameContacto.IsVisible = true;
                        Contaco.IsVisible = true;
                        Correo.IsVisible = true;
                        Celular.IsVisible = true;
                        Telefono.IsVisible = true;

                        RegisterE.IsVisible = false;

                        FrameMaterias.IsVisible = true;
                        Materia.IsVisible = true;
                        Materias.IsVisible = true;
                        ShowSubject.IsVisible = true;
                        Register.IsVisible = true;

                        DisplayAlert("Validación", "Código de registro validado: Docente", "OK");
                    }
                }
                else if(answersC_div[1]=="Usado")
                {
                    DisplayAlert("Error", "Este código de registro ya a sido usado", "OK");
                }
            }
        }

        private void RegisterE_Clicked(object sender, EventArgs e)
        {
            
            string cr = CodigoRegistro.Text;
            Uri sC = new Uri(urlCodigo + "Codigo=" + cr);
            string answersC = new WebClient().DownloadString(sC);
            string[] answersC_div = answersC.Split(',');
            if (answersC != "Codigo Invalido")
            {
                if (answersC_div[1] == "Activo")
                {
                        Random R = new Random();
                        matricula = R.Next(100000, 999999).ToString();
                        
                        Uri smS = new Uri(urlMatriculaS + "Matricula=" + matricula);
                        string smSResult = new WebClient().DownloadString(smS);
                        string[] smSResult_div = smSResult.Split(',');
                        Uri smT = new Uri(urlMatriculaT + "Matricula=" + matricula);
                        string smTResult = new WebClient().DownloadString(smT);
                        string[] smTResult_div = smTResult.Split(',');
                        

                    while (smSResult_div[1] == "Si existe" || smTResult_div[1] == "Si existe")
                        {
                            matricula = R.Next(100000, 999999).ToString();

                            smS = new Uri(urlMatriculaS + "Matricula=" + matricula);
                            smSResult = new WebClient().DownloadString(smS);
                            smSResult_div = smSResult.Split(',');
                            smT = new Uri(urlMatriculaT + "Matricula=" + matricula);
                            smTResult = new WebClient().DownloadString(smT);
                            smTResult_div = smTResult.Split(',');
                        }

                        if (smSResult_div[1] == "No existe" && smTResult_div[1] == "No existe")
                        {
                            nombre = Nombre.Text;
                            apellidop = APaterno.Text;
                            apellidom = AMaterno.Text;
                            nacimineto = Nacimiento.Date.ToShortDateString().ToString();
                            edad = Edad.Text;
                            grupo = Grupo.SelectedItem.ToString();
                            sangre = Sangre.SelectedItem.ToString();
                            if (AlergiasSI.IsChecked)
                            {
                                alergias = "Si";
                            }
                            else if (AlergiasNO.IsChecked)
                            {
                                alergias = "No";
                            }
                            if (AlergiasC.IsVisible)
                            {
                            alergiac = ", " + AlergiasC.Text;
                            }
                            else
                            {
                            alergiac = "";
                            }
                            if (OrganosSI.IsChecked)
                            {
                                organos = "Si";
                            }
                            else if (OrganosNO.IsChecked)
                            {
                                organos = "No";
                            }
                            if (MedicinasSI.IsChecked)
                            {
                                medicina = "Si";
                            }
                            else if (MedicinasNO.IsChecked)
                            {
                                medicina = "No";
                            }
                            if(MedicinasC.IsVisible)
                            {
                            medicinac = ", " + MedicinasC.Text;
                            }
                            else
                            {
                            medicinac = "";
                            }
                            emergencia = Emergencia.Text;
                            correo = Correo.Text;
                            celular = Celular.Text;
                            telefono = Telefono.Text;
                            
                            contraseña = Convert.ToString((char)R.Next('A', 'Z')) + Convert.ToString((char)R.Next('A', 'Z')) + R.Next(0, 9).ToString() + Convert.ToString((char)R.Next('A', 'Z')) + Convert.ToString((char)R.Next('A', 'Z')) + R.Next(0, 9).ToString();


                            Uri iS = new Uri(urlStudent + "Matricula=" + matricula + "&Contrasena=" + contraseña + "&Nombre=" + nombre + "&Apellidop=" + apellidop + "&Apellidom=" + apellidom + "&Nacimiento=" + nacimineto + "&Edad=" + edad + "&Grupo=" + grupo);
                            string iSResult = new WebClient().DownloadString(iS);
                            Uri iM = new Uri(urlMedica + "Sangre=" + sangre + "&Alergia=" + alergias + alergiac + "&Donante=" + organos + "&Medicina=" + medicina + medicinac + "&Emergencia=" + emergencia + "&Matricula=" + matricula);
                            string iMResult = new WebClient().DownloadString(iM);
                            Uri iC = new Uri(urlContacto + "Correo=" + correo + "&Celular=" + celular + "&Telefono=" + telefono + "&Matricula=" + matricula);
                            string iCResult = new WebClient().DownloadString(iC);
                            Uri sM = new Uri(urlMaterias + "Grupo=" + grupo);
                            string sMResult = new WebClient().DownloadString(sM);
                            string[] sM_div = sMResult.Split(',');
                            int c = 1;
                            for (int i = 1; i < sM_div.Length; i = i + 2)
                            {
                                Uri iCF = new Uri(urlCalificaciones + "Materia=" + sM_div[c] + "&Matricula=" + matricula);
                                iCFResult = new WebClient().DownloadString(iCF);
                            c = c+2;
                            }
                        
                            if (iSResult == "Registro exitoso" && iMResult == "Registro exitoso" && iCResult == "Registro exitoso" && iCFResult == "Registro exitoso")
                            {
                                DisplayAlert("Registro exitoso", "Se a mandado a su correo su matricula y contraseña", "OK");

                                Uri uC = new Uri(urlCodigoUpdate + "Codigo=" + cr);
                                string uCResult = new WebClient().DownloadString(uC);

                                MailMessage msg = new MailMessage();
                                msg.To.Add(correo);
                                msg.Subject = "Registro de usuario";
                                msg.SubjectEncoding = Encoding.UTF8;

                                msg.Body = "<html>";
                                msg.Body += "<head>";
                                msg.Body += "<body>";
                                msg.Body += "<h1>Su registro ha sido exitoso " + nombre + " " + apellidop + " " + apellidom + "</h1><br>";
                                msg.Body += "Recuerde no compartir su matricula ni contraseña<br>";
                                msg.Body += "Se a registrado como: <b>Estudiante</b><br>";
                                msg.Body += "<b>Matricula = <b>" + matricula + "</b><br>";
                                msg.Body += "Contrase&ntilde;a = <b>" + contraseña + "</b>";
                                msg.Body += "</body>";
                                msg.Body += "</head>";
                                msg.Body += "<html>";
                                msg.BodyEncoding = Encoding.UTF8;
                                msg.IsBodyHtml = true;
                                msg.From = new MailAddress("contactosieve@gmail.com", "SIEvE");
                                SmtpClient cliente = new SmtpClient("smtp.gmail.com", 587);
                                cliente.EnableSsl = true;
                                NetworkCredential credentials = new NetworkCredential("contactosieve@gmail.com", "al18311477");
                                cliente.Credentials = credentials;
                                try
                                {
                                    cliente.Send(msg);
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                matricula = "";
                                contraseña = "";

                                CodigoRegistro.Text = "";
                                Nombre.Text = "";
                                APaterno.Text = "";
                                AMaterno.Text = "";
                                Nacimiento.Date = new DateTime(1999, 01, 01);
                                Edad.Text = "";
                                Sangre.SelectedItem = "";
                                AlergiasNO.IsChecked = false;
                                AlergiasSI.IsChecked = false;
                                AlergiasC.Text = "";
                                OrganosNO.IsChecked = false;
                                OrganosSI.IsChecked = false;
                                MedicinasNO.IsChecked = false;
                                MedicinasSI.IsChecked = false;
                                MedicinasC.Text = "";
                                Emergencia.Text = "";
                                Correo.Text = "";
                                Celular.Text = "";
                                Telefono.Text = "";
                                Materias.Text = "";
                            }
                            else
                            {
                            DisplayAlert("Registro no exitoso", "Registro fallido", "OK");
                            }

                        }
                    
                }
                else if (answersC_div[1] == "Usado")
                {
                    DisplayAlert("Error", "Este código de registro ya a sido usado", "OK");
                }
            }
        }
    }
}