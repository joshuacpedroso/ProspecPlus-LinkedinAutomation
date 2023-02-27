using prospecplus_interface.entities.Config;
using prospecplus_interface.entities.Views.subs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;
using OpenQA.Selenium;
using System.Data.SQLite;
using System.Reflection;
using System.Resources;

namespace prospecplus_interface.entities.Views
{
    /// <summary>
    /// Lógica interna para inicio.xaml
    /// </summary>
    public partial class inicio : Window
    {
        private string idiomaVar;
        public inicio(bool loginV)
        {

            InitializeComponent();

            if (loginV == false)
            {
                frame.Navigate(new home());
                usuario usuario = new usuario();
                nome.Text = usuario.primeiroNome();
                string connectionString = "Data Source=dados.sqlite;Version=3;";
                SQLiteConnection connection = new SQLiteConnection(connectionString);
                SQLiteCommand command = new SQLiteCommand(connection);
                try
                {
                    connection.Open();
                    command.CommandText = "SELECT * FROM config";
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        idiomaVar = reader["idioma"].ToString();
                        switch (idiomaVar)
                        {
                            case "pt-BR":
                                idioma.SelectedIndex = 0;
                                break;
                            case "en-US":
                                idioma.SelectedIndex = 1;
                                break;
                            case "es-ES":
                                idioma.SelectedIndex = 2;
                                break;
                        }
                    }
                }
                catch(Exception ex)
                {

                }
                finally { connection.Close(); } 
             
            }
            else
            {
                login login = new login();
                this.Content = login;

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new home());

        }
        private void pt(object sender, RoutedEventArgs e)
        {
            mudaridioma("pt-BR");
            idioma.SelectedIndex = 0;

        }
        private void en(object sender, RoutedEventArgs e)
        {

            mudaridioma("en-US");
            idioma.SelectedIndex = 1;

        }
        private void es(object sender, RoutedEventArgs e)
        {
            mudaridioma("es-ES");
            idioma.SelectedIndex = 2;
        }
        private void config_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new config());

        }
        private void mudaridioma(string idioma)
        {

            ResourceManager rs = new ResourceManager("prospecplus_interface.Resources.Strings", Assembly.GetExecutingAssembly());


            Thread t = new Thread(() =>
            {

                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    try
                    {
                        string connectionString = "Data Source=dados.sqlite;Version=3;";
                        SQLiteConnection connection = new SQLiteConnection(connectionString);
                        SQLiteCommand command = new SQLiteCommand(connection);
                        connection.Open();

                        command.CommandText = $"UPDATE config SET idioma = '{idioma}'";
                        command.ExecuteNonQuery();
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(idioma);
                        MessageBox.Show(rs.GetString("sucesso_troca_idioma"));

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(rs.GetString("error_troca_idioma"));
                    }




                });
            }
         );

            t.Start();
          

        }
    }
    }
