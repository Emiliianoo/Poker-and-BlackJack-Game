using System;
using CardGame.Models;
using CardGame.Enumeradores;
using CardGame.Interfaces;

namespace CardGame.Models
{
    public class BlackJackDealer : IDealer, IJugador
    {

        /*-_-_-_-_-_-_-_-_-_-_-_-_ Comportamientos de Dealer -_-_-_-_-_-_-_-_-_-_-_-_*/


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


        /*-_-_-_-_-_-_-_-_-_-_-_-_ Comportamientos de Jugador -_-_-_-_-_-_-_-_-_-_-_-_*/

        public ICarta DevolverCarta(int indiceCarta)
        {
            throw new NotImplementedException();
        }

        public List<ICarta> DevolverTodasLasCartas()
        {
            throw new NotImplementedException();
        }

        public ICarta MostrarCarta(int indiceCarta)
        {
            throw new NotImplementedException();
        }

        public List<ICarta> MostrarCartas()
        {
            throw new NotImplementedException();
        }

        public void ObtenerCartas(List<ICarta> cartas)
        {
            throw new NotImplementedException();
        }

        public void RealizarJugada()
        {
            throw new NotImplementedException();
        }


    }
}