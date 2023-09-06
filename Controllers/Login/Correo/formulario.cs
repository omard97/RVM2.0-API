using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApiRVM2019.Controllers.Login.Correo
{
    public class formulario
    {
        public static void Email(string correo, string contrasenia, string nombre, string usuario)
        {
            string EmailOrigen = "VillaMariaRVM@hotmail.com";
            string Contraseña = "Felipe098";
            string EmailDestino = correo;

            string mensaje = @"
        <html>
        
        <body>
            <main class='main'>
                
                    <div class='main'>
                         <p><span>Hola " + nombre + " , gracias por crear tu cuenta en RVM.</span></p>" +
                        "<p><span>Tu nombre de usuario: " + usuario + "<p></span></p> " +
                        "<p><span>Tu contraseña: " + contrasenia + "</span></p> " +
                       
                   "</div>"+
            "</main>" +
        "</body>" +
       "</html>";
    
           

            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Bienvenido a RVM!!", mensaje);

            // Adjuntar la imagen
            //Attachment attachment = new Attachment(@"C:\Users\omard\Desktop\ApiRVM\Assets\rmv-black.png");
            //oMailMessage.Attachments.Add(attachment);

            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp-mail.outlook.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Host = "smtp-mail.outlook.com";
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new NetworkCredential(EmailOrigen, Contraseña);

            try
            {
                oSmtpClient.Send(oMailMessage);
                Console.WriteLine("Correo enviado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
            finally
            {
                oSmtpClient.Dispose();
            }


        }
    }
}
