using prospecplus_interface.entities.Config;
using prospecplus_interface.entities.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
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

namespace prospecplus_interface.entities.Views.subs
{
    /// <summary>
    /// Interação lógica para home.xam
    /// </summary>
    public partial class home : Page
    {
        private bool sales2;
        private sales sales;
        public home()
        {
            InitializeComponent();
            configuracao configuracao = new configuracao();
            ResourceManager rs = new ResourceManager("prospecplus_interface.Resources.Strings", Assembly.GetExecutingAssembly());

            iniciar.Content = rs.GetString("iniciar");
            continuar.Content = rs.GetString("continuar");
            pararButton.Content = rs.GetString("parar");
            int quantidade = configuracao.getQuantidade();
            int execucao = configuracao.getExecucao();
            string mensagem = configuracao.getMensagem();
            string email = configuracao.getEmail();
            sales2 = configuracao.getSales();
            sales = new sales(quantidade, mensagem, execucao, cronometro, email );
        }

        private void iniciar_Click(object sender, RoutedEventArgs e)
        {
            if(sales2 == true)
            {
                try
                {
                    sales.iniciar();
                    continuar.IsEnabled = true;
                    pararButton.IsEnabled = true;
                }catch(Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro", "ERROR");
                }

            }
        }

        private void continuar_Click(object sender, RoutedEventArgs e)
        {
            Thread t1 = new Thread(new ThreadStart(executar));
            t1.Start();
            continuar.IsEnabled = false;
        }
        private void executar()
        {
            if (sales2 == true)
            {
                try
                {
                    sales.continuar();
                }
                catch (Exception ex)
                {

                }
            }
        }
       

        private void pararButton_Click(object sender, RoutedEventArgs e)
        {
            if (sales2 == true)
            {
                try
                {
                    sales.parar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Falha ao encerrar, feche o programa ", "ERROR");
                }
            }
        }
    }
}
