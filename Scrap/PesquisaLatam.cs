using System.Data;
using WebScraping.Model;
using EasyAutomationFramework;
using OpenQA.Selenium;
using EasyAutomationFramework.Model;
using OpenQA.Selenium.Chrome;

namespace WebScraping
{
    public class PesquisaLatam : Web
    {
        public PesquisaLatam(IWebDriver _driver)
        {
            driver = _driver;
        }
        public void ConfigurarSomenteIda()
        {
            const string somenteIda = "OW";
            var campoIdaVolta = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]/div[1]/div[1]/div/div[1]").element;
            campoIdaVolta.Click();
            Task.Delay(2000).Wait();
            var listaIdaVolta = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]/div[1]/div[1]/div/div[2]/div/div[1]/span").element;
            var valor = listaIdaVolta.GetAttribute("data-value");
            if (valor == somenteIda)
            {
                listaIdaVolta.Click();
                Task.Delay(2000).Wait();
            }

        }

        public void ConfigurarData(string dia, string mesAno)
        {
            var dataIda = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element.FindElement(By.Id("departure"));
            Task.Delay(2000).Wait();
            dataIda.Click();
            Task.Delay(2000).Wait();

            SelecionarCalendario(dia, mesAno);
        }

        public void SelecionarCalendario(string dia, string mesAno)
        {
            
            var mesContrado = false;
            while (mesContrado == false)
            {
                var mesTabelaEscolha = GetValue(TypeElement.Xpath, $"/html/body/div/div[2]/div[1]/table/thead/tr[1]/th[2]").Value;

                if (mesTabelaEscolha == mesAno)
                {
                    Console.WriteLine("Mês Encontrado");
                    mesContrado = true;
                    SelecionarDiaCalendario(dia);
                }
                else
                {
                    Console.WriteLine("Data Não Encontrado");
                    GetValue(TypeElement.Xpath, $"/html/body/div").element.FindElement(By.ClassName("next")).Click();
                    Task.Delay(2000).Wait();
                    Console.WriteLine("Proxima Data Calendario");
                }
            }
        }

        public void SelecionarDiaCalendario(string diaPesquisa)
        {
            var diaEncontrado = false;
            for (int linha = 1; linha < 6; linha++)
            {
                if (diaEncontrado) break;
                for (int coluna = 1; coluna < 8; coluna++)
                {
                    var dataCalendario = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[2]/div[1]/table/tbody/tr[{linha}]/td[{coluna}]").Value;
                    if (diaPesquisa == dataCalendario)
                    {
                        GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[2]/div[1]/table/tbody/tr[{linha}]/td[{coluna}]").element.Click();
                        diaEncontrado = true;
                        break;
                    }
                }

            }
        }

        public void ConfigurarOrigem(string origem)
        {
            var aeroportoOrigem = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element
                                        .FindElement(By.Id("irportorigemtext"));
            aeroportoOrigem.Click();
            aeroportoOrigem.SendKeys(origem);

            Task.Delay(5000).Wait();
            var aeroportoOrigemLista = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]/div[3]/div[1]/div/div[2]/div/div[1]").element;

            aeroportoOrigemLista.Click();
        }

        public void ConfigurarDestino(string destino)
        {
            var aeroportoOrigem = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element
                                        .FindElement(By.Id("irportdestinationtext"));
            aeroportoOrigem.Click();
            aeroportoOrigem.SendKeys(destino);

            Task.Delay(2000).Wait();
            var aeroportoOrigemLista = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]/div[3]/div[2]/div/div[2]/div/div[1]").element;

            aeroportoOrigemLista.Click();
        }
    }
}
