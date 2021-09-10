using PrimerRegistroCompleto.BLL;
using PrimerRegistroCompleto.Entidades;
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

namespace PrimerRegistroCompleto.UI.Registros
{
    /// <summary>
    /// Interaction logic for rRoles.xaml
    /// </summary>
    public partial class rRoles : Window
    {
        public class DateFormat : System.Windows.Data.IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value == null) return null;

                return ((DateTime)value).ToString("dd/MM/yyyy");
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        private Roles BaseDato = new Roles();
        public rRoles()
        {
            InitializeComponent();
            this.DataContext = BaseDato;
        }
        private void Limpiar()
        {
            this.BaseDato = new Roles();
            this.DataContext = BaseDato;
        }

        private bool Validar()
        {
            bool posibilidadEscritura = true;

            if (FechaDatePicker.Text.Length == 0)
            {
                posibilidadEscritura = false;
                MessageBox.Show("Ha ocurrido un error, inserte la fecha", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (DescripcionTextBox.Text.Length == 0)
            {
                posibilidadEscritura = false;
                MessageBox.Show("Algo ha salido mal, ingrese la descripcion", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //MessageBox.Show($"Descripcion {DescripcionTextBox.Text} y {rol.Descripcion}");
            return posibilidadEscritura;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var roless = RolesBLL.Buscar(Utilidades.ToInt(RolesIdTextBox.Text));
            if (roless != null)
                this.BaseDato = roless;
            else
            {
                this.BaseDato = new Roles();
                MessageBox.Show("No se ha Encontrado", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            this.DataContext = this.BaseDato;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (RolesBLL.ExisteDescripcion(DescripcionTextBox.Text))
            {
                MessageBox.Show("Ya existe este rol, por favor ingrese otro", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var paso = RolesBLL.Guardar(this.BaseDato);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Proceso Exitoso!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Proceso Erroneo", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (RolesBLL.Eliminar(Utilidades.ToInt(RolesIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado!", "Exito",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se puede eliminar", "Fallo",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
