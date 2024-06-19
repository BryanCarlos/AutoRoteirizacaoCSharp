using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRoteirizacao
{
    internal class Peca
    {
        public string qtd;
        public string descricao;
        public string codigo;
        public string observacoes;

        public Peca(string qtd, string descricao, string codigo, string observacoes)
        {
            this.qtd = qtd;
            this.descricao = descricao;
            this.codigo = codigo;
            this.observacoes = observacoes;
        }
    }
}
