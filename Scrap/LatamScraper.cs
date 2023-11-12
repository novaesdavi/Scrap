using System.Data;
using WebScraping.Model;
using EasyAutomationFramework;
using OpenQA.Selenium;
using EasyAutomationFramework.Model;
using OpenQA.Selenium.Chrome;

namespace WebScraping
{
    public class LatamScraper : Web
    {
        PesquisaLatam pesquisa = null;
        ResultadoPesquisaLatam resultadoPesquisa = null;
        public LatamScraper()
        {
            
        }
        private void InitBrowser(string link)
        {
            var options = new ChromeOptions();
            options.DebuggerAddress = "127.0.0.1:9100";
            driver = new ChromeDriver(options);
            Task.Delay(5000).Wait();
            driver.Url = link;

            pesquisa = new PesquisaLatam(driver);
            resultadoPesquisa = new ResultadoPesquisaLatam(driver);
        }


        public bool MontarPesquisa(string link)
        {
            InitBrowser(link);
            Task.Delay(5000).Wait();
            
            pesquisa.ConfigurarSomenteIda();
            pesquisa.ConfigurarOrigem();
            pesquisa.ConfigurarDestino();

            string dia = "11";
            string mesAno = "Novembro 2023";
            pesquisa.ConfigurarData(dia, mesAno);

            Task.Delay(2000).Wait();

            GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element
                .FindElement(By.ClassName("btn-latam"))
                .Click();
            return true;
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

        public void MontarPesquisa()
        {
            var teste = new ResultadoPesquisaLatam();
            teste.MontarLista();
        }
    }
}
