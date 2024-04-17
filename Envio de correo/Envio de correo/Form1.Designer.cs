namespace Envio_de_correo
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Enviar_correo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Enviar_correo
            // 
            this.Enviar_correo.Location = new System.Drawing.Point(176, 41);
            this.Enviar_correo.Name = "Enviar_correo";
            this.Enviar_correo.Size = new System.Drawing.Size(96, 41);
            this.Enviar_correo.TabIndex = 0;
            this.Enviar_correo.Text = "Enviar Correo";
            this.Enviar_correo.UseVisualStyleBackColor = true;
            this.Enviar_correo.Click += new System.EventHandler(this.Enviar_correo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 169);
            this.Controls.Add(this.Enviar_correo);
            this.Name = "Form1";
            this.Text = "Envío de Correo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Enviar_correo;
    }
}

