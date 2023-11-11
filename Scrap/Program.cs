// See https://aka.ms/new-console-template for more information
using EasyAutomationFramework;
using EasyAutomationFramework.Model;
using WebScraping;

//var web = new WebScraper();

//web.BuildData("https://webscraper.io/test-sites/e-commerce/allinone/computers/tablets");

var latam = new LatamScraper();
latam.BuildData("https://latampass.latam.com/pt_br/passagens");

