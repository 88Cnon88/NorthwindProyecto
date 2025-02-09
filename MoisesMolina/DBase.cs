using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using MoisesMolina;

namespace MoisesMolina
{
    public static class DBase
    {
        // Cadena de conexión (se recomienda tenerla en un archivo de configuración)
        private static readonly string connectionString =
            "server=localhost;port=3306;user id=root;password=root;database=northwind";

        /// <summary>
        /// Realiza el login del usuario verificando si existe en la base de datos.
        /// </summary>
        public static bool Login(string usuario, string contrasena)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    string consulta = "SELECT COUNT(*) FROM usuarios WHERE nombre = @usuario AND contraseña = @contrasena";
                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar iniciar sesión: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Obtiene la lista de productos con su nombre y categoría.
        /// </summary>
        public static List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    string consulta = @"
                        SELECT p.ProductName AS Nombre, c.CategoryName AS Categoria
                        FROM products p
                        INNER JOIN categories c ON p.CategoryID = c.CategoryID";
                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    using (MySqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            productos.Add(new Producto
                            {
                                Nombre = lector["Nombre"].ToString(),
                                Categoria = lector["Categoria"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener productos: " + ex.Message);
            }

            return productos;
        }

        /// <summary>
        /// Obtiene la lista de categorías.
        /// </summary>
        public static List<string> ObtenerCategorias()
        {
            List<string> categorias = new List<string>();

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    string consulta = "SELECT CategoryName FROM categories";
                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    using (MySqlDataReader lector = cmd.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            categorias.Add(lector["CategoryName"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener categorías: " + ex.Message);
            }

            return categorias;
        }

        /// <summary>
        /// Agrega un producto nuevo a la base de datos.
        /// </summary>
        public static bool AgregarProducto(string productName, string category)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    string consulta = @"
                        INSERT INTO products (ProductName, CategoryID)
                        SELECT @productName, c.CategoryID
                        FROM categories c
                        WHERE c.CategoryName = @category";
                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@productName", productName);
                        cmd.Parameters.AddWithValue("@category", category);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Modifica los datos de un producto existente.
        /// </summary>
        public static bool ModificarProducto(string oldProductName, string newProductName, string newCategory)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    // Obtener CategoryID de la nueva categoría
                    string queryCategoria = "SELECT CategoryID FROM categories WHERE CategoryName = @newCategory";
                    int newCategoryId;
                    using (MySqlCommand cmdCategoria = new MySqlCommand(queryCategoria, conexion))
                    {
                        cmdCategoria.Parameters.AddWithValue("@newCategory", newCategory);
                        object result = cmdCategoria.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("La categoría especificada no existe.");
                            return false;
                        }
                        newCategoryId = Convert.ToInt32(result);
                    }

                    // Actualizar el producto
                    string updateQuery = "UPDATE products SET ProductName = @newProductName, CategoryID = @newCategoryId WHERE ProductName = @oldProductName";
                    using (MySqlCommand cmdUpdate = new MySqlCommand(updateQuery, conexion))
                    {
                        cmdUpdate.Parameters.AddWithValue("@newProductName", newProductName);
                        cmdUpdate.Parameters.AddWithValue("@newCategoryId", newCategoryId);
                        cmdUpdate.Parameters.AddWithValue("@oldProductName", oldProductName);
                        int filasAfectadas = cmdUpdate.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el producto: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Elimina un producto de la base de datos.
        /// </summary>
        public static bool EliminarProducto(string productName)
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    string consulta = "DELETE FROM products WHERE ProductName = @productName";
                    using (MySqlCommand cmd = new MySqlCommand(consulta, conexion))
                    {
                        cmd.Parameters.AddWithValue("@productName", productName);
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el producto: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Cierra la conexión si está abierta (no es estrictamente necesario al usar using).
        /// </summary>
        public static void CerrarConexion()
        {
            // Este método puede quedar vacío si se utiliza "using" en cada método.
        }

        /// <summary>
        /// Clase que representa un producto.
        /// </summary>
        public class Producto
        {
            public string Nombre { get; set; }
            public string Categoria { get; set; }
        }
    }
}
