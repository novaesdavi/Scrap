using System.Data;
using WebScraping.Model;
using EasyAutomationFramework;
using OpenQA.Selenium;
using EasyAutomationFramework.Model;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static sun.awt.image.ImageWatched;

namespace WebScraping
{
    public class LatamScraper : Web
    {
        public LatamScraper()
        {
        }

        private void StartBrowser()
        {
            IWebDriver driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = link;

            driver.Manage().Window.Maximize();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Task.Delay(5000).Wait();

        }
        public void BuildData(string link)
        {

            StartBrowser(TypeDriver.GoogleChorme);

            var items = new List<Item>();

            Navigate(link);
            ConfigurarOrigem();
            ConfigurarDestino();

            string dia = "11";
            string mesAno = "Novembro 2023";
            ConfigurarData(dia, mesAno);

            Task.Delay(2000).Wait();

            //GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element
            //    .FindElement(By.Id("arrival"))
            //    .SendKeys("09/12/2023");
            //Task.Delay(2000).Wait();

            GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element
                .FindElement(By.ClassName("btn-latam"))
                .Click();


            SalvarDados(items);

        }

        private void ConfigurarData(string dia, string mesAno)
        {
            Task.Delay(2000).Wait();
            var dataIda = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element.FindElement(By.Id("departure"));
            Task.Delay(2000).Wait();
            dataIda.Click();
            Task.Delay(2000).Wait();

            var mesTabelaEscolha = GetValue(TypeElement.Xpath, $"/html/body/div/div[2]/div[1]/table/thead/tr[1]/th[2]").Value;
            var mesContrado = false;
            while (mesContrado == false)
            {
                if (mesTabelaEscolha == mesAno)
                {
                    mesContrado = true;
                    SelecionarDataIda(dia);
                }
                else
                    GetValue(TypeElement.Xpath, $"/html/body/div/div[3]/div[1]/table/thead/tr[1]/th[3]").element.Click();
            } 
        }

        private void SelecionarDataIda(string diaPesquisa)
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

            Task.Delay(2000).Wait();
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
