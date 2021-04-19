using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregar los namespaces requeridos
using System.Data.SqlClient;
using System.Configuration;

namespace wpf_proyectounicah
{
    //Crear una variable que mantenga los valores  para los estados de la habitacion

    public enum EstadosHabitacion 
    {
        Ocupada = 'O',
        Disponible = 'D',
        Mantenimiento  = 'M', 
        FueraServicio= 'F'
    }
    class Habitacion
    {
        //Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["wpf_proyectounicah.Properties.Settings.ReservacionesConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        
        //Propiedades
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public int Numero { get; set; }

        public EstadosHabitacion Estado  { get; set;  }

        //Constructores
        public Habitacion() { }

        public Habitacion(string descripcion, int numero, EstadosHabitacion estado)
        {
            Descripcion = descripcion;
            Numero = numero;
            Estado = estado;
        }
        //Metodos
        private string ObtenerEstados(EstadosHabitacion estado) 
        {
            switch (estado) 
            {
                case EstadosHabitacion.Ocupada: 
                return "OCUPADA";

                case EstadosHabitacion.Disponible:
                return "DISPONIBLE";

                case EstadosHabitacion.Mantenimiento:
                    return "MANTENIMIENTO";

                case EstadosHabitacion.FueraServicio: 
                    return "FUERA DE SERVICIO";
                default:
                    return "DISPONIBLE";
            }
            }

        public void CrearHabitacion(Habitacion habitacion ) 
        {
            try 
            {
                //Query de inserción
                string query = @"INSERT INTO Habitaciones.Habitacion(descripcion, numero, estado)
                                VALUES(@descripcion,@numero, @estado)";

                //Establecer conexion
                sqlConnection.Open();

                //Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue(@"descripcion", habitacion.Descripcion);
                sqlCommand.Parameters.AddWithValue(@"numero", habitacion.Numero);
                sqlCommand.Parameters.AddWithValue(@"estado", ObtenerEstados(habitacion.Estado));

                //Ejecutar el comando de inserción
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
            
        }
    
    }   
}
