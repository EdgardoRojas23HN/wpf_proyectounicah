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
        Ocupada = 'OCUPADA',
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
    
    }   
}
