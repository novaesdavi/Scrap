using System.Data;
using WebScraping.Model;
using EasyAutomationFramework;
using OpenQA.Selenium;
using EasyAutomationFramework.Model;

namespace WebScraping
{
    public class LatamScraper : Web
    {
        public LatamScraper()
        {
        }

        public void BuildData(string link)
        {
            StartBrowser(TypeDriver.GoogleChorme);

            var items = new List<Item>();

            Navigate(link);
            ConfigurarOrigem();
            ConfigurarDestino();

            SelecionarDataIda("11");

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

        private void SelecionarDataIda(string diaPesquisa)
        {
            var dataIda = GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element.FindElement(By.Id("departure"));
            dataIda.Click();

            Task.Delay(2000).Wait();
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
