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
            List<material> catalogo = new List<material>();
            List<persona> registroPersonas = new List<persona>();
            List<movimiento> movimientos = new List<movimiento>();

            InitializeComponent();
            this.biblioteca = new biblioteca(catalogo, registroPersonas, movimientos);
            this.conexion = new conexionSQLcs();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void boton_Click(object sender, EventArgs e)
        {
            biblioteca.agregarMaterial(new material(1234, "Libro Ejemplo", DateTime.Now, 10, 5));
            conexion.AbrirConexion();
            conexion.CerrarConexion();
        }
        

    }
}
