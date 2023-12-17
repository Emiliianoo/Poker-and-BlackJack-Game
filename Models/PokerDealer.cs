using CardGame.Interfaces;

namespace CardGame.Models
{
    public class PokerDealer : IDealer
    {
        private IDeckDeCartas _deck;

        public PokerDealer()
        {
            _deck = new Deck();
        }

        public PokerDealer(IDeckDeCartas deck)
        {
            this._deck = deck;
        }

        public void BarajearDeck()
        {
            _deck.BarajearDeck();
        }

        public void RecogerCartas(List<ICarta> cartas)
        {
            _deck.MeterCarta(cartas);
        }

        public List<ICarta> RepartirCartas(int numeroDeCartas)
        {
            var cards = new List<ICarta>();
            for(int i = 0; i < numeroDeCartas; i++)
            {
                cards.Add(_deck.SacarCarta(0));
            }

            return cards;
        }
    }
}