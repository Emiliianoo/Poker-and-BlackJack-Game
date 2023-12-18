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

         private List<ICarta> _cartas;
        public List<ICarta> Cartas
        {
            get
            {
                return _cartas;
            }
            set
            {
                if(value == null)
                {
                    throw new Exception("La lista de cartas no puede ser nula");
                }
                _cartas = value;
            }
        }

        //Se crean los constructores de la clase
        public BlackJackDealer()
        {
            _deck = new Deck();
            Cartas = new List<ICarta>();
        }

        public BlackJackDealer(IDeckDeCartas deck)
        {
            this._deck = deck;
            Cartas = new List<ICarta>();
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
             //Devuelve la carta que se encuentra en el indice indicado si es que este existe
            if (indiceCarta < Cartas.Count && indiceCarta >= 0)
            {
                //Obtener la carta que se encuentra en el indice indicado            
                ICarta cartaADevolver = Cartas[indiceCarta];         

                //Quitar la carta de la lista de cartas del jugador
                Cartas.RemoveAt(indiceCarta);

                //Devolver la carta
                return cartaADevolver;
            }
            else
            {
                throw new Exception("El indice de la carta para devolver no existe");
            }
        }

        public List<ICarta> DevolverTodasLasCartas()
        {
            // Devuelve todas las cartas del jugador si es que este tiene cartas
            if (Cartas.Count == 0)
            {
                throw new Exception("El jugador no tiene cartas para devolver");
            }
            else
            {
                // Obtener todas las cartas del jugador
                List<ICarta> cartasADevolver = new List<ICarta>(Cartas);

                // Quitar todas las cartas de la lista de cartas del jugador
                Cartas.RemoveAll(carta => true);

                RecogerCartas(cartasADevolver);

                // Devolver todas las cartas
                return cartasADevolver;
            }
        }

        public ICarta MostrarCarta(int indiceCarta)
        {
            //Muestra la carta que se encuentra en el indice indicado si es que este existe
            if (indiceCarta < Cartas.Count)
            {
                //Mostrar la carta en la consola
                Console.WriteLine(Cartas[indiceCarta].Figura + " de " + Cartas[indiceCarta].Valor);
                return Cartas[indiceCarta];
            }
            else
            {
                throw new Exception("El indice de la carta para mostrar no existe");
            }
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