using CardGame.Enumeradores;
using CardGame.Interfaces;

namespace CardGame.Models{
    public class Carta : ICarta
    {
        public FigurasCartasEnum Figura => figura;

        public ValoresCartasEnum Valor => valor;

        private FigurasCartasEnum figura;
        private ValoresCartasEnum valor;

        public Carta (FigurasCartasEnum figura, ValoresCartasEnum valor){
            this.valor = valor;
            this.figura = figura;
        }
    }
}