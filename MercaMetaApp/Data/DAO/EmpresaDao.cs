using MercaMetaApp.Data.DTO;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using NuGet.Protocol.Plugins;

namespace MercaMetaApp.Data.DAO
{
    public class EmpresaDao
    {
        private readonly ConexionDb _conexionDb;
        public EmpresaDao(ConexionDb conexionDb)
        {
            _conexionDb = conexionDb;
        }

        public EmpresaDto InsertarEmpresa(EmpresaDto empresaDto)
        {
            string sql = "INSERT INTO empresa VALUES (@nit, @nombre,@direccion,@url_imagen,@telefono,@id_representante,@id_usuario)";
            var MysqlConnection = _conexionDb.MysqlConnection;
            int filas = 0;
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql, MysqlConnection))
                {
                    command.Parameters.AddWithValue("@nit", empresaDto.Nit);
                    command.Parameters.AddWithValue("@nombre", empresaDto.Nombre);
                    command.Parameters.AddWithValue("@direccion", empresaDto.Direccion);
                    command.Parameters.AddWithValue("@url_imagen", empresaDto.UrlImagen);
                    command.Parameters.AddWithValue("@telefono", empresaDto.Telefono);
                    command.Parameters.AddWithValue("@id_representante", empresaDto.IdRepresentante);
                    command.Parameters.AddWithValue("@id_usuario", empresaDto.IdUsuario);



                    filas = command.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.Write(ex.Message);
            }

            if (filas > 0) return empresaDto;

            return null;
        }

        public List<EmpresaViewDto> GetEmpresas()
        {
            string sql = "select emp.nit, emp.nombre, emp.direccion, emp.url_imagen,emp.telefono," +
                "per.nombre as nombre_representante,per.apellido from empresa emp inner join " +
                "persona per on emp.id_representante = per.id;";

            var listaEmpresas = new List<EmpresaViewDto>();
            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var empresaViewDto = new EmpresaViewDto
                    {
                        Nit = reader.GetInt32("nit"),
                        Nombre = reader.GetString("nombre"),
                        Direccion = reader.GetString("direccion"),
                        UrlImagen = reader.GetString("url_imagen"),
                        Telefono = reader.GetInt64("telefono"),
                        NombreRepresentante = reader.GetString("nombre_representante"),
                        ApellidoRepresentante = reader.GetString("apellido")


                    };
                    listaEmpresas.Add(empresaViewDto);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }


            return listaEmpresas;
        }

        public List<EmpresaAdminDto> GetEmpresasAdmin()
        {
            string sql = "select emp.nit, emp.nombre, emp.direccion, emp.url_imagen,emp.telefono," +
                "per.id as id_representante, per.nombre as nomb_representante,per.apellido," +
                "per.telefono as tel_representante, per.direccion as dir_representante " +
                "from empresa emp inner join  persona per on emp.id_representante = per.id";

            var listaEmpresas = new List<EmpresaAdminDto>();
            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    var empresaAdminDto = new EmpresaAdminDto
                    {
                        Nit = reader.GetInt32("nit"),
                        Nombre = reader.GetString("nombre"),
                        Direccion = reader.GetString("direccion"),
                        UrlImagen = reader.GetString("url_imagen"),
                        Telefono = reader.GetInt64("telefono"),
                        Representante = new PersonaDto
                        {
                            Id = reader.GetInt32("id_representante"),
                            Nombre = reader.GetString("nomb_representante"),
                            Apellido = reader.GetString("apellido"),
                            Direccion = reader.GetString("dir_representante"),
                            Telefono = reader.GetInt64("tel_representante")

                        }


                    };
                    listaEmpresas.Add(empresaAdminDto);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }

            if (listaEmpresas.Count == 0) return null;

            return listaEmpresas;
        }

        public EmpresaAdminDto GetEmpresaAdminPorId(int idEmpresa)
        {
            string sql = "select emp.nit, emp.nombre, emp.direccion, emp.url_imagen,emp.telefono," +
                "per.id as id_representante, per.nombre as nomb_representante,per.apellido," +
                "per.telefono as tel_representante, per.direccion as dir_representante " +
                "from empresa emp inner join  persona per on emp.id_representante = per.id " +
                "where emp.nit = @id_empresa";

            var empresaAdminDto = new EmpresaAdminDto();
            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@id_empresa", idEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    empresaAdminDto.Nit = reader.GetInt32("nit");
                    empresaAdminDto.Nombre = reader.GetString("nombre");
                    empresaAdminDto.Direccion = reader.GetString("direccion");
                    empresaAdminDto.UrlImagen = reader.GetString("url_imagen");
                    empresaAdminDto.Telefono = reader.GetInt64("telefono");
                    empresaAdminDto.Representante = new PersonaDto
                    {
                        Id = reader.GetInt32("id_representante"),
                        Nombre = reader.GetString("nomb_representante"),
                        Apellido = reader.GetString("apellido"),
                        Direccion = reader.GetString("dir_representante"),
                        Telefono = reader.GetInt64("tel_representante")

                    };


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }


            return empresaAdminDto;
        }

        public int EliminarEmpresa(int idEmpresa)
        {
            int filas = 0;
            try
            {
                string sql = "DELETE FROM empresa WHERE nit = @id_empresa";
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@id_empresa", idEmpresa);
                filas = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

            return filas;
        }

        public int EliminarEmpresa2(int idEmpresa, int idUsuario, int idPersona)
        {
            int filas = 0;
            int filasUsuario = 0;
            int filasPersona = 0;
            MySqlConnection Mysqlconnection = _conexionDb.MysqlConnection;
            using MySqlTransaction transaction = Mysqlconnection.BeginTransaction();
            string sql = "DELETE FROM empresa WHERE nit=@id_empresa";
            string sqlUsuario = "delete from usuario where id=@id_usuario";
            string sqlPersona = "delete from persona where id=@id_persona";

            try
            {
                
                var command = Mysqlconnection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = sql;
                command.Parameters.AddWithValue("@id_empresa", idEmpresa);
                filas = command.ExecuteNonQuery();

                command = Mysqlconnection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = sqlUsuario;
                command.Parameters.AddWithValue("@id_usuario", idUsuario);
                filasUsuario = command.ExecuteNonQuery();

                command = Mysqlconnection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = sqlPersona;
                command.Parameters.AddWithValue("@id_persona", idPersona);
                filasPersona = command.ExecuteNonQuery();

                if (filas > 0 && filasUsuario > 0 && filasPersona > 0)
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
                Console.WriteLine(ex.Message);
                return 0;
            }

            return filas;
        }

        public int GetIdRepresentante(int idEmpresa)
        {
            string sql = "select per.id from empresa emp inner join persona per " +
                        "on per.id = emp.id_representante where emp.nit = @id_empresa";
            int id_representante = 0;


            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@id_empresa", idEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id_representante = reader.GetInt32("id");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                return 0;
            }


            return id_representante;
        }


        public int GetIdUsuario(int idEmpresa)
        {
            string sql = "select id_usuario from empresa where nit=@id_empresa";
            int idUsuario = 0;


            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@id_empresa", idEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    idUsuario = reader.GetInt32("id_usuario");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                return idUsuario;
            }


            return idUsuario;
        }


        public string GetUrlEmpresa(int idEmpresa)
        {
            string sql = "select url_imagen from empresa where nit=@id_empresa";
            string urlImagen = "";


            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@id_empresa", idEmpresa);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    urlImagen = reader.GetString("url_imagen");
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return urlImagen;
            }


            return urlImagen;
        }

        public int ActualizarEmpresaAdmin(EmpresaAdminDto empresa)
        {
            int filas = 0;
            try
            {
                bool bandera = false;
                string SqlImagen = "";
                if (empresa.Imagen !=null)
                {
                    SqlImagen = ", url_imagen=@url_imagen";
                    bandera = true; 
                }

                string sql = "update empresa set nombre=@nombre, direccion=@direccion, " +
                    "telefono=@telefono, id_representante=@id_representante"+SqlImagen+
                    " where nit=@id_empresa";
                using (MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection))
                {
                    command.Parameters.AddWithValue("@nombre", empresa.Nombre);
                    command.Parameters.AddWithValue("@direccion", empresa.Direccion);
                    command.Parameters.AddWithValue("@telefono", empresa.Telefono);
                    command.Parameters.AddWithValue("@id_representante", empresa.Representante.Id);
                    command.Parameters.AddWithValue("@id_empresa", empresa.Nit);
                    if (bandera) command.Parameters.AddWithValue("@url_imagen", empresa.UrlImagen);


                        filas = command.ExecuteNonQuery();

                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                return 0;
            }

            return filas;
        }


        public int ObetenerIdEmpresaPorEmail(string email)
        {
            string sql = "select emp.nit from empresa emp inner join usuario us" +
                " on us.id = emp.id_usuario where us.email=@email";
            int idEmpresa = 0;


            try
            {
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@email", email);
                using MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    idEmpresa = reader.GetInt32("nit");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return idEmpresa;
            }


            return idEmpresa;
        }




    }
}
