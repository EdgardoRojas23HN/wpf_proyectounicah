using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace wpf_proyectounicah
{
    /// <summary>
    /// Lógica de interacción para Habitaciones.xaml
    /// </summary>
    public partial class Habitaciones : Window
    {

        //Variables miembro 
        private Habitacion habitacion = new Habitacion();
        private List<Habitacion> habitaciones;
        public Habitaciones()
        {
            InitializeComponent();

            //Lenar el combobox de estado de la habitacion
            cmbEstado.ItemsSource = Enum.GetValues(typeof(EstadosHabitacion));

            //LLenar el listbox de habitaciones
            ObtenerHabitaciones();
        }

        private void LimpiarFormulario() 
        {
            txtdescripcion.Text = string.Empty;
            txtNumeroHabitacion.Text = string.Empty;
            cmbEstado.SelectedValue = null;
        
        }

        private void ObtenerValoresFormulario() 
        {
            habitacion.Descripcion = txtdescripcion.Text;
            habitacion.Numero = Convert.ToInt32(txtNumeroHabitacion.Text);
            habitacion.Estado = (EstadosHabitacion)cmbEstado.SelectedValue;
        }
        private void ObtenerHabitaciones()
        {
            habitaciones = habitacion.MostrarHabitaciones();
            lbHabitaciones.DisplayMemberPath = "Descripcion";
            lbHabitaciones.SelectedValuePath = "Id";
            lbHabitaciones.ItemsSource = habitaciones;


        }

        private void ValoresFormularioDesdeObjeto() 
        {
            txtdescripcion.Text = habitacion.Descripcion;
            txtNumeroHabitacion.Text = habitacion.Numero.ToString();
            cmbEstado.SelectedValue = habitacion.Estado;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //Verificar que se ingresaron los valores requeridos
            if (txtdescripcion.Text == string.Empty || txtNumeroHabitacion.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto ");
            }
            else if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el estado de la habitacion");
            }
            else 
            {
                try
                {
                    //Obtener los valores para la habitacion
                    ObtenerValoresFormulario();

                    //Insertar los datos de la habitacion
                    habitacion.CrearHabitacion(habitacion);

                    //Mensaje de insercion exitosa 
                    MessageBox.Show("!Datos insertados correctamente¡");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar la habitacion..");
                    Console.WriteLine(ex.Message);
                }
                finally 
                {
                    LimpiarFormulario();
                    ObtenerHabitaciones();
                } 
               
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (lbHabitaciones.SelectedValue == null)
            {
                MessageBox.Show("Porfavor seleccionar una habitacion desde el listado");
            }
            else 
            {
                try
                {
                    //Obtener la informacion de la habitacion
                    habitacion = habitacion.BuscarHabitacion(Convert.ToInt32(lbHabitaciones.SelectedValue));

                    //Llenar los valores del formulario
                     ValoresFormularioDesdeObjeto();
                
                    //Ocultar los botones de operaciones CRUD
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ha ocurrido un error al momento de modificar la habitacion...");
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
