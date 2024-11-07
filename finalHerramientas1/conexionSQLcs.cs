using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace finalHerramientas1
{
    internal class conexionSQLcs
    {
        private SqlConnection connection;
        public conexionSQLcs()
        {
            string connectionString = "Server=DESKTOP-FGG38HQ\\SQLEXPRESSS;Database=herramientas1;Trusted_Connection=True";
            connection = new SqlConnection(connectionString);
        }

        public void AbrirConexion()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                    MessageBox.Show("Conexión abierta con éxito.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la conexión: " + ex.Message);
            }
        }
        public void CerrarConexion()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Conexión cerrada con éxito.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar la conexión: " + ex.Message);
            }
        }
        public SqlConnection ObtenerConexion()
        {
            return connection;
        }

        public void EjecutarConsulta(string consultaSQL)
        {
            try
            {
                AbrirConexion();
                SqlCommand command = new SqlCommand(consultaSQL, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("Consulta ejecutada con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }
        public SqlDataReader EjecutarConsultaLectura(string consultaSQL)
        {
            SqlDataReader dataReader = null;
            try
            {
                AbrirConexion();
                SqlCommand command = new SqlCommand(consultaSQL, connection);
                dataReader = command.ExecuteReader(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta de lectura: " + ex.Message);
            }
            return dataReader; 
        }
    }
}
