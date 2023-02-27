using prospecplus_interface.entities.Login;
using prospecplus_interface.entities.Views.subs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Navigation;

namespace prospecplus_interface.entities.Carregamento
{
    class verificamento
    {
        private SQLiteCommand command;
        private SQLiteConnection connection;

        public verificamento()
        {
            string connectionString = "Data Source=dados.sqlite;Version=3;";
            connection = new SQLiteConnection(connectionString);
            command = new SQLiteCommand(connection);
        }

        public bool verificarlogin()
        {
            try { 
               connection.Open();
                tabelas();
                criarUser();
                idioma();

            command.CommandText = "SELECT COUNT(*) FROM cliente";
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int count = reader.GetInt32(0);
                    reader.Close();
                    if (count >= 1)
                    {
                        command.CommandText = "SELECT * FROM cliente";
                        var ler = command.ExecuteReader();
                        if (ler.Read())
                        {
                            var email = ler["email"];
                            var senha = ler["senha"];
                            ler.Close();
                            loginClass login = new loginClass(email.ToString(), senha.ToString());

                            if (login.erro == false)
                            {

                                return true;
                            }

                        }

                    }
                }

                return false;
            }catch(Exception ex)
            {
                 return false;
            }
            finally { connection.Close(); }
        }
        public void criarUser()
        {
            try
            {
                command.CommandText = "SELECT COUNT(*) FROM cliente";
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int count = reader.GetInt32(0);
                    reader.Close();

                    if (count == 0)
                    {

                        command.CommandText = "INSERT INTO cliente (id, nome, email, senha) VALUES (1, 'null', 'null', 'null')";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro {ex.Message}", "ERROR");
            }
           
        }
        public void verificarConfig()
        {
            try
            {
                connection.Open();
                command.CommandText = "SELECT COUNT(*) FROM config";
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int count = reader.GetInt32(0);
                    reader.Close();
                    if (count == 0)
                    {
                       
                        command.CommandText = "INSERT INTO config (quantidade, executacao, mensagem, sales, email, idioma) VALUES (30, 5, 'Olá', 1, 'teste@gmail.com', 'pt-BR')";
                        command.ExecuteNonQuery();
                    }
                }
            }catch(Exception ex) {
                MessageBox.Show($"Ocorreu um erro {ex.Message}", "ERROR");

            }
            finally { connection.Close(); }

        }
        public void tabelas()
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS cliente(id INTEGER, Nome TEXT, email TEXT, senha TEXT)";
            command.ExecuteNonQuery();
            command.CommandText = "CREATE TABLE IF NOT EXISTS config(quantidade INTEGER, executacao INTEGER, mensagem TEXT, sales NUMERIC, email TEXT, idioma TEXT)";
            command.ExecuteNonQuery();
        }
        private void idioma()
        {
            command.CommandText = "SELECT * FROM config";
            SQLiteDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                string idioma_sq = reader["idioma"].ToString();
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(idioma_sq);

            }
            reader.Close();
        }
        
        
      
    }
}
