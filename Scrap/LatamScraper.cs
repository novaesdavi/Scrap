using System.Data;
using WebScraping.Model;
using EasyAutomationFramework;
using OpenQA.Selenium;
using EasyAutomationFramework.Model;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static sun.awt.image.ImageWatched;
using java.sql;

namespace WebScraping
{
    public class LatamScraper : Web
    {
        public LatamScraper()
        {
        }
        //cd "C:\Program Files (x86)\Google\Chrome\Application"
        // .\chrome.exe --remote-debugging-port=9100 --user-data-dir="C:\Users\dnova\AppData\Local\Google\Chrome\User Data\Default\Cache\Cache_Data"
        private void InitBrowser(string link)
        {
            var options = new ChromeOptions();
            options.DebuggerAddress = "127.0.0.1:9100";
            driver = new ChromeDriver(options);
            Task.Delay(5000).Wait();
            driver.Url = link;
            

        }


        public void BuildData(string link)
        {
            InitBrowser(link);
            Task.Delay(5000).Wait();
            
            var items = new List<Item>();
            ConfigurarSomenteIda();
            ConfigurarOrigem();
            ConfigurarDestino();

            string dia = "11";
            string mesAno = "Novembro 2023";
            ConfigurarData(dia, mesAno);

            Task.Delay(2000).Wait();

            GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element
                .FindElement(By.ClassName("btn-latam"))
                .Click();


            SalvarDados(items);

        }

        private void ConfigurarSomenteIda()
        {
            const string somenteIda = "OW";
            var campoIdaVolta = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]/div[1]/div[1]/div/div[1]").element;
            Task.Delay(2000).Wait();
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

        private void ConfigurarData(string dia, string mesAno)
        {
            var dataIda = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element.FindElement(By.Id("departure"));
            Task.Delay(2000).Wait();
            dataIda.Click();
            Task.Delay(2000).Wait();

            SelecionarCalendario(dia, mesAno);
        }

        private void SelecionarCalendario(string dia, string mesAno)
        {
            var mesTabelaEscolha = GetValue(TypeElement.Xpath, $"/html/body/div/div[2]/div[1]/table/thead/tr[1]/th[2]").Value;
            var mesContrado = false;
            while (mesContrado == false)
            {
                if (mesTabelaEscolha == mesAno)
                {
                    mesContrado = true;
                    SelecionarDiaCalendario(dia);
                }
                else
                    GetValue(TypeElement.Xpath, $"/html/body/div/div[3]/div[1]/table/thead/tr[1]/th[3]").element.Click();
            }
        }

        private void SelecionarDiaCalendario(string diaPesquisa)
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

        private void ConfigurarOrigem()
        {
            var aeroportoOrigem = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element
                                        .FindElement(By.Id("irportorigemtext"));
            aeroportoOrigem.Click();
            aeroportoOrigem.SendKeys("SAO");

            Task.Delay(5000).Wait();
            var aeroportoOrigemLista = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]/div[3]/div[1]/div/div[2]/div/div[1]").element;

            aeroportoOrigemLista.Click();
        }

        private void ConfigurarDestino()
        {
            var aeroportoOrigem = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element
                                        .FindElement(By.Id("irportdestinationtext"));
            aeroportoOrigem.Click();
            aeroportoOrigem.SendKeys("Natal");

            Task.Delay(2000).Wait();
            var aeroportoOrigemLista = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]/div[3]/div[2]/div/div[2]/div/div[1]").element;

            aeroportoOrigemLista.Click();
        }

        private void SalvarDados(IEnumerable<Item> items)
        {

            var dados = new List<DataTables>()
                    {
                        new DataTables("tablets", Base.ConvertTo(items.ToList()))
                    };

            var paramss = new ParamsDataTable("Dados", @"C:\Users\dnova\source\repos\WebScraping\WebScraping", dados);

            Base.GenerateExcel(paramss);

        }
    }
}
