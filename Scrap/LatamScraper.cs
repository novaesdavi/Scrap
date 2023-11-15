using System.Data;
using WebScraping.Model;
using EasyAutomationFramework;
using OpenQA.Selenium;
using EasyAutomationFramework.Model;
using OpenQA.Selenium.Chrome;
using java.text;
using System.Diagnostics;
using System.Reflection;

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

            try
            {
                var options = new ChromeOptions();
                options.DebuggerAddress = "127.0.0.1:9100";
                driver = new ChromeDriver(options);
            }
            catch (Exception ex)
            {

                Process.Start("C:\\Users\\dnova\\.gitRepositories\\Scrap\\Scrap\\InitChromeOnDebugMode.bat");
                Task.Delay(5000).Wait();
                var options = new ChromeOptions();
                options.DebuggerAddress = "127.0.0.1:9100";
                driver = new ChromeDriver(options);
            }


            Task.Delay(5000).Wait();
            driver.Url = link;

            pesquisa = new PesquisaLatam(driver);
            resultadoPesquisa = new ResultadoPesquisaLatam(driver);
        }


        public bool MontarPesquisa(string link, string origem, string destino, string data)
        {
            DateTime dateTime = DateTime.ParseExact(data, "dd/MM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
            if (dateTime < DateTime.Now)
            {
                Console.WriteLine("Pesquisa Iniciada");
                return false;
            }
            else
            {
                string mesAno = null;
                string dia = data.Substring(0, 2);
                if (data.Substring(3, 2) == "03")
                    mesAno = $"Março {data.Substring(6, 4)}";
                else
                    mesAno = $"{(Mes)Convert.ToInt32(data.Substring(3, 2))} {data.Substring(6, 4)}";

                InitBrowser(link);
                Task.Delay(5000).Wait();

                Console.WriteLine("Pesquisa Iniciada");

                pesquisa.ConfigurarSomenteIda();
                pesquisa.ConfigurarOrigem(origem);
                pesquisa.ConfigurarDestino(destino);
                pesquisa.ConfigurarData(dia, mesAno);

                Task.Delay(2000).Wait();

                GetValue(TypeElement.Xpath, "/html/body/main/div/div[2]/div/div/div/div/div[2]").element
                    .FindElement(By.ClassName("btn-latam"))
                    .Click();
                Console.WriteLine("Pesquisa Concluída");
                return true;
            }
        }

        public void MontarListagem()
        {
            resultadoPesquisa.MontarLista();
        }
    }
}
