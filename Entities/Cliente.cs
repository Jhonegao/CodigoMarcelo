using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CNH{ get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }

    }
}
