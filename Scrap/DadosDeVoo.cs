using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraping
{
    public class DadosDeVoo
    {
        public string Data { get; set; }
        public string HorarioPartida { get; set; }
        public string AeroportoPartida { get; set; }
        public string HorarioChegadaDestino { get; set; }
        public string AeroportoDestino { get; set; }
        public string Duracao { get; set; }
        public string TipoTrajeto { get; set; }
        public string Preco { get; set; }
        public string Moeda { get; set; }
    }
}
