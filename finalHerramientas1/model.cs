using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace finalHerramientas1
{
    
    public class biblioteca
    {
        private List<material> catalogo;
        private List<persona> registroPersonas;
        private List<movimiento> movimientos;
        
        public biblioteca(List<material> catalogo, List<persona> registroPersonas, List<movimiento> movimientos)
        {
            this.Catalogo = catalogo;
            this.RegistroPersonas = registroPersonas;
            this.Movimientos = movimientos;
        }

        public List<material> Catalogo { get => catalogo; set => catalogo = value; }
        public List<persona> RegistroPersonas { get => registroPersonas; set => registroPersonas = value; }
        public List<movimiento> Movimientos { get => movimientos; set => movimientos = value; }

        public void agregarMaterial(material nuevoMaterial)
        {
            if (nuevoMaterial == null)
            {
                catalogo.Add(nuevoMaterial);
                MessageBox.Show("Guardado con exito");
            }
 
        }
    }


    public class movimiento
    {
        private persona persona;
        private material material;
        private DateTime fechaMovimiento;
        private string tipoMovimiento;

        public movimiento(persona persona, material material, DateTime fechaMovimiento, string tipoMovimiento)
        {
            this.Persona = persona;
            this.Material = material;
            this.FechaMovimiento = fechaMovimiento;
            this.TipoMovimiento = tipoMovimiento;
        }

        public persona Persona { get => persona; set => persona = value; }
        public material Material { get => material; set => material = value; }
        public DateTime FechaMovimiento { get => fechaMovimiento; set => fechaMovimiento = value; }
        public string TipoMovimiento { get => tipoMovimiento; set => tipoMovimiento = value; }
    }

    public class material
    {
        private int iSBN;
        private string nombre;  
        private DateTime fechaCreacion;
        private int cantidadReg;
        private int cantidadAct;

        public material(int iSBN, string nombre, DateTime fechaCreacion, int cantidadReg, int cantidadAct)
        {
            this.ISBN = iSBN;
            this.Nombre = nombre;
            this.FechaCreacion = fechaCreacion;
            this.CantidadReg = cantidadReg;
            this.CantidadAct = cantidadAct;
        }

        public int ISBN { get => iSBN; set => iSBN = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
        public int CantidadReg { get => cantidadReg; set => cantidadReg = value; }
        public int CantidadAct { get => cantidadAct; set => cantidadAct = value; }

        
    }

    public class persona
    {
        private string nombre;
        private int cc;
        private string roll;
        private int cantMaxPrestamo;
        
        public persona(string nombre, int cc, string roll, int cantMaxPrestamo)
        {
            this.Nombre = nombre;
            this.Cc = cc;
            this.Roll = roll;
            this.CantMaxPrestamo = cantMaxPrestamo;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Cc { get => cc; set => cc = value; }
        public string Roll { get => roll; set => roll = value; }
        public int CantMaxPrestamo { get => cantMaxPrestamo; set => cantMaxPrestamo = value; }
    }




}
