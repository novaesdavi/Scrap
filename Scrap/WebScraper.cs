using System.Data;
using WebScraping.Model;
using EasyAutomationFramework;
using OpenQA.Selenium;
using EasyAutomationFramework.Model;

namespace WebScraping
{
    public class WebScraper : Web
    {
        public WebScraper()
        {
        }

        public void BuildData(string link)
        {
            StartBrowser(TypeDriver.GoogleChorme);

            var items = new List<Item>();

            Navigate(link);

            var elements = GetValue(TypeElement.Xpath, "/html/body/div[1]/div[3]/div/div[2]/div").element.FindElements(By.ClassName("thumbnail"));

            foreach (var element in elements)
            {
                var item = new Item();
                item.Title = element.FindElement(By.ClassName("title")).GetAttribute("title");
                item.Price = element.FindElement(By.ClassName("price")).Text;
                item.Description = element.FindElement(By.ClassName("description")).Text;
                items.Add(item);
            }

            SalvarDados(items);

        }
        private void SalvarDados(IEnumerable<Item> items) {

            var dados = new List<DataTables>()
                    {
                        new DataTables("tablets", Base.ConvertTo(items.ToList()))
                    };

            var paramss = new ParamsDataTable("Dados", @"C:\Users\dnova\source\repos\WebScraping\WebScraping", dados);

            Base.GenerateExcel(paramss);

        }
    }
}
