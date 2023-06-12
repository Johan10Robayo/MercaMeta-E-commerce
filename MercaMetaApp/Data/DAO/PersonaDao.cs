using MercaMetaApp.Data.DTO;
using MercaMetaApp.Models;
using MySql.Data.MySqlClient;
using System.Transactions;

namespace MercaMetaApp.Data.DAO
{
    public class PersonaDao
    {
        private readonly ConexionDb _conexionDb;

        public PersonaDao(ConexionDb conexionDb)
        {
            _conexionDb = conexionDb;
        }

        public PersonaDto InsertarPersona (PersonaDto personaDto) 
        {
            string sql = "INSERT INTO persona VALUES (@id, @nombre, @apellido, @telefono, @direccion)";
            var MysqlConnection = _conexionDb.MysqlConnection;
            int filas = 0;
            using MySqlTransaction transaction = MysqlConnection.BeginTransaction();

            try
            {
                
                var command = MysqlConnection.CreateCommand();  
                command.CommandText= sql;   
                command.Parameters.AddWithValue("@id", personaDto.Id);
                command.Parameters.AddWithValue("@nombre", personaDto.Nombre);
                command.Parameters.AddWithValue("@apellido", personaDto.Apellido);
                command.Parameters.AddWithValue("@telefono", personaDto.Telefono);
                command.Parameters.AddWithValue("@direccion", personaDto.Direccion);

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

            

            if (filas > 0) return personaDto;

            return null;
        }

        public int ActualizarPersonaAdmin(PersonaDto persona, int idPersonaActual)
        {
            int filas = 0;
            try
            {
                string sql = "update persona set id=@id, nombre=@nombre, apellido=@apellido," +
                    " telefono=@telefono, direccion=@direccion where id=@id_persona_actual";
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@id", persona.Id);
                command.Parameters.AddWithValue("@nombre", persona.Nombre);
                command.Parameters.AddWithValue("@apellido", persona.Apellido);
                command.Parameters.AddWithValue("@telefono", persona.Telefono);
                command.Parameters.AddWithValue("@direccion", persona.Direccion);
                command.Parameters.AddWithValue("@id_persona_actual", idPersonaActual);

                filas = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

            return filas;
        }

        public int EliminarPersona(int idPersona)
        {
            int filas = 0;
            try
            {
                string sql = "delete from persona where id=@id_persona";
                using MySqlCommand command = new MySqlCommand(sql, _conexionDb.MysqlConnection);
                command.Parameters.AddWithValue("@id_persona", idPersona);

                filas = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }

            return filas;
        }



    }
}
