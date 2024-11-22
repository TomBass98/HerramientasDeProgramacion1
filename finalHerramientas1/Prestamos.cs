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
    public partial class Prestamos : Form
    {
        private biblioteca biblioteca;
        private conexionSQLcs conexion;
        public Prestamos()
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

        private void campoISBN_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int prestIsbn = int.Parse(campoISBN.Text);

            biblioteca.prestamo(prestIsbn);

            biblioteca.ConsultarFilaPorISBN(prestIsbn);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int prestIsbn = int.Parse(campoISBN.Text);
            biblioteca.Devolucion(prestIsbn);
            biblioteca.ConsultarFilaPorISBN(prestIsbn);
        }
    }
}
