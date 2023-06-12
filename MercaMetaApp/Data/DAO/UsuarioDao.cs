using MercaMetaApp.Data.DTO;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

namespace MercaMetaApp.Data.DAO
{
    public class UsuarioDao
    {
        private readonly ConexionDb _conexionDb;

        public UsuarioDao(ConexionDb conexionDb)
        {
            _conexionDb = conexionDb;
        }

        public UsuarioDto InsertarUsuario(UsuarioDto usuarioDto)
        {
            string sql = "INSERT INTO usuario(email,paswd,rol) VALUES (@email, @paswd, @rol)";
            var MysqlConnection = _conexionDb.MysqlConnection;
            int filas = 0;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {
                var command = MysqlConnection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@email", usuarioDto.Email);
                command.Parameters.AddWithValue("@paswd", usuarioDto.Paswd);
                command.Parameters.AddWithValue("@rol", usuarioDto.Rol);
                   

                filas = command.ExecuteNonQuery();
                if (filas > 0)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }


            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }



            if (filas > 0) return usuarioDto;

            return null;
        }

        public int ObtenerIdUsuario(string email, string rol)
        {
            string sql = "SELECT id FROM usuario WHERE email=@email and rol=@rol";
            int id = 0;
            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@rol", rol);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    id = reader.GetInt32("id");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            

            return id;
        }

        public int EliminarUsuario(int idUsuario)
        {
            int filas = 0;
            try
            {
                string sql = "delete from usuario where id=@id_usuario";
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@id_usuario", idUsuario);

                filas = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

            return filas;
        }

        public int VerificarUsuario(string emailUsuario)
        {
            string sql = "select exists(select id from usuario " +
                "where email = @email_usuario) as resultado";
            int id = 0;
            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);
           
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    id = reader.GetInt32("resultado");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }


            return id;


        }

        public string ObtenerHashPasswd(string emailUsuario)
        {
            string sql = "select paswd from usuario " +
                "where email = @email_usuario";
            string hashPasswd = "";
            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    hashPasswd = reader.GetString("paswd");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return hashPasswd;
            }


            return hashPasswd;
        }

        public List<string> ObtenerRolesUsuario(string emailUsuario)
        { 
            List<string> roles = new List<string>();
            string sql = "select rol from usuario " +
                         "where email = @email_usuario";
 
            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@email_usuario", emailUsuario);

                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    roles.Add(reader.GetString("rol"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }

            return roles;
        }



    }
}
