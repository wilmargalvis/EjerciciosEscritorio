using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail; // Para correos
using System.Net; // Para correos
using System.IO; //Para archivos secuenciales

namespace Envio_de_correo
{
    public partial class Form1 : Form
    {

        //Variables para correo
        string usuario;
        string contraseña;
        string server_smtp;
        string puerto;
        string Email_html1;
        string Email_html2;
        string Email_html3;
        string Email_html4;
        string Email_html5;
        string Email_html6;
        string Email_html7;
        string Email_html8;
        string tipo_usuario;
        string usuario_sesion;
        string hora_inicio_tarea;

        //Objeto que contiene el texto con los impuestos que serán enviados al correo
        TextBox Obj_textnotas = new TextBox();

        string Email_alerta1 = "wilmargalvis@gmail.com";
        string Email_alerta2="wilmar.galvis@nuva.co";        

        public Form1()
        {
            InitializeComponent();
        }

        private void Enviar_correo_Click(object sender, EventArgs e)
        {

            try
            {
                MailMessage Correo = new MailMessage();
                Correo.From = new MailAddress(usuario);

                if (Email_alerta2 != "")            //Direccion de correo electronico que queremos que reciba una copia del mensaje
                {
                    try
                    {

                        Correo.Bcc.Add(Email_alerta2); //Dirección de correo de copia (Opcional)
                    }
                    catch (Exception Ex)
                    {
                        //MessageBox.Show(Ex.Message + "Error de envío al segundo correo registrado", "Error de envío", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                try
                {
                    Correo.To.Add(Email_alerta1); //Direccion de correo destino. Nota: La propiedad To, puede ser una colección
                }
                catch (Exception Ex)
                {
                    //MessageBox.Show(Ex.Message + "Error de envío al primer correo registrado", "Error de envío", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                Correo.Subject = "Alerta vencimiento de impuestos"; //Asunto
                Correo.SubjectEncoding = System.Text.Encoding.UTF8;

                //Cuerpo del Mensaje

                Correo.IsBodyHtml = true;
                Correo.BodyEncoding = System.Text.Encoding.UTF8;

                Obj_textnotas.Clear();
                Obj_textnotas.AppendText("Cuerpo del mensaje de correo");
                Correo.Body = Obj_textnotas.Text;
                //Correo.IsBodyHtml = false; //Si no queremos que se envíe como HTML...            
                Correo.Priority = MailPriority.Normal;

                ////Correo electronico desde la que enviamos el mensaje
                //Correo.From = new System.Net.Mail.MailAddress("micuenta@servidordominio.com");

                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(usuario, contraseña); // usuario y contraseña el correo origen
                smtp.Host = server_smtp;
                smtp.Port = Convert.ToInt16(puerto);
                smtp.EnableSsl = true;

                try
                {

                    smtp.Send(Correo);
                    //MessageBox.Show("Correo enviado satisfactoriamente", "Envío de correo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (System.Net.Mail.SmtpException Ex_smtp)
                {
                    //Aquí gestionamos los errores al intentar enviar el correo
                    MessageBox.Show(Ex_smtp.Message + "Error de envío SMTP", "Error de envío de correo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + "Error general de envío de correo", "Error de envío de correo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //Correo.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                if (File.Exists("file_smtp.dll"))
                {
                    StreamReader reader = new System.IO.StreamReader("file_smtp.dll"); //linea 1
                    //StreamReader reader = new System.IO.StreamReader(@"\\192.168.1.200\server\info\ASIEL\file_smtp.dll"); //linea 1
                    //StreamReader reader = new System.IO.StreamReader(@"C:\file_smtp.dll"); //linea 1
                    while (!reader.EndOfStream)
                    {
                        usuario = reader.ReadLine();
                        contraseña = reader.ReadLine();
                        server_smtp = reader.ReadLine();
                        puerto = reader.ReadLine();
                        Email_html1 = reader.ReadLine();
                        Email_html2 = reader.ReadLine();
                        Email_html3 = reader.ReadLine();
                        Email_html4 = reader.ReadLine();
                        Email_html5 = reader.ReadLine();
                        Email_html6 = reader.ReadLine();
                        Email_html7 = reader.ReadLine();
                        Email_html8 = reader.ReadLine();
                        //Console.WriteLine(read);
                        //read = reader.ReadLine();
                    }
                    reader.Close();
                }

            }
            catch (Exception EX) // Evita salirse del programa, si no puede abrir la DB o si encuentra errores de acceso.
            { MessageBox.Show(EX.Message, "Error en la lectura de los datos de correo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }
    }
}
