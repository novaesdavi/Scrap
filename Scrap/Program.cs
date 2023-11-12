// See https://aka.ms/new-console-template for more information
using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using WebScraping;

//var web = new WebScraper();

//web.BuildData("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");

var latam = new LatamScraper();

latam.MontarPesquisa();

//var pesquisou = latam.MontarPesquisa("https://latampass.latam.com/pt_br/passagens");
//if (pesquisou)
//    latam.MontarPesquisa();
//else
//    Console.WriteLine("Pesquisa não finalizada com sucesso");


Console.WriteLine("Concluído");