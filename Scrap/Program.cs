// See https://aka.ms/new-console-template for more information
using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using WebScraping;

var web = new WebScraper();

var tablets = web.GetData("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");

var dados = new List<DataTables>()
    {
        new DataTables("tablets", tablets)
    };

var paramss = new ParamsDataTable("Dados", @"C:\Users\dnova\source\repos\WebScraping\WebScraping", dados);

Base.GenerateExcel(paramss);

