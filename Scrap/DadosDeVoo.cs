using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraping
{
    public class DadosDeVoo
    {
        public string Data { get; internal set; }
        public string HorarioPartida { get; set; }
        public string AeroportoPartida { get; set; }
        public string HorarioChegadaDestino { get; set; }
        public string AeroportoDestino { get; set; }
        public string Duracao { get; internal set; }
        public string TipoTrajeto { get; internal set; }
        public string Preco { get; internal set; }
        public string Moeda { get; internal set; }
    }
}
