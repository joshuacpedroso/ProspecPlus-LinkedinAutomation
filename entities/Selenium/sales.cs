using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace prospecplus_interface.entities.Selenium
{
    class sales
    {
        private ChromeDriver driver;
        private int quantidade;
        private int segundosConf;
        private TextBlock cronometro;
        private string mensagem;
        private bool ativo;
        private string email;
        private Stopwatch st;

        public sales(int quantidade, string messagem, int segundosConf, TextBlock cronometro, string email)
        {
            this.quantidade = quantidade;
            mensagem = messagem;
            this.segundosConf = segundosConf;
            this.cronometro = cronometro;
            this.email = email;
        }
        public void iniciar()
        {


            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.linkedin.com/sales/login");

        }
        public void continuar()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));
            var wait3 = new WebDriverWait(driver, new TimeSpan(0, 0, 0));

            ativo = true;
            st = new Stopwatch();
            st.Start();
            Thread t = new Thread(
              () =>
              {
                  while (ativo == true)
                  {

                      Application.Current.Dispatcher.Invoke((Action)delegate {
                          cronometro.Text = st.Elapsed.ToString();
                      });


                  }
              }
          );
            t.Start();

            int relatorio = 0;

            while (ativo == true || relatorio < quantidade)
            {
                try
                {
                    var elements = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id=\"search-results-container\"]/div/ol/li")));
                    for (int i = 0; i < elements.Count(); i++)
                    {
                        if (relatorio < quantidade)
                        {



                            var buttonmens = 1;
                            try
                            {
                                var wait2 = new WebDriverWait(driver, new TimeSpan(0, 0, segundosConf));

                                buttonmens = wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath($"/html/body/main/div[1]/div[2]/div[2]/div/ol/li[{i + 1}]/div/div/div[2]/div[2]/ul/li[2]/div[2]/button/span"))).Count();
                                var buttonmens2 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"/html/body/main/div[1]/div[2]/div[2]/div/ol/li[{i + 1}]/div/div/div[2]/div[2]/ul/li[2]/div[2]/button/span")));
                                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", buttonmens2);

                            }
                            catch (Exception e)
                            {
                                buttonmens = 0;
                            }
                            if (buttonmens == 0)
                            {

                                try
                                {
                                    var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"/html/body/main/div[1]/div[2]/div[2]/div/ol/li[{i + 1}]/div/div/div[2]/div[2]/ul/li[1]/div/button")));
                                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
                                    Task.Delay(5);
                                    try
                                    {
                                        driver.FindElement(By.XPath($"/html/body/main/div[1]/div[2]/div[2]/div/ol/li[{i + 1}]/div/div/div[2]/div[2]/ul/li[2]/a"));

                                    }
                                    catch (Exception ex)
                                    {

                                        element.Click();
                                        Task.Delay(5);
                                        //Seguir 
                                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"/html/body/main/div[1]/div[2]/div[2]/div/ol/li[{i + 1}]/div/div/div[2]/div[2]/ul/li[1]/div/div[1]/div/ul/li[1]/div"))).Click();
                                        Task.Delay(5);
                                        //Pegar o nome da pessoa

                                        var nome = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"/html/body/div[6]/div/div/div[2]/div/div[1]/div[2]/div"))).Text;
                                        //Trocar os nomes dos textos pelo nome da pessoa
                                        var mensagem2 = mensagem.Replace("{nome}", nome);
                                        //Escrever a mensagem 
                                        try
                                        {
                                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"/html/body/div[6]/div/div/div[2]/div/textarea"))).SendKeys(mensagem2);
                                            try
                                            {
                                                wait3.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"/html/body/div[6]/div/div/div[2]/div/input"))).SendKeys(email);

                                            }catch(Exception e)
                                            {
                                                Console.WriteLine("Erro");
                                            }
                                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[6]/div/div/div[3]/div/button[2]"))).Click();
                                            relatorio++;

                                        }
                                        catch (Exception e)
                                        {
                                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[6]/div/div/button"))).Click();

                                        }



                                    }
                                }
                                catch (Exception e)
                                {

                                }
                            }
                        }
                        else
                        {
                            parar();
                        }
                    }
                    var next = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/main/div[1]/div[2]/div[2]/div/div[4]/div/button[2]")));
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", next);
                    next.Click();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }

            }


        }
        public void parar()
        {
            ativo = false;
            if (st != null)
            {
                st.Stop();
            }
            driver.Close();
        }

    }
}
