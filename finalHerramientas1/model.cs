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
            string query = $"INSERT INTO material (iSBN, Nombre,  CantidadRegistrada, CantidadActual) " +
                           $"VALUES ({nuevoMaterial.ISBN}, '{nuevoMaterial.Nombre}', {nuevoMaterial.CantidadReg}, {nuevoMaterial.CantidadAct})";

            conexion.EjecutarConsulta(query);

            MessageBox.Show("Material registrado con éxito.");
        }
        public void prestamo(int isbn)
        {
            // Verificar que el ISBN sea válido
            if (isbn <= 0)
            {
                MessageBox.Show("Por favor, ingrese un ISBN válido.");
                return;
            }


            string queryVerificar = "SELECT COUNT(*) FROM material WHERE iSBN = @isbn";

      
            string queryActualizar = "UPDATE material SET CantidadActual = CantidadActual - 1 WHERE iSBN = @isbn AND CantidadActual > 0";

            try
            {


                // Verificar si el material existe
                using (SqlCommand comandoVerificar = new SqlCommand(queryVerificar, conexion.ObtenerConexion()))
                {
                    comandoVerificar.Parameters.AddWithValue("@isbn", isbn);

                    conexion.AbrirConexion();
                    int cantidad = (int)comandoVerificar.ExecuteScalar(); // Devuelve la cantidad de registros encontrados

                    if (cantidad == 0) // Si no existe el material
                    {
                        MessageBox.Show("El material no existe en la base de datos.");
                        return;
                    }
                }

                // Actualizar la cantidad del material
                using (SqlCommand comandoActualizar = new SqlCommand(queryActualizar, conexion.ObtenerConexion()))
                {
                    comandoActualizar.Parameters.AddWithValue("@isbn", isbn);

                    int filasAfectadas = comandoActualizar.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Material actualizado con éxito. Cantidad disminuida correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el material. Verifica la cantidad disponible.");
                    }
                }
                
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al consultar o actualizar el material: {ex.Message}");
            }
        }

        public void Devolucion(int isbn)
        {
            // Verificar que el ISBN sea válido
            if (isbn <= 0)
            {
                MessageBox.Show("Por favor, ingrese un ISBN válido.");
                return;
            }


            string queryVerificar = "SELECT COUNT(*) FROM material WHERE iSBN = @isbn";


            string queryActualizar = "UPDATE material SET CantidadActual = CantidadActual + 1 WHERE iSBN = @isbn AND CantidadActual > 0";

            try
            {


                // Verificar si el material existe
                using (SqlCommand comandoVerificar = new SqlCommand(queryVerificar, conexion.ObtenerConexion()))
                {
                    comandoVerificar.Parameters.AddWithValue("@isbn", isbn);

                    conexion.AbrirConexion();
                    int cantidad = (int)comandoVerificar.ExecuteScalar(); // Devuelve la cantidad de registros encontrados

                    if (cantidad == 0) // Si no existe el material
                    {
                        MessageBox.Show("El material no existe en la base de datos.");
                        return;
                    }
                }

                // Actualizar la cantidad del material
                using (SqlCommand comandoActualizar = new SqlCommand(queryActualizar, conexion.ObtenerConexion()))
                {
                    comandoActualizar.Parameters.AddWithValue("@isbn", isbn);

                    int filasAfectadas = comandoActualizar.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Material actualizado con éxito. Cantidad aumentada correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el material. Verifica la cantidad disponible.");
                    }
                }

            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al consultar o actualizar el material: {ex.Message}");
            }
        }
        public void ConsultarFilaPorISBN(int isbn)
        {
            string query = "SELECT * FROM material WHERE isbn = @isbn";

            try
            {
                conexion.AbrirConexion();

                using (SqlCommand command = new SqlCommand(query, conexion.ObtenerConexion()))
                {
                    command.Parameters.AddWithValue("@isbn", isbn);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nombre = reader["nombre"].ToString();
                            string fechaCreacion = reader["fechaCreacion"].ToString();
                            string cantidadRegistrada = reader["cantidadRegistrada"].ToString();
                            string cantidadActual = reader["cantidadActual"].ToString();

                            string resultado = $"ISBN: {isbn}\nNombre: {nombre}\nFecha Creación: {fechaCreacion}\nCantidad Registrada: {cantidadRegistrada}\nCantidad Actual: {cantidadActual}";
                            MessageBox.Show(resultado, "Información del Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el material con el ISBN especificado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar la fila: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                           $"VALUES ({nuevaPersona.Cc}, '{nuevaPersona.Nombre}', '{nuevaPersona.Rol}', {nuevaPersona.CantMaxPrestamo})";

            conexion.EjecutarConsulta(query);

            MessageBox.Show("Persona registrada con éxito.");
        }


        public void eliminarPersona(int cc)
        {
            persona per = consultaPersona(cc);

            if (per == null)
            {
                MessageBox.Show("La persona no está registrada.");
                return;
            }

            if (movimientos.Any(m => m.Persona.Cc == cc && m.TipoMovimiento == "Préstamo"))
            {
                MessageBox.Show("No se puede eliminar una persona con préstamos activos.");
                return;
            }

            registroPersonas.Remove(per);

            string query = "DELETE FROM persona WHERE cc = @cc";
            try
            {
                conexion.AbrirConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@cc", cc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Persona eliminada con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la persona de la base de datos: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
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
            string query = "SELECT nombre, cc, rol, cantMaxPrestamo FROM persona WHERE cc = @cc";

            try
            {
                conexion.AbrirConexion();
                SqlCommand cmd = new SqlCommand(query, conexion.ObtenerConexion());
                cmd.Parameters.AddWithValue("@cc", cc);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    persona persona = new persona(
                        reader["nombre"]?.ToString() ?? "Desconocido",
                        Convert.ToInt32(reader["cc"]),
                        reader["rol"]?.ToString() ?? "Sin rol",
                        reader["cantMaxPrestamo"] != DBNull.Value
                            ? Convert.ToInt32(reader["cantMaxPrestamo"])
                            : 3
                    );

                    reader.Close();
                    return persona;
                }
                else
                {

                    reader.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar la persona: {ex.Message}");
                return null;
            }
            finally
            {
                conexion.CerrarConexion();
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
            private int cantidadReg;
            private int cantidadAct;
            private string tipo;

            public material(int iSBN, string nombre,  int cantidadReg, int cantidadAct, string tipo)
            {
                this.ISBN = iSBN;
                this.Nombre = nombre;
                
                this.CantidadReg = cantidadReg;
                this.CantidadAct = cantidadAct;
                this.tipo = tipo;
            }

            public int ISBN { get => iSBN; set => iSBN = value; }
            public string Nombre { get => nombre; set => nombre = value; }
        
            public int CantidadReg { get => cantidadReg; set => cantidadReg = value; }
            public int CantidadAct { get => cantidadAct; set => cantidadAct = value; }

            public string Tipo { get => tipo; set => tipo = value; }
        }

        public class persona
        {
            // Campos privados
            private string nombre;
            private int cc;
            private string rol; // Corregido de "roll" a "rol"
            private int cantMaxPrestamo;

            // Constructor
            public persona(string nombre, int cc, string rol, int cantMaxPrestamo)
            {
                this.Nombre = nombre;
                this.Cc = cc;
                this.Rol = rol; // Corregido
                this.CantMaxPrestamo = cantMaxPrestamo;
            }

            // Propiedades públicas
            public string Nombre
            {
                get => nombre;
                set => nombre = value;
            }
            public int Cc
            {
                get => cc;
                set => cc = value;
            }
            public string Rol // Corregido
            {
                get => rol;
                set => rol = value;
            }
            public int CantMaxPrestamo
            {
                get => cantMaxPrestamo;
                set => cantMaxPrestamo = value;
            }
        }





    }
}
