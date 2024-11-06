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
        private material [] catalogo;
        private persona [] registroPersonas;
        private movimiento[] movimientos;
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
        private roll roll;

        public persona(string nombre, int cc, roll roll)
        {
            this.Nombre = nombre;
            this.Cc = cc;
            this.Roll = roll;
            
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Cc { get => cc; set => cc = value; }
        public roll Roll { get => roll; set => roll = value; }
    }
    
    public class roll
    {
        private string tipoRoll;
        private int maxPrestamos;

        public roll(string tipoRoll, int maxPrestamos)
        {
            this.TipoRoll = tipoRoll;
            this.MaxPrestamos = maxPrestamos;
        }

        public string TipoRoll { get => tipoRoll; set => tipoRoll = value; }
        public int MaxPrestamos { get => maxPrestamos; set => maxPrestamos = value; }
    }



}
