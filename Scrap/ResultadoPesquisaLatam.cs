using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using java.sql;
using javax.lang.model.util;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraping.Model;
using static sun.awt.image.ImageWatched;

namespace WebScraping
{
    public class ResultadoPesquisaLatam : Web
    {
        public ResultadoPesquisaLatam()
        {
            InitBrowser();
        }

        private void InitBrowser()
        {
            var options = new ChromeOptions();
            options.DebuggerAddress = "127.0.0.1:9100";
            driver = new ChromeDriver(options);
            Task.Delay(5000).Wait();
        }

        public ResultadoPesquisaLatam(IWebDriver _driver)
        {
            driver = _driver;
        }
        public void MontarLista()
        {

            var items = new List<DadosDeVoo>();
            for (int i = 0; i <= 11; i++)
            {
                try
                {
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                    OrganizarLista();
                    items.AddRange(ObterListaDadosVoo());
                }
                catch (Exception)
                {
                    driver.SwitchTo().Window(driver.WindowHandles[1]);
                    OrganizarLista();
                    items.AddRange(ObterListaDadosVoo());
                }

                ProximaData();

                Task.Delay(5000).Wait();
            }

            SalvarDados(items);
        }

        private void OrganizarLista()
        {
            var ListaOrganizacao = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/div[3]/div/div/button").element;
            ListaOrganizacao.Click();

            var ItemListaOrganizacao = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/div[3]/div/div/div/div[1]/menu/li[2]").element;
            ItemListaOrganizacao.Click();
            Task.Delay(2000).Wait();
        }

        private void ProximaData()
        {
            var proxData = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/div[4]/div/ol/div/div/li[4]").element;

            if (proxData != null)
                proxData.Click();
        }

        private List<DadosDeVoo> ObterListaDadosVoo()
        {
            ReadOnlyCollection<IWebElement> elements;
            Task.Delay(5000).Wait();
            var items = new List<DadosDeVoo>();
            Console.WriteLine("Iniciando Scrapping em Resultados");
            for (int lista = 1; lista < 6; lista++)
            {
                try
                {
                    var item = new DadosDeVoo();
                    //for (int cardVoo = 1; cardVoo < 2; cardVoo++)
                    //{
                    //    item.HorarioPartida = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/ol/li[{lista}]/div/div/div[{cardVoo}]/div[2]/div[1]/div[1]/span[1]").element.Text;
                    //    item.AeroportoPartida = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/ol/li[{lista}]/div/div/div[{cardVoo}]/div[2]/div[1]/div[1]/span[2]").element.Text;

                    //}
                    var teste = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/ol/li[{lista}]").element
                                                    .FindElement(By.ClassName("displayAmount"));
                    var teste2 = teste.FindElements(By.XPath(".//span"));

                    item.Preco = teste2[0].Text;
                    item.Moeda = teste2[1].Text;
                    item.Data = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/div[4]/div/ol/div/div/li[3]/button/span").element.Text;
                    item.HorarioPartida = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/ol/li[{lista}]/div/div/div[1]/div[2]/div[1]/div[1]/span[1]").element.Text;
                    item.AeroportoPartida = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/ol/li[{lista}]/div/div/div[1]/div[2]/div[1]/div[1]/span[2]").element.Text;
                    item.HorarioChegadaDestino = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/ol/li[{lista}]/div/div/div[1]/div[2]/div[1]/div[3]/span[1]").element.Text;
                    item.AeroportoDestino = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/ol/li[{lista}]/div/div/div[1]/div[2]/div[1]/div[3]/span[2]").element.Text;
                    item.Duracao = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/ol/li[{lista}]/div/div/div[1]/div[2]/div[1]/div[2]/span[2]").element.Text;
                    item.TipoTrajeto = GetValue(TypeElement.Xpath, $"/html/body/div[1]/div[1]/main/div/div/div/div/ol/li[{lista}]/div/div/div[2]/div[1]/a/span").element.Text;




                    items.Add(item);
                }
                catch (Exception)
                {
                    if (lista > 1)
                        continue;
                    else
                        throw;

                }


            }

            return items;
        }

        private void SalvarDados(IEnumerable<DadosDeVoo> items)
        {
            string folderpath = @"C:\Users\dnova\.gitRepositories\Scrap\Scrap";
            var dados = new List<DataTables>()
                    {
                        new DataTables("tablets", Base.ConvertTo(items.ToList()))
                    };

            var paramss = new ParamsDataTable("Dados", folderpath, dados);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = folderpath,
                FileName = "explorer.exe"
            };
            Base.GenerateExcel(paramss);


            Process.Start(startInfo);

            

        }
    }
}
