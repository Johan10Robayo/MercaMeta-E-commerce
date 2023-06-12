using MercaMetaApp.Data.DTO;
using MercaMetaApp.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics.CodeAnalysis;

namespace MercaMetaApp.Data.DAO
{
    public class ClienteDao
    {

        private readonly ConexionDb _conexionDb;

        public ClienteDao(ConexionDb conexionDb)
        {
            _conexionDb = conexionDb;
        }


        public ClienteFkDto InsertarCliente(ClienteFkDto clienteFkDto)
        {
            string sql = "INSERT INTO cliente VALUES (@idPersona, @idUsuario)";
            var MysqlConnection = _conexionDb.MysqlConnection;
            int filas = 0;
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql, MysqlConnection))
                {
                   command.Parameters.AddWithValue("@idPersona", clienteFkDto.IdPersona);
                   command.Parameters.AddWithValue("@idUsuario", clienteFkDto.IdUsario);
                    
                    filas = command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            if (filas > 0) return clienteFkDto;

            return null;
        }



    }
}
