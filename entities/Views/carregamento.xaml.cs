using prospecplus_interface.entities.Carregamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
using System.Windows.Shapes;

namespace prospecplus_interface.entities.Views
{
    /// <summary>
    /// Lógica interna para carregamento.xaml
    /// </summary>
    public partial class carregamento : Window
    {
        public carregamento()
        {
            InitializeComponent();
           var admin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            this.AllowsTransparency = true;
            this.Background = Brushes.Transparent;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            new atualizacao();
            if (admin)
            {
                Thread t = new Thread(() =>
               {
                   verificamento verificamento = new verificamento();

                   Application.Current.Dispatcher.Invoke((Action)delegate
                   {
                       if (verificamento.verificarlogin())
                       {
                           verificamento.verificarConfig();
                           inicio inicio = new inicio(false);
                           inicio.Show();
                           entities.Carregamento.fechar.close();
                       }
                       else
                       {
                           verificamento.verificarConfig();
                           inicio inicio = new inicio(true);
                           inicio.Show();
                           entities.Carregamento.fechar.close();

                       }



                   });
                   Thread.Sleep(4000);

               }
           );

                t.Start();
                
            }else
            {
                MessageBox.Show("Execute o programa em modo administrador!");
                entities.Carregamento.fechar.close();

            }
        }
    }
}
