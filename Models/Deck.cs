using CardGame.Interfaces;
using CardGame.Enumeradores;

namespace CardGame.Models
{
    public class Deck : IDeckDeCartas
    {
        private List<ICarta> _cards;
        public Deck()
        {
            _cards = new List<ICarta>();

            for(int i = 0; i < 4; i++)
            {
                for(int j = 1; j <= 13; j++)
                {
                    _cards.Add(new Carta((FigurasCartasEnum)i, (ValoresCartasEnum)j));
                }
            }
        }

        public void BarajearDeck()
        {
            for(int i = 0; i < _cards.Count; i++)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, _cards.Count);
                ICarta temp = _cards[i];
                _cards[i] = _cards[randomIndex];
                _cards[randomIndex] = temp;
            }
        }

        public void MeterCarta(ICarta carta)
        {
            _cards.Add(carta);
        }

        public void MeterCarta(List<ICarta> cartas)
        {
            _cards.AddRange(cartas);
        }

        public ICarta SacarCarta(int indiceCarta)
        {
            ICarta card = _cards[indiceCarta];
            _cards.RemoveAt(indiceCarta);
            return card;
        }

        public ICarta VerCarta(int indiceCarta)
        {
            ICarta card = _cards[indiceCarta];
            return card;
        }
    }
}