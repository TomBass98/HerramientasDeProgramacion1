using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalHerramientas1
{
    public partial class RegistrarMaterial : Form
    {
        private biblioteca biblioteca;
        private conexionSQLcs conexion;
        public RegistrarMaterial()
        {
            InitializeComponent();
            
            this.biblioteca = new biblioteca(new List<biblioteca.material>(), new List<biblioteca.persona>(), new List<biblioteca.movimiento>());
            this.conexion = new conexionSQLcs();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(campoNombre.Text);
            try {
                int isbn = Convert.ToInt32(campoISBN.Text); // la excepción salta aquí
                string nombre = campoNombre.Text;
                string tipo = campoTipo.Text;
                int cantidadRegistrada = int.Parse(campoCNueva.Text);

                biblioteca.registrarMaterial(new biblioteca.material(isbn, nombre,  cantidadRegistrada, cantidadRegistrada, tipo));
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}