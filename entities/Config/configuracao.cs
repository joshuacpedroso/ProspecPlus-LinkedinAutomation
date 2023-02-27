using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace prospecplus_interface.entities.Config
{
    class configuracao
    {
        private int quantidade;
        private int execucao;
        private string mensagem;
        private bool sales = false;
        private string email;

        //SQL
        private SQLiteConnection connection;
        private SQLiteCommand command;

        public configuracao()
        {
            string connectionString = "Data Source=dados.sqlite;Version=3;";

            connection = new SQLiteConnection(connectionString);
            command = new SQLiteCommand(connection);
          
            try
            {
                connection.Open();
                command.CommandText = "SELECT * FROM config";
                var reader = command.ExecuteReader();
                if(reader.Read())
                {
                    quantidade = int.Parse(reader["quantidade"].ToString());
                    execucao = int.Parse(reader["executacao"].ToString());
                    mensagem = reader["mensagem"].ToString();
                    email = reader["email"].ToString();

                    if (int.Parse(reader["sales"].ToString()) == 1)
                    {
                        sales = true;
                    }
                   
                }
                reader.Close();

            }catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao carregamento da configuração! + {ex.Message}", "ERROR");
            }
            finally { connection.Close(); }
        }
        public int getQuantidade()
        {
            return quantidade;
        }
        public int getExecucao() { 
        return execucao;
        }
        public string getMensagem()
        {
            return mensagem;
        }
        public bool getSales()
        {
            return sales;
        }
        public string getEmail()
        {
            return email;
        }

    }
}
