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
    /// <summary>
    /// Lógica de interacción para Agregar.xaml
    /// </summary>
    public partial class Agregar : Page
    {
        public Agregar()
        {
            InitializeComponent();
            CargarProductos();  // Cargar los productos
            CargarCategorias(); // Cargar las categorías
        }

        private void CargarProductos()
        {
            // Obtener los productos desde la base de datos
            List<DBase.Producto> productos = DBase.ObtenerProductos();

            // Enlazar los productos al DataGrid
            ProductosDataGrid.ItemsSource = productos;
        }

        private void CargarCategorias()
        {
            // Obtener las categorías desde la base de datos
            List<string> categorias = DBase.ObtenerCategorias();

            // Asignar las categorías al ComboBox
            CategoryComboBox.ItemsSource = categorias;
        }

        private void AgregarProducto(object sender, RoutedEventArgs e)
        {
            string productName = ProductNameText.Text;
            string category = CategoryComboBox.SelectedItem.ToString(); 

            // Verifica que ambos campos tengan valores
            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Realiza el Insert en la base de datos
            bool exito = DBase.AgregarProducto(productName, category);

            if (exito)
            {
                MessageBox.Show("Producto agregado exitosamente.");
                // Opcional: Actualiza la DataGrid con los nuevos datos
                ProductosDataGrid.ItemsSource = DBase.ObtenerProductos();
            }
            else
            {
                MessageBox.Show("Error al agregar el producto.");
            }
        }
    }
}
