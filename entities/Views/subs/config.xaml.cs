using prospecplus_interface.entities.Config;
using System;
using System.Data.SQLite;
using System.Resources;

using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Threading;

namespace prospecplus_interface.entities.Views.subs
{
    /// <summary>
    /// Interação lógica para config.xam
    /// </summary>
    public partial class config : Page
    {
        private int quantidadeVar;
        private float execucao;
        private string mensagemVar;
        private int salesVar;
        private string emailVar;
        private SQLiteConnection connection;
        private SQLiteCommand command;
       
        public config()
        {
            InitializeComponent();
            ResourceManager rs = new ResourceManager("prospecplus_interface.Resources.Strings", Assembly.GetExecutingAssembly());

            string connectionString = "Data Source=dados.sqlite;Version=3;";
            connection = new SQLiteConnection(connectionString);
            command = new SQLiteCommand(connection);

            connection.Open();
            carregar();
            //Idioma
            //Titulo
            titulo_configuracao.Content = rs.GetString("titulo_configuracao");
            //SubTitulos
            subtitulo_quantidade.Content = rs.GetString("subtitulo_quantidade");
            subtitulo_conexao.Content = rs.GetString("subtitulo_conexao");
            subtitulo_mensagem.Content = rs.GetString("subtitulo_mensagem");
            subtitulo_tempo.Content = rs.GetString("subtitulo_tempo");
            aviso_mensagem.Text = rs.GetString("aviso_mensagem");
            //Personalizado 
            personalizado_text.Content = rs.GetString("personalizado_text");
            personalizado_text1.Content = rs.GetString("personalizado_text");
            //Conexões
            _30.Content = rs.GetString("_30");
            _50.Content = rs.GetString("_50");
            _80.Content = rs.GetString("_80");
            //Segundos
            _5s.Content = rs.GetString("_5s");
            _10s.Content = rs.GetString("_10s");
            _20s.Content = rs.GetString("_20s");

        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void carregar()
        {
            command.CommandText = "SELECT * FROM config";
            var reader = command.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    this.quantidadeVar = int.Parse(reader["quantidade"].ToString());
                    this.execucao = float.Parse(reader["executacao"].ToString());
                    this.mensagemVar = reader["mensagem"].ToString();
                    this.salesVar = int.Parse(reader["sales"].ToString());
                    this.emailVar = reader["email"].ToString();
                    preencher();
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro");
            }
            reader.Close();
            

        }
        private void preencher()
        {

            switch (quantidadeVar)
            {
                case 30:
                     quantidade.Text = "30";
                     _30.IsChecked = true;
                    break;
                case 50:
                    quantidade.Text = "50";
                    _50.IsChecked = true;
                    break;
                case 80:
                    quantidade.Text = "80";
                    _80.IsChecked = true;
                    break;
                default: 
                    quantidade.Text = quantidadeVar.ToString();
                    break;

            }
            switch (execucao)
            {
                case 5:
                    segundos.Text = "5";
                    _5s.IsChecked = true;
                    break;
                case 10:
                    segundos.Text = "10";
                    _10s.IsChecked = true;
                    break;
                case 20:
                    segundos.Text = "20";
                    _20s.IsChecked = true;
                    break;
                default:
                    segundos.Text = execucao.ToString();
                    break;

            }
        if(salesVar == 1)
            {
                sales.IsChecked = true;
            }else
            {
                normal.IsChecked = true;
            }
            mensagem.Text = mensagemVar;
            email.Text = emailVar;
        }
        //Checkbox Quantidade

        private void _30_Checked(object sender, RoutedEventArgs e)
        {
            quantidade.Text = "30";
            verificar(_50, _80);
        }
        private void _50_Checked(object sender, RoutedEventArgs e)
        {
            quantidade.Text = "50";
            verificar(_30, _80);
        }
        private void _80_Checked(object sender, RoutedEventArgs e)
        {
            quantidade.Text = "80";
            verificar(_30, _50);
        }
        //Checkbox Segundos
        private void _5_Checked(object sender, RoutedEventArgs e)
        {
            segundos.Text = "5";
            verificar(_10s, _20s);
        }
        private void _10_Checked(object sender, RoutedEventArgs e)
        {
            segundos.Text = "10";
            verificar(_5s, _20s);
        }
        private void _20_Checked(object sender, RoutedEventArgs e)
        {
            segundos.Text = "20";
            verificar(_10s, _5s);
        }

        //Normal ou Sales
        private void sales_Checked(object sender, RoutedEventArgs e)
        {
            verificar(normal);
        }

        private void normal_Checked(object sender, RoutedEventArgs e)
        {
            verificar(sales);
        }
        //verificar se os checkbox tão pressionado
        private void verificar(CheckBox primeiro, CheckBox segundo)
        {
            if ((bool)primeiro.IsChecked)
            {
                primeiro.IsChecked = false;
            }
            if ((bool)segundo.IsChecked)
            {
                segundo.IsChecked = false;
            }
        }
        //Sobrecarga
        private void verificar(CheckBox primeiro)
        {
            if ((bool)primeiro.IsChecked)
            {
                primeiro.IsChecked = false;

            }
          
        }

        //Salvamentos
        private void salvar_Click(object sender, RoutedEventArgs e)
        {
            connection.Close();
            salvar_config();
          
      

          
        }
        private void salvar_config()
        {

            var quantidadeInput = int.Parse(quantidade.Text);
            var executacaoInput = int.Parse(segundos.Text);
            var mensagemInput = mensagem.Text;
            var emailInput = email.Text;
            var salesInput = 0;
            if ((bool)sales.IsChecked)
            {
                salesInput = 1;
            }

            try
            {
                connection.Open();
                command.CommandText = $"UPDATE config SET quantidade = {quantidadeInput}, executacao = {executacaoInput}, mensagem = '{mensagemInput.Replace("'", "''")}', sales = {salesInput}, email = '{emailInput.Replace("'", "''")}'";
                command.ExecuteNonQuery();
                MessageBox.Show("Salvo com sucesso!", "SALVO");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao salvar a configuração + {ex.Message}", "ERROR");

            }
            finally { connection.Close(); }
            

           
            
            
  
           
        }

      
    }
}
