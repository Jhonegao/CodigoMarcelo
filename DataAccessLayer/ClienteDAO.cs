using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    //CRUD -> Create Read Update Delete
    public class ClienteDAO
    {
        public Response Insert(Cliente cliente)
        {
            Response response = new Response();
            //Classe responsável por realizar a conexão física 
            //com o banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\entra21\Documents\SistemaEstacionamento.mdf;Integrated Security=True;Connect Timeout=10";

            //Classe responsável por executar uma query no banco
            //de dados
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "INSERT INTO CLIENTES (NOME,CPF,CNH,ATIVO) VALUES (@NOME,@CPF,@CNH,@ATIVO)";
            command.Parameters.AddWithValue("@NOME", cliente.Nome);
            command.Parameters.AddWithValue("@CPF", cliente.CPF);
            command.Parameters.AddWithValue("@CNH", cliente.CNH);
            command.Parameters.AddWithValue("@ATIVO", true);

            //SqlCommando -> O QUE
            //SqlConnection -> ONDE
            command.Connection = connection;

            try
            {
                //Realiza, DE FATO, a conexão física com o banco de dados.
                //Este método pode e VAI lançar erros caso a base não exista ou esteja ocupada ou ainda o usuário não tenha permissão.
                connection.Open();
                //Se chegou aqui, o banco existe e podemos trabalhar!
                command.ExecuteNonQuery();
                response.Success = true;
                response.Message = "Cadastrado com sucesso.";
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o banco não foi encontrado ou não tinhamos permissão ou ainda estava ocupado.
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                //Estas duas propriedades são para LOG!
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            }
            finally
            {
                //Finally é um bloco de código que SEMPRE é executado, independentemente de exceções ou returns
                //connection.Dispose();
                connection.Close();
            }
            return response;
        }

        public Response Update(Cliente cliente)
        {
            Response response = new Response();
            //Classe responsável por realizar a conexão física 
            //com o banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\entra21\Documents\SistemaEstacionamento.mdf;Integrated Security=True;Connect Timeout=10";

            //Classe responsável por executar uma query no banco
            //de dados
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "UPDATE CLIENTES SET NOME = @NOME, ATIVO = @ATIVO WHERE ID = @ID";
            command.Parameters.AddWithValue("@NOME", cliente.Nome);
            command.Parameters.AddWithValue("@ATIVO", cliente.Ativo);
            command.Parameters.AddWithValue("@ID", cliente.ID);

            //SqlCommando -> O QUE
            //SqlConnection -> ONDE
            command.Connection = connection;

            try
            {
                //Realiza, DE FATO, a conexão física com o banco de dados.
                //Este método pode e VAI lançar erros caso a base não exista ou esteja ocupada ou ainda o usuário não tenha permissão.
                connection.Open();
                //Se chegou aqui, o banco existe e podemos trabalhar!
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1)
                {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }

                response.Success = true;
                response.Message = "Atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o banco não foi encontrado ou não tinhamos permissão ou ainda estava ocupado.
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                //Estas duas propriedades são para LOG!
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            }
            finally
            {
                //Finally é um bloco de código que SEMPRE é executado, independentemente de exceções ou returns
                //connection.Dispose();
                connection.Close();
            }
            return response;
        }

        public Response Delete(Cliente cliente)
        {
            Response response = new Response();
            //Classe responsável por realizar a conexão física 
            //com o banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\entra21\Documents\SistemaEstacionamento.mdf;Integrated Security=True;Connect Timeout=10";

            //Classe responsável por executar uma query no banco
            //de dados
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "DELETE FROM CLIENTES WHERE ID = @ID";
            command.Parameters.AddWithValue("@ID", cliente.ID);

            //SqlCommando -> O QUE
            //SqlConnection -> ONDE
            command.Connection = connection;

            try
            {
                //Realiza, DE FATO, a conexão física com o banco de dados.
                //Este método pode e VAI lançar erros caso a base não exista ou esteja ocupada ou ainda o usuário não tenha permissão.
                connection.Open();
                //Se chegou aqui, o banco existe e podemos trabalhar!
                int nLinhasAfetadas = command.ExecuteNonQuery();
                if (nLinhasAfetadas != 1)
                {
                    response.Success = false;
                    response.Message = "Registro não encontrado!";
                    return response;
                }

                response.Success = true;
                response.Message = "Excluído" +
                    " com sucesso.";
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o banco não foi encontrado ou não tinhamos permissão ou ainda estava ocupado.
                response.Success = false;
                response.Message = "Erro no banco de dados, contate o administrador.";
                //Estas duas propriedades são para LOG!
                response.StackTrace = ex.StackTrace;
                response.ExceptionError = ex.Message;
            }
            finally
            {
                //Finally é um bloco de código que SEMPRE é executado, independentemente de exceções ou returns
                //connection.Dispose();
                connection.Close();
            }
            return response;
        }

        public QueryResponse<Cliente> GetAll()
        {
            QueryResponse<Cliente> response = new QueryResponse<Cliente>();

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\entra21\Documents\SistemaEstacionamento.mdf;Integrated Security=True;Connect Timeout=10";

            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM CLIENTES WHERE ATIVO = 1";

            command.Connection = connection;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                List<Cliente> clientes = new List<Cliente>();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.ID = (int)reader["ID"];
                    cliente.Nome = (string)reader["NOME"];
                    cliente.CPF = (string)reader["CPF"];
                    cliente.CNH = (string)reader["CNH"];
                    cliente.DataCriacao = (DateTime)reader["DATACRIACAO"];
                    cliente.Ativo = (bool)reader["ATIVO"];
                    clientes.Add(cliente);

                }
                response.Success = true;
                response.Message = "Dados selecionados com sucesso.";
                response.Data = clientes;
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Erro no banco de dados contate o adm.";
                response.ExceptionError = ex.Message;
                response.StackTrace = ex.StackTrace;
                return response;
            }
            finally
            {
                connection.Close();
            }

        }
    }
}


        //public interface IEntityCRUD<T>
        //{
        //    DbResponse Insert(T item);
        //    DbResponse Update(T item);
        //    DbResponse Delete(T item);
        //}
        //public class Veiculo
        //{

        //}
        //public class VeiculoDAO : IEntityCRUD<Veiculo>
        //{
        //    public DbResponse Insert(Veiculo item)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public DbResponse Update(Veiculo item)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public DbResponse Delete(Veiculo item)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
