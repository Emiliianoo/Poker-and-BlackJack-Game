using System;
using CardGame.Interfaces;
using CardGame.Models;
using CardGame.Enumeradores;

namespace CardGame.Models
{
    public class JuegoBlackJack : IJuego
    {
        private List<IJugador> _jugadores;
        private List<ICarta> _cartasDelDealer;

        public IDealer Dealer { get; private set; }

        public bool JuegoTerminado => throw new NotImplementedException();

        public JuegoBlackJack()
        {
            _jugadores = new List<IJugador>();
            //Dealer = new BlackJackDealer();
        }

        public void AgregarJugador(IJugador jugador)
        {
            if(_jugadores.Contains(jugador))
            {
                throw new Exception("El jugador ya existe en el juego");
            }
            else
            {
                _jugadores.Add(jugador);
            }
        }

        public void IniciarJuego()
        {
            // El dealer barajea las cartas
            Dealer.BarajearDeck();

            // El dealer reparte las cartas a los jugadores
            foreach(var jugador in _jugadores)
            {
                jugador.ObtenerCartas(Dealer.RepartirCartas(2));
            }

            // El dealer reparte las cartas a si mismo
            _cartasDelDealer = Dealer.RepartirCartas(2);

            // Se juega la ronda
            JugarRonda();
        }

        public void JugarRonda()
        {
            // Cada jugador juega su turno
            foreach(var jugador in _jugadores)
            {
                jugador.RealizarJugada();
            }

            // El dealer juega su turno
            // TODO: Implementar

            // Se muestra el ganador
            MostrarGanador();
        }

        public void MostrarGanador()
        {
            int puntajeDealer = CalcularPuntaje(_cartasDelDealer);
            bool dealerSePaso = puntajeDealer > 21;

            Console.WriteLine($"El dealer tiene {puntajeDealer} puntos y ");
            foreach(var jugador in _jugadores)
            {
                int puntajeJugador = CalcularPuntaje(jugador.DevolverTodasLasCartas());

                if(puntajeJugador > 21) 
                {
                    Console.WriteLine($"El jugador {jugador} perdio");
                }
                else
                {
                    if(dealerSePaso || puntajeJugador > puntajeDealer)
                    {
                        Console.WriteLine($"El jugador {jugador} gano");
                    }
                    else
                    {
                        Console.WriteLine($"El jugador {jugador} perdio");
                    }
                }
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
    }
}