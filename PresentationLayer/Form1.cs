using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //Classe responsável por realizar a conexão física 
            //com o banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\entra21\Documents\SistemaEstacionamento.mdf;Integrated Security=True;Connect Timeout=10";

            //Classe responsável por executar uma query no banco
            //de dados
            SqlCommand command = new SqlCommand();
            command.CommandText =
                "INSERT INTO CLIENTES (NOME,CPF,CNH,ATIVO) VALUES (@NOME,@CPF,@CNH,@ATIVO)";
            command.Parameters.AddWithValue("@NOME", txtNome.Text);
            command.Parameters.AddWithValue("@CPF", txtCPF.Text);
            command.Parameters.AddWithValue("@CNH", txtCNH.Text);
            command.Parameters.AddWithValue("@ATIVO", true);

            //SqlCommando -> O QUE
            //SqlConnection -> ONDE
            command.Connection = connection;
            bool sucesso = true;
            try
            {
                //Realiza, DE FATO, a conexão física com o banco de dados.
                //Este método pode e VAI lançar erros caso a base não exista ou esteja ocupada ou ainda o usuário não tenha permissão.
                connection.Open();
                //Se chegou aqui, o banco existe e podemos trabalhar!
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o banco não foi encontrado ou não tinhamos permissão ou ainda estava ocupado.
                MessageBox.Show("Erro no banco de dados, contate o adm");
                sucesso = false;
            }
            finally
            {
                //Finally é um bloco de código que SEMPRE é executado, independentemente de exceções ou returns
                //connection.Dispose();
                connection.Close();
                if (sucesso)
                {
                    MessageBox.Show("Cadastrado com sucesso!");
                }
            }
        }
    }
}
