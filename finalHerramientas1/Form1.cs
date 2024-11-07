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
    public partial class Form1 : Form
    {
        private biblioteca biblioteca;
        private conexionSQLcs conexion;

        public Form1()
        {
            InitializeComponent();
            this.biblioteca = new biblioteca(new material[20], new persona[20], new movimiento[20]);
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
    }
}
