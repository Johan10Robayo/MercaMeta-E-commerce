using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System.Security.Cryptography.X509Certificates;

namespace MercaMetaApp.Data
{
    public sealed class ConexionDb
    {
        private readonly MySqlConnection conexion;

        public ConexionDb(string cadenaConexion)
        {
            try
            {
                conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }         
        }

        public MySqlConnection MysqlConnection { get => conexion; }




    }
}
