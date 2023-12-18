using System;
using CardGame.Interfaces;
using CardGame.Enumeradores;

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

                Dealer.RecogerCartas(cartasADevolver);

                // Devolver todas las cartas
                return cartasADevolver;
            }
        }

        public ICarta MostrarCarta(int indiceCarta)
        {
             //Muestra la carta que se encuentra en el indice indicado si es que este existe
            if (indiceCarta < Cartas.Count && indiceCarta >= 0)
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
             //Muestra todas las cartas del jugador si es que este tiene cartas
            if (Cartas.Count == 0)
            {
                throw new Exception("El jugador no tiene cartas para mostrar");
            }
            else
            {
                Console.WriteLine($"El jugador {Nombre} tiene las siguientes cartas: ");
                //Mostrar todas las cartas en la consola
                foreach (ICarta carta in Cartas)
                {
                    Console.WriteLine(carta.Figura + " de " + carta.Valor);
                }
                return Cartas;
            }
        }

        public void ObtenerCartas(List<ICarta> cartas)
        {
            //Si las cartas recibidas no estan vacias se agregan a la lista de cartas del jugador
            if (cartas.Count != 0) {
                Cartas.AddRange(cartas);
            } else {
                throw new Exception("No hay cartas para agregar");
            }
        }

        private int CalcularPuntaje(List<ICarta> cartas)
        {
            int puntajeSinAses = 0;
            int numeroDeAses = 0;

            foreach(var carta in cartas)
            {
                bool esDiezOMayor = carta.Valor == ValoresCartasEnum.Diez || 
                                    carta.Valor == ValoresCartasEnum.Jota || 
                                    carta.Valor == ValoresCartasEnum.Reina || 
                                    carta.Valor == ValoresCartasEnum.Rey;
                
                if(esDiezOMayor)
                {
                    puntajeSinAses += 10;
                }
                else if(carta.Valor == ValoresCartasEnum.As)
                {
                    numeroDeAses++;
                }
                else
                {
                    puntajeSinAses += (int)carta.Valor;
                }
            }

            int puntajeTemporalAses = numeroDeAses * 11;

            while(puntajeTemporalAses + puntajeSinAses > 21 && numeroDeAses > 0)
            {
                puntajeTemporalAses -= 10;
                numeroDeAses--;
            }

            int puntajeFinal = puntajeSinAses + puntajeTemporalAses;

            return puntajeFinal;
        }

        public void RealizarJugada()
        {
            int puntaje = CalcularPuntaje(Cartas);    

            while(puntaje < 17)
            {
                Console.WriteLine($"El jugador {Nombre} pide una carta");
                ObtenerCartas(Dealer.RepartirCartas(1));
                puntaje = CalcularPuntaje(Cartas);
            }

            Console.WriteLine($"El jugador {Nombre} se planta");
        }
    }
}

