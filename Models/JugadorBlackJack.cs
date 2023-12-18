using System;
using CardGame.Interfaces;

namespace CardGame.Models
{
    public class JugadorBlackJack : IJugador
    {

         private string _nombre;        
        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("El Nombre no puede ser espacios en blanco");
                }
                _nombre = value;
            }
        }

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

        private IDealer _dealer;
        public IDealer Dealer
        {
            get
            {
                return _dealer;
            }
            set
            {
                if(value == null)
                {
                    throw new Exception("El dealer no puede ser nulo");
                }
                _dealer = value;
            }
        }

        public JugadorBlackJack(string nombre, IDealer dealer)
        {
            Nombre = nombre;
            Cartas = new List<ICarta>();
            Dealer = dealer;            
        }


        public ICarta DevolverCarta(int indiceCarta)
        {
           //Devuelve la carta que se encuentra en el indice indicado si es que este existe
            if (indiceCarta < Cartas.Count)
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

                Dealer.RecogerCartas(cartasADevolver);

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

