
using MoisesMolina.Frames;
using MoisesMolina;
using Mysqlx.Notice;
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

namespace MoisesMolina
{
    /// <summary>
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
        }
        private void CambioPaginaInicio(object sender, RoutedEventArgs e)
        {
            // Cambiar el contenido del Frame a la página "Inicio.xaml"
            miFrame.Content = new Inicio();  // 'miFrame' es el nombre de tu Frame
        }

        private void CambioPaginaAgregar(object sender, RoutedEventArgs e)
        {
            // Cambiar el contenido del Frame a la página "Agregar.xaml"
            miFrame.Content = new Agregar();  // 'miFrame' es el nombre de tu Frame
        }
        private void CambioPaginaModificar(object sender, RoutedEventArgs e)
        {
            // Cambiar el contenido del Frame a la página "Agregar.xaml"
            miFrame.Content = new Modificar();  // 'miFrame' es el nombre de tu Frame
        }
        private void CambioPaginaBorrar(object sender, RoutedEventArgs e)
        {
            miFrame.Content = new Borrar();
        }

        private void Minimice(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }


        private void VolverLogin(object sender, RoutedEventArgs e)
        {
            {
                Login login = new Login(); // Asegúrate de tener esta ventana creada
                login.Show();
                this.Close(); // Cierra la ventana actual
            }
        }

    }
}