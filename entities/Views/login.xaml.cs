using OpenQA.Selenium;
using prospecplus_interface.entities.Login;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Resources;

namespace prospecplus_interface.entities.Views
{
    /// <summary>
    /// Interação lógica para login.xam
    /// </summary>
    public partial class login : Page
    {
        private ResourceManager rs;
        public login()
        {
            InitializeComponent();
            rs = new ResourceManager("prospecplus_interface.Resources.Strings", Assembly.GetExecutingAssembly());
            //titulo
            titulo_naotem.Text = rs.GetString("titulo_naotem");
            titulo_bemvindo.Content = rs.GetString("titulo_bemvindo");
            //Subtitulo
            subtitulo_naotem.Text = rs.GetString("subtitulo_naotem");
            //Botões
            button_naotem.Content = rs.GetString("button_naotem");
            entrar.Content = rs.GetString("entrar");
            conectado.Content = rs.GetString("conectado");
            //Input
            senha.Text = rs.GetString("senha");
        }
        private void Login_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if(tb.Text == "Email" || tb.Text == rs.GetString("senha"))
            {
                tb.Text = string.Empty;

            }
        }
        private void Login_LostFocusE(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == string.Empty)
            {
                tb.Text = "Email";

            }
        }
        private void Login_LostFocusS(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text == string.Empty)
            {
                tb.Text = rs.GetString("senha");

            }
        }

        private void entrar_Click(object sender, RoutedEventArgs e)
        {
            var emailVar = email.Text;
            var senhaVar = senha.Text;
            loginClass login = new loginClass(emailVar, senhaVar);
            if(login.erro == false)
            {
                string connectionString = "Data Source=dados.sqlite;Version=3;";

               SQLiteConnection connection = new SQLiteConnection(connectionString);
               SQLiteCommand command = new SQLiteCommand(connection);
                try
                {
                    if ((bool)conectado.IsChecked)
                    {
                        connection.Open();
                        command.CommandText = $"UPDATE cliente SET id = {login.id}, nome = '{login.nome.Replace("'", "''")}', email = '{emailVar.Replace("'", "''")}', senha = '{senhaVar.Replace("'", "''")}'";
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        connection.Open();
                        command.CommandText = $"UPDATE cliente SET nome = '{login.nome.Replace("'", "''")}'";
                        command.ExecuteNonQuery();

                    }

                    inicio inicio = new inicio(false);
                    inicio.Show();
                    entities.Carregamento.fechar.close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu algum erro... + {ex.Message}", "Error");

                }
                finally { connection.Close(); }

            }
            else
            {
                MessageBox.Show(login.mensagem, "Error");
            }
        }
      
    }
}
