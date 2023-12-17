using System;
using CardGame.Models;
using CardGame.Enumeradores;
using CardGame.Interfaces;

namespace CardGame.Models
{
    public class BlackJackDealer : IDealer
    {
        public void BarajearDeck()
        {
            throw new NotImplementedException();
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