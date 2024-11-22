namespace finalHerramientas1
{
    partial class RegistrarMaterial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.campoNombre = new System.Windows.Forms.TextBox();
            this.campoTipo = new System.Windows.Forms.TextBox();
            this.campoCNueva = new System.Windows.Forms.TextBox();
            this.campoISBN = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(206, 313);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 56);
            this.button2.TabIndex = 13;
            this.button2.Text = "Menú principal";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(44, 326);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 30);
            this.button1.TabIndex = 14;
            this.button1.Text = "enviar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "isbn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "Nombre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "La fecha de registro será la fecha actual";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(236, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "Cantidad nueva";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Tipo de material";
            // 
            // campoNombre
            // 
            this.campoNombre.Location = new System.Drawing.Point(44, 117);
            this.campoNombre.Name = "campoNombre";
            this.campoNombre.Size = new System.Drawing.Size(100, 22);
            this.campoNombre.TabIndex = 22;
            // 
            // campoTipo
            // 
            this.campoTipo.Location = new System.Drawing.Point(44, 174);
            this.campoTipo.Name = "campoTipo";
            this.campoTipo.Size = new System.Drawing.Size(100, 22);
            this.campoTipo.TabIndex = 23;
            // 
            // campoCNueva
            // 
            this.campoCNueva.Location = new System.Drawing.Point(239, 47);
            this.campoCNueva.Name = "campoCNueva";
            this.campoCNueva.Size = new System.Drawing.Size(100, 22);
            this.campoCNueva.TabIndex = 25;
            // 
            // campoISBN
            // 
            this.campoISBN.Location = new System.Drawing.Point(44, 47);
            this.campoISBN.Name = "campoISBN";
            this.campoISBN.Size = new System.Drawing.Size(100, 22);
            this.campoISBN.TabIndex = 27;
            // 
            // RegistrarMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.campoISBN);
            this.Controls.Add(this.campoCNueva);
            this.Controls.Add(this.campoTipo);
            this.Controls.Add(this.campoNombre);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Name = "RegistrarMaterial";
            this.Text = "RegistrarMaterial";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox campoNombre;
        private System.Windows.Forms.TextBox campoTipo;
        private System.Windows.Forms.TextBox campoCNueva;
        private System.Windows.Forms.TextBox campoISBN;
    }
}