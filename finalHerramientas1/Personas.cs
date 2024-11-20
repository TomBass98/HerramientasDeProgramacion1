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
    public partial class Personas : Form
    {
        private biblioteca biblioteca;
        private conexionSQLcs conexion;
        public Personas()
        {
            InitializeComponent();
            this.biblioteca = new biblioteca(new List<material>(), new List<persona>(), new List<movimiento>());
            this.conexion = new conexionSQLcs();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.ToLower();
            int cc = int.Parse(txtCC.Text);
            string rol = txtRol.Text.ToLower();
            int cantMaxPrest = 0;
            switch (rol)
            {
                case "estudiante":
                    cantMaxPrest = 5;
                    break;
                case "profesor":
                    cantMaxPrest = 3;
                    break;
                case "administrativo":
                    cantMaxPrest = 1;
                    break;
                default:
                    MessageBox.Show("Rol no válido. Los roles permitidos son: Estudiante, Profesor, Administrativo.");
                    return;
            }


            biblioteca.registrarPersona(new persona(nombre, cc, rol, cantMaxPrest));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int cc = int.Parse(txtCC.Text);
           
            biblioteca.eliminarPersona(cc);
        }
    }
}
