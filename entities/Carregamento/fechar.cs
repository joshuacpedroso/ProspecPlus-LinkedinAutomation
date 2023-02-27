using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace prospecplus_interface.entities.Carregamento
{
    class fechar
    {
        public static void close()
        {
            var tela = Application.Current.Windows.OfType<Window>().First();
            tela.Close();
        }
    }
}
