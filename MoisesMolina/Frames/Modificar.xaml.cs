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
    /// <summary>
    /// Lógica de interacción para Modificar.xaml
    /// </summary>
    public partial class Modificar : Page
    {
        public Modificar()
        {
            InitializeComponent();
            CargarProductos();
            CategoryComboBox.ItemsSource = DBase.ObtenerCategorias();
        }


        private void CargarProductos()
        {
            ProductosDataGrid.ItemsSource = DBase.ObtenerProductos();
        }


        private void CargarCategorias()
        {
            // Obtener las categorías desde la base de datos
            List<string> categorias = DBase.ObtenerCategorias();

            // Asignar las categorías al ComboBox
            CategoryComboBox.ItemsSource = categorias;
        }

        private void ModificarProduct(object sender, RoutedEventArgs e)
        {
            string oldProductName = OldProduct.Text;
            string newProductName = NewProduct.Text;
            string selectedCategory = CategoryComboBox.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(oldProductName) || string.IsNullOrWhiteSpace(newProductName) || string.IsNullOrWhiteSpace(selectedCategory))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            bool modificado = DBase.ModificarProducto(oldProductName, newProductName, selectedCategory);

            if (modificado)
            {
                MessageBox.Show("Producto modificado con éxito.");
                CargarProductos(); 
            }
            else
            {
                MessageBox.Show("No se pudo modificar el producto. Verifica que el nombre del producto antiguo sea correcto.");
            }
        }


        private Producto productoSeleccionado;

        private void ProductosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productoSeleccionado = ProductosDataGrid.SelectedItem as Producto;

            if (productoSeleccionado != null)
            {
                OldProduct.Text = productoSeleccionado.Nombre;
                CategoryComboBox.SelectedItem = productoSeleccionado.Categoria; // Opcional si quieres preseleccionar la categoría
            }
        }

    }
}
