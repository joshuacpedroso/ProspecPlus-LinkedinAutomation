using MySql.Data.MySqlClient;
using prospecplus_interface.entities.Config;
using prospecplus_interface.entities.Views.Modals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace prospecplus_interface.entities.Carregamento
{
    internal class atualizacao : mysqlconnection
    {
        public atualizacao()
        {
            var connection = new MySqlConnection(conexao);
            var command = connection.CreateCommand();
            try
            {
                connection.Open();
                command.CommandText = "select * from `atualizacao`";
                var dados = command.ExecuteReader();
                if (dados.Read())
                {

                    Assembly assembly = Assembly.GetExecutingAssembly();
                    FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                    string version = fileVersionInfo.ProductVersion;
                    if (dados["versao"].ToString() != version)
                    {
                        MessageBoxResult result = MessageBox.Show("Esse programa está desatualizado, atualize ele em nosso site.", "Atualização", MessageBoxButton.OK);

                    }else
                    {
                       // changelog changelog = new changelog();
                        //changelog.ShowDialog();
                    }

                }
                dados.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro do Banco de dados " + ex, "Aviso");

            }
            finally
            {
                connection.Close();

            }
        }
    }
}
