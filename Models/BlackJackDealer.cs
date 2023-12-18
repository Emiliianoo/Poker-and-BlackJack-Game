using System;
using CardGame.Models;
using CardGame.Enumeradores;
using CardGame.Interfaces;

namespace CardGame.Models
{
    public class BlackJackDealer : IDealer, IJugador
    {

        /*-_-_-_-_-_-_-_-_-_-_-_-_ Comportamientos de Dealer -_-_-_-_-_-_-_-_-_-_-_-_*/

        //Se crea una variable de tipo IDeckDeCartas para poder utilizar los metodos de la clase Deck
        private IDeckDeCartas _deck;

        //Se crean los constructores de la clase
        public BlackJackDealer()
        {
            _deck = new Deck();
        }

        public BlackJackDealer(IDeckDeCartas deck)
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
            var cartasARepartir = new List<ICarta>();
            for(int i = 0; i < numeroDeCartas; i++)
            {
                cartasARepartir.Add(_deck.SacarCarta(0));
            }

            return cartasARepartir;
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