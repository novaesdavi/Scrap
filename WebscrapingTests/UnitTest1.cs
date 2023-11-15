using WebScraping;
using static com.sun.management.VMOption;

namespace WebscrapingTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TesteIntegrado_Pesquisa_E2E()
        {
            string origem = "SAO";
            string destino = "Natal";
            string data = "11/03/2024";

            var latam = new LatamScraper();

            var pesquisou = latam.MontarPesquisa("https://latampass.latam.com/pt_br/passagens", origem, destino, data);

            Assert.IsTrue(pesquisou);
        }


        [Test]
        public void TesteIntegrado_Obterresultados_E2E()
        {
            var teste = new ResultadoPesquisaLatam();
            teste.MontarLista();
        }
    }
}