// See https://aka.ms/new-console-template for more information
using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using WebScraping;

//var web = new WebScraper();

//web.BuildData("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");

var latam = new LatamScraper();

//latam.MontarPesquisa();

Console.WriteLine("Digite Origem:");
var origem = Console.ReadLine();
Console.WriteLine("Digite Destino:");
var destino = Console.ReadLine();

Console.WriteLine("Digite Data (dd/mm/yyy):");
var data = Console.ReadLine();

var pesquisou = latam.MontarPesquisa("https://latampass.latam.com/pt_br/passagens", origem, destino, data);
if (pesquisou)
    latam.MontarListagem();
else
    Console.WriteLine("Pesquisa não finalizada com sucesso");


Console.WriteLine("Concluído");