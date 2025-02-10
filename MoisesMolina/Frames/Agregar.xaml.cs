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
using MoisesMolina.Frames;


namespace MoisesMolina.Frames
{
    
    /// Lógica donde se interactua para Agregar.xaml
    
    public partial class Agregar : Page
    {
        public Agregar()
        {
            InitializeComponent();
            CargarProductos();  // Carga los productos
            CargarCategorias(); // Carga las categorías
        }

        private void CargarProductos()
        {
            // Obtengo los productos desde la base de datos
            List<DBase.Producto> productos = DBase.ObtenerProductos();

            // Enlazo los productos al DataGrid
            ProductosDataGrid.ItemsSource = productos;
        }

        private void CargarCategorias()
        {
            // Obtengo las categorias
            List<string> categorias = DBase.ObtenerCategorias();

            // Asigno al ComboBox
            CategoryComboBox.ItemsSource = categorias;
        }

        private void AgregarProducto(object sender, RoutedEventArgs e)
        {
            string productName = ProductNameText.Text;
            string category = CategoryComboBox.SelectedItem.ToString(); 

            // Verifico
            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Realizo insert
            bool exito = DBase.AgregarProducto(productName, category);

            if (exito)
            {
                MessageBox.Show("Producto agregado exitosamente.");
                // Actualizo
                ProductosDataGrid.ItemsSource = DBase.ObtenerProductos();
            }
            else
            {
                MessageBox.Show("Error al agregar el producto.");
            }
        }
    }
}
