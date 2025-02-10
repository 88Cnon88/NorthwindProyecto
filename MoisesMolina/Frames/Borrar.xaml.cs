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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MoisesMolina.DBase;

namespace MoisesMolina.Frames
{
    
    /// Lógica de borrar
    
    public partial class Borrar : Page
    {
        public Borrar()
        {
            InitializeComponent();
            CargarProductos();
        }

        private Producto productoSeleccionado;

        private void CargarProductos()
        {
            ProductosDataGrid.ItemsSource = DBase.ObtenerProductos();
        }

        private void ProductosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productoSeleccionado = ProductosDataGrid.SelectedItem as Producto;

            if (productoSeleccionado != null)
            {
                OldProduct.Text = productoSeleccionado.Nombre;
            }
        }

        private void EliminarProducto(object sender, RoutedEventArgs e)
        {
            string nombreProducto = OldProduct.Text;

            if (string.IsNullOrWhiteSpace(nombreProducto))
            {
                MessageBox.Show("Por favor, selecciona o escribe el nombre del producto a eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var resultado = MessageBox.Show($"¿Estás seguro de que quieres eliminar el producto '{nombreProducto}'?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                bool eliminado = DBase.EliminarProducto(nombreProducto);

                if (eliminado)
                {
                    MessageBox.Show("Producto eliminado exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CargarProductos();  // Actualizo por aqui la lista en el DataGrid
                    OldProduct.Clear(); // Limpio
                }
                else
                {
                    MessageBox.Show("No se encontró el producto o no se pudo eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
