using MySql.Data.MySqlClient;
using prospecplus_interface.entities.Config;
using prospecplus_interface.entities.Utils.date;
using prospecplus_interface.entities.Views;
using System;
using System.Windows;

namespace prospecplus_interface.entities.Login
{
    class loginClass : mysqlconnection
    {
        public bool erro = false;
        public string mensagem;
        public string nome;
        public int id;
        public loginClass(string user, string pass)
        {
            date data = new date();
            var connection = new MySqlConnection(conexao);
            var command = connection.CreateCommand();
            try
            {
                connection.Open();
                command.CommandText = $"select * from `users` where `email` = '{user}' and `senha` = '{pass}'";

                var result = command.ExecuteScalar();
                var vencimento = command.ExecuteReader();

                if (result != null && vencimento.Read())
                {
                    var data_vencimento = Convert.ToDateTime(vencimento["validade"]).Date;
                    var vencimento2 = DateTime.Compare(data_vencimento, date.data());
                    nome = vencimento["nome"].ToString();
                    id = int.Parse(vencimento["id"].ToString());
                    vencimento.Close();
                    if (vencimento2 < 0)
                    {
                        error("Mensalidade não paga.");


                    }
                    else if (vencimento2 >= 0)
                    {
                      
                    }


                }
                else
                {
                    error("Usuário inválido"); 
                }

            }
            catch (Exception ex)
            {
                error("Erro no Banco de Dados");

            }
            finally
            {
                connection.Close();
              

            }

        }
     public void error(string mensagem)
        {
            erro = true;
            this.mensagem = mensagem;
        }

    

}
}
