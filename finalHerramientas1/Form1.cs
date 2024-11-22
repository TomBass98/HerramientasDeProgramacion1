using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalHerramientas1
{
    public partial class Form1 : Form
    {
       
        private biblioteca biblioteca;
        private conexionSQLcs conexion;

        public Form1()
        {
            
            InitializeComponent();
            this.biblioteca = new biblioteca(new List<biblioteca.material>(), new List<biblioteca.persona>(), new List<biblioteca.movimiento>());
            this.conexion = new conexionSQLcs();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void boton_Click(object sender, EventArgs e)
        {
            conexion.AbrirConexion();
            conexion.CerrarConexion();
            
           

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Personas personas = new Personas();
            personas.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RegistrarMaterial registrar = new RegistrarMaterial();
            registrar.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Prestamos prestamo = new Prestamos();
            prestamo.Show();
            this.Hide();

        }
    }
}
