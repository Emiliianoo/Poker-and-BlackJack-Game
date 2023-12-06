using CardGame.Enumeradores;
using CardGame.Interfaces;

namespace CardGame.Models
{
    public class Card : ICarta
    {
        public Card(FigurasCartasEnum figura, ValoresCartasEnum valor)
        {
            throw new NotImplementedException();
        }

        public FigurasCartasEnum Figura => throw new NotImplementedException();

        public ValoresCartasEnum Valor => throw new NotImplementedException();
    }
}