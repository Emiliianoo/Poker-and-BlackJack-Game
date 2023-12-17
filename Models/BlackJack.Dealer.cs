using System;
using CardGame.Models;
using CardGame.Enumeradores;
using CardGame.Interfaces;

namespace CardGame.Models
{
    public class BlackJackDealer : IDealer
    {
        private IDeckDeCartas _deck;

        public BlackJackDealer()
        {
            _deck = new Deck();
        }

        public void BarajearDeck()
        {
            _deck.BarajearDeck();
        }

        public void RecogerCartas(List<ICarta> cartas)
        {
            throw new NotImplementedException();
        }

        public List<ICarta> RepartirCartas(int numeroDeCartas)
        {
            throw new NotImplementedException();
        }
    }
}