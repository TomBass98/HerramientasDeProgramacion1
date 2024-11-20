using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private conexionSQLcs conexion;
        public biblioteca(List<material> catalogo, List<persona> registroPersonas, List<movimiento> movimientos)
        {
            this.Catalogo = catalogo;
            this.RegistroPersonas = registroPersonas;
            this.Movimientos = movimientos;
            this.conexion = new conexionSQLcs(); // Inicialización de la conexión
        }

        public List<material> Catalogo { get => catalogo; set => catalogo = value; }
        public List<persona> RegistroPersonas { get => registroPersonas; set => registroPersonas = value; }
        public List<movimiento> Movimientos { get => movimientos; set => movimientos = value; }

        
        public void registrarMaterial(material nuevoMaterial)
        {
            if (catalogo.Exists(m => m.ISBN == nuevoMaterial.ISBN))
            {
                MessageBox.Show("El material con este ISBN ya existe.");
                return;
            }

            catalogo.Add(nuevoMaterial);

            // Guardar en la base de datos
            string query = $"INSERT INTO Material (ISBN, Nombre, FechaRegistro, CantidadReg, CantidadAct) " +
                           $"VALUES ({nuevoMaterial.ISBN}, '{nuevoMaterial.Nombre}', '{nuevoMaterial.FechaCreacion}', {nuevoMaterial.CantidadReg}, {nuevoMaterial.CantidadAct})";

            conexion.EjecutarConsulta(query);

            MessageBox.Show("Material registrado con éxito.");
        }

        // Incrementar Cantidad de Material
        public void incrementarCantidad(int isbn, int cantidad)
        {
            material mat = catalogo.FirstOrDefault(m => m.ISBN == isbn);
            if (mat == null)
            {
                MessageBox.Show("Material no encontrado.");
                return;
            }

            mat.CantidadAct += cantidad;

            // Actualizar en la base de datos
            string query = $"UPDATE Material SET CantidadAct = {mat.CantidadAct} WHERE ISBN = {isbn}";
            conexion.EjecutarConsulta(query);

            MessageBox.Show("Cantidad incrementada con éxito.");
        }

        // Registrar Persona
        public void registrarPersona(persona nuevaPersona)
        {
            if (registroPersonas.Exists(p => p.Cc == nuevaPersona.Cc))
            {
                MessageBox.Show("Ya existe una persona registrada con esta cédula.");
                return;
            }

            registroPersonas.Add(nuevaPersona);

            // Guardar en la base de datos
            string query = $"INSERT INTO Persona (CC, Nombre, Rol, CantMaxPrestamo) " +
                           $"VALUES ({nuevaPersona.Cc}, '{nuevaPersona.Nombre}', '{nuevaPersona.Roll}', {nuevaPersona.CantMaxPrestamo})";

            conexion.EjecutarConsulta(query);

            MessageBox.Show("Persona registrada con éxito.");
        }


        public void eliminarPersona(int cc)
        {
            // Primero verificamos si la persona existe utilizando consultaPersona
            persona per = consultaPersona(cc);

            // Si la persona no existe, mostramos un mensaje y salimos del método
            if (per == null)
            {
                MessageBox.Show("La persona no está registrada.");
                return;
            }

            // Verificamos si la persona tiene préstamos activos
            if (movimientos.Any(m => m.Persona.Cc == cc && m.TipoMovimiento == "Préstamo"))
            {
                MessageBox.Show("No se puede eliminar una persona con préstamos activos.");
                return;
            }

            // Si no tiene préstamos activos, la eliminamos del registro en memoria
            registroPersonas.Remove(per);

            // Eliminar la persona de la base de datos
            string query = $"DELETE FROM Persona WHERE Cedula = {cc}";
            try
            {
                // Ejecutar la consulta SQL para eliminar la persona de la base de datos
                conexion.EjecutarConsulta(query);
                MessageBox.Show("Persona eliminada con éxito.");
            }
            catch (Exception ex)
            {
                // Manejar cualquier error de la base de datos
                MessageBox.Show($"Error al eliminar la persona de la base de datos: {ex.Message}");
            }
        }


        // Registrar Préstamo
        public void registrarPrestamo(int isbn, int cc)
        {
            material mat = catalogo.FirstOrDefault(m => m.ISBN == isbn);
            persona per = registroPersonas.FirstOrDefault(p => p.Cc == cc);

            if (mat == null || per == null)
            {
                MessageBox.Show("Material o persona no encontrados.");
                return;
            }

            if (mat.CantidadAct == 0)
            {
                MessageBox.Show("No hay unidades disponibles para préstamo.");
                return;
            }

            int prestamosActivos = movimientos.Count(m => m.Persona.Cc == cc && m.TipoMovimiento == "Préstamo");
            if (prestamosActivos >= per.CantMaxPrestamo)
            {
                MessageBox.Show("La persona ha alcanzado el límite máximo de préstamos.");
                return;
            }

            mat.CantidadAct--;
            movimientos.Add(new movimiento(per, mat, DateTime.Now, "Préstamo"));

            // Guardar en la base de datos
            string queryMovimiento = $"INSERT INTO Movimiento (CC, ISBN, FechaMovimiento, TipoMovimiento) " +
                                      $"VALUES ({cc}, {isbn}, '{DateTime.Now}', 'Préstamo')";
            string queryMaterial = $"UPDATE Material SET CantidadAct = {mat.CantidadAct} WHERE ISBN = {isbn}";

            conexion.EjecutarConsulta(queryMovimiento);
            conexion.EjecutarConsulta(queryMaterial);

            MessageBox.Show("Préstamo registrado con éxito.");
        }

        // Registrar Devolución
        public void registrarDevolucion(int isbn, int cc)
        {
            material mat = catalogo.FirstOrDefault(m => m.ISBN == isbn);
            persona per = registroPersonas.FirstOrDefault(p => p.Cc == cc);

            if (mat == null || per == null)
            {
                MessageBox.Show("Material o persona no encontrados.");
                return;
            }

            mat.CantidadAct++;
            movimientos.Add(new movimiento(per, mat, DateTime.Now, "Devolución"));

            // Guardar en la base de datos
            string queryMovimiento = $"INSERT INTO Movimiento (CC, ISBN, FechaMovimiento, TipoMovimiento) " +
                                      $"VALUES ({cc}, {isbn}, '{DateTime.Now}', 'Devolución')";
            string queryMaterial = $"UPDATE Material SET CantidadAct = {mat.CantidadAct} WHERE ISBN = {isbn}";

            conexion.EjecutarConsulta(queryMovimiento);
            conexion.EjecutarConsulta(queryMaterial);

            MessageBox.Show("Devolución registrada con éxito.");
        }

        // Consultar Historial
        public List<movimiento> consultarHistorial()
        {
            // Leer datos de la base de datos y mapearlos a objetos movimiento
            string query = "SELECT * FROM Movimiento";
            var reader = conexion.EjecutarConsultaLectura(query);

            List<movimiento> historial = new List<movimiento>();

            while (reader.Read())
            {
                int cc = reader.GetInt32(0);
                int isbn = reader.GetInt32(1);
                DateTime fecha = reader.GetDateTime(2);
                string tipo = reader.GetString(3);

                persona per = registroPersonas.FirstOrDefault(p => p.Cc == cc);
                material mat = catalogo.FirstOrDefault(m => m.ISBN == isbn);

                if (per != null && mat != null)
                {
                    historial.Add(new movimiento(per, mat, fecha, tipo));
                }
            }

            reader.Close();
            return historial;
        }

public persona consultaPersona(int cc)
{
    // Definir la consulta SQL para obtener los datos de la persona usando parámetros
    string query = "SELECT Nombre, Cedula, Rol, CantMaxPrestamo FROM Persona WHERE Cedula = @cc";

    try
    {
        // Crear la conexión SQL
        using (SqlConnection conexionSQL = new SqlConnection("your_connection_string_here"))
        {
            // Crear el comando SQL con el parámetro
            SqlCommand cmd = new SqlCommand(query, conexionSQL);
            cmd.Parameters.AddWithValue("@cc", cc); // Usamos el parámetro @cc

            // Abrir la conexión
            conexionSQL.Open();

            // Ejecutar la consulta y obtener el SqlDataReader
            SqlDataReader reader = cmd.ExecuteReader();

            // Verificar si se ha encontrado una persona con esa cédula
            if (reader.Read())
            {
                // Crear una nueva instancia de Persona con los datos obtenidos
                persona persona = new persona(
                    reader["Nombre"].ToString(),
                    Convert.ToInt32(reader["Cedula"]),
                    reader["Rol"].ToString(),
                    Convert.ToInt32(reader["CantMaxPrestamo"]) // Suponiendo que tienes este campo en la base de datos
                );

                reader.Close();
                return persona;
            }
            else
            {
                // Si no se encuentra la persona
                MessageBox.Show("La persona no se encontró en la base de datos.");
                reader.Close(); // Cerramos el reader si no se encuentra la persona
                return null;
            }
        }
    }
    catch (Exception ex)
    {
        // Si ocurre un error al realizar la consulta
        MessageBox.Show($"Error al consultar la persona: {ex.Message}");
        return null;
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
