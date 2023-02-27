using OpenQA.Selenium;
using Org.BouncyCastle.Security;
using prospecplus_interface.entities.Selenium;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace prospecplus_interface.entities.Config
{
    class usuario
    {
        private string nome;
        private string email;

        //SQL
        private SQLiteConnection connection;
        private SQLiteCommand command;
        public usuario() {
            string connectionString = "Data Source=dados.sqlite;Version=3;";

            connection = new SQLiteConnection(connectionString);
            command = new SQLiteCommand(connection);

            try
            {
                connection.Open();
                command.CommandText = "SELECT * FROM cliente";
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    nome = reader["nome"].ToString();
                    email = reader["email"].ToString();

                }
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao carregamento da conta! + {ex.Message}", "ERROR");
            }
            finally { connection.Close(); }

        }
        public string getNome()
        {
            return nome;
        }
        public string getEmail() {
            return email;
        }
        public string primeiroNome()
        {
            var cortado = nome.Split(' ');
            return cortado[0];
        }

       

    }
}
