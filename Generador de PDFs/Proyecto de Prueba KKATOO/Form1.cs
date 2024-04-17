using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Threading;

namespace Proyecto_de_Prueba_KKATOO
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        DateTime Hoy = DateTime.Now;
        int Posicion = 200;
        int Posicionnombrearchivo = 50;

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static int raizhora(int raiz)
        {
            DateTime Hoy = DateTime.Now;
            string hora;
            int horas, minutos, segundos, total;
            hora = Hoy.ToString("H:mm:ss");
            string[] partes = hora.Split(':');
            horas = Int32.Parse(partes[0]);
            minutos = Int32.Parse(partes[1]);
            segundos = Int32.Parse(partes[2]);
            total = horas + minutos + segundos;
            //double raiz = System.Math.Sqrt(total);
            raiz = Convert.ToInt32(System.Math.Sqrt(total));
            return (raiz);
        }

        private void btngenerar_Click(object sender, EventArgs e)
        {

            int docPDF = Convert.ToInt32(txtgenerar.Text);
            if (txtgenerar.Text == "0")
            {
               MessageBox.Show("Debes ingresar un número mayor que cero (0)", "Ingresa datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
              else {


                for (int count = 1; count <= docPDF; ++count)
                {
                    string path = Path.GetRandomFileName();
                    string rutaPDF = Path.ChangeExtension(path, ".pdf");

                    Document documento = new Document();
                    PdfWriter.GetInstance(documento, new FileStream(rutaPDF, FileMode.Create));
                    try
                    {

                        documento.Open();
                        int parametro = 0;
                        int resultadoraiz = raizhora(parametro);
                        Phrase msg = new Phrase("La hora inicial es: ");
                        Phrase horainicial = new Phrase(Hoy.ToString("H:mm:ss"));
                        Phrase linea = new Phrase("\n\n");

                        documento.Add(msg);
                        documento.Add(horainicial);
                        documento.Add(linea);

                        Posicion = Posicion + 30;
                        Posicionnombrearchivo = Posicionnombrearchivo + 30; ;

                        Label lb = new Label();
                        lb.SetBounds(50, Posicion, 120, 30);
                        lb.Parent = this;
                        lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
                        lb.Text = "Archivo PDF";
                        lb.CreateControl();

                        ProgressBar pb = new ProgressBar();
                        pb.SetBounds(200, Posicion, 200, 23);
                        pb.Parent = this;
                        pb.CreateControl();
                            
                        pb.Minimum = resultadoraiz;
                        pb.Maximum = 100000;
                        pb.Step = 1;

                        for (double i = resultadoraiz; i < 100000; ++i)
                        {
                            Phrase p = new Phrase(Convert.ToString(i));
                            documento.Add(p);
                            pb.PerformStep();

                            //decimal porcentaje = (pb.Value / 100000) * 100;
                            //label2.Text = porcentaje.ToString() + "%";
                            //label2.Refresh();
                        }

                        Phrase linea2 = new Phrase("\n\n");
                        Phrase msg2 = new Phrase("La hora final es: ");
                        Phrase horafinal = new Phrase(Hoy.ToString("H:mm:ss"));
                        Phrase linea3 = new Phrase("\n\n");

                        documento.Add(linea2);
                        documento.Add(msg2);
                        documento.Add(horafinal);
                        documento.Add(linea3);

                        documento.Close();
                    }
                    catch (Exception EX)
                    { MessageBox.Show(EX.Message, "Exception Controlada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                }
                MessageBox.Show("Proceso terminado satisfactoriamente", "Estado de procesamiento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                label4.Visible = true;
                btndirectorio.Visible = true;
            }
        }

        private void txtgenerar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btndirectorio_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@".\");
        }

    }
}
