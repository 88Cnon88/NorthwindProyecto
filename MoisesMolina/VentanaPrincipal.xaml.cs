
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
    
    public partial class VentanaPrincipal : Window
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
        }
        private void CambioPaginaInicio(object sender, RoutedEventArgs e)
        {
            
            miFrame.Content = new Inicio();  
        }

        private void CambioPaginaAgregar(object sender, RoutedEventArgs e)
        {
            
            miFrame.Content = new Agregar();  
        }
        private void CambioPaginaModificar(object sender, RoutedEventArgs e)
        {
            
            miFrame.Content = new Modificar();  
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
                Login login = new Login(); 
                login.Show();
                this.Close(); 
            }
        }

    }
}