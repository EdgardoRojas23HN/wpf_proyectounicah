using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregar los namespaces necesarios
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.InteropServices;

namespace wpf_proyectounicah
{
    class Usuario
    {
        //Variables miembro

        private static string connectionString  = ConfigurationManager.ConnectionStrings["wpf_proyectounicah.Properties.Settings.ReservacionesConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades
        public int Id { get; set; }

        public string NombreCompleto { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool Estado { get; set; }

        //Constructores

        public Usuario() { }

        public Usuario(string nombreCompleto, string username, string password, bool estado) {

            NombreCompleto = nombreCompleto;
            Username = username;
            Password = password;
            Estado = estado;
        }

        //Metodos
        /// <summary>
        /// Verifica si las credenciales de inicio de sesion son correctas
        /// </summary>
        /// <param name="username">El nombre del usuario</param>
        /// <returns>Los datos del usuario</returns>
        public Usuario BuscarUsuario(string username)
        {
            //Crear el objeto que almacena la informacion de los resultados

            Usuario usuario = new Usuario();

            try
            {
                //Query de seleccion
                string query = @"SELECT * FROM  Usuarios.Usuario
                                WHERE username = @username";

                //Establecer la conexion
                //esta linea de codigo no es tan importante ya que el using me abrira
                //la conexion
               sqlConnection.Open();

                //crear  el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                //Establecer los valores de los parametros
                sqlCommand.Parameters.AddWithValue("@username", username);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())

                {
                    while (rdr.Read())
                    {
                        //Obtener  los valores del usuario si la consulta retorna valores
                        usuario.Id = Convert.ToInt32(rdr["id"]);
                        usuario.NombreCompleto = rdr["nombreCompleto"].ToString();
                        usuario.Username = rdr["username"].ToString();
                        usuario.Password = rdr["password"].ToString();
                        usuario.Estado = Convert.ToBoolean(rdr["estado"]);

                    }
                }
                //retornar el usuario con los valores
                return usuario;


            }
            catch (Exception e)
            {

                throw e;
            }
            finally 
            {
                //Cerrar la conexion
                sqlConnection.Close();
            }
        }
            
            }
}
