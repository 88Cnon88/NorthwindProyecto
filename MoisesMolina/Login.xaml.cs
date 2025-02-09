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
using System.Data.SqlClient;
using System.Windows.Shapes;
using MoisesMolina;

namespace MoisesMolina
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Entrar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = UserText.Text; // Asumiendo que el TextBox del usuario tiene x:Name="UserText"
            string contrasena = PassText.Password; // Asumiendo que el PasswordBox tiene x:Name="PassText"

            if (DBase.Login(usuario, contrasena))
            {
                // Credenciales correctas, abrir la siguiente ventana
                VentanaPrincipal mainWindow = new VentanaPrincipal(); // Asegúrate de tener esta ventana creada
                mainWindow.Show();
                this.Close(); // Cierra la ventana actual
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Minimice(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}


