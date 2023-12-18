using System;
using CardGame.Interfaces;
using CardGame.Models;
using CardGame.Enumeradores;

namespace CardGame.Models
{
    public class JuegoBlackJack : IJuego
    {
        private List<IJugador> _jugadores;
        private int _numeroDeRonadas;

        public IDealer Dealer { get; private set; }

        public bool JuegoTerminado { get; private set;}

        public JuegoBlackJack(int nRondas)
        {
            _jugadores = new List<IJugador>();
            Dealer = new BlackJackDealer();
            _numeroDeRonadas = nRondas;
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

            int ronda = 1;
            while(!JuegoTerminado)
            {
                Console.WriteLine($"\n------------------RONDA {ronda}------------------\n");
                JugarRonda();
                _numeroDeRonadas--;
                ronda++;

                if(_numeroDeRonadas == 0) JuegoTerminado = true;
            }
        }

        public void JugarRonda()
        {
            var Dealer = this.Dealer as BlackJackDealer ?? throw new Exception("El dealer no es de tipo BlackJackDealer");

            // El dealer barajea las cartas
            Dealer.BarajearDeck();

            // El dealer reparte las cartas a los jugadores
            foreach(var jugador in _jugadores)
            {
                jugador.ObtenerCartas(Dealer.RepartirCartas(2));
            }

            // El dealer reparte las cartas a si mismo
            Dealer.ObtenerCartas(Dealer.RepartirCartas(2));

            // Cada jugador juega su turno
            foreach(var jugador in _jugadores)
            {
                jugador.RealizarJugada();
            }

            // El dealer juega su turno
            Dealer.RealizarJugada();

            // Se muestra el ganador
            MostrarGanador();
        }

        public void MostrarGanador()
        {
            var Dealer = this.Dealer as BlackJackDealer ?? throw new Exception("El dealer no es de tipo BlackJackDealer");

            Console.WriteLine($"\n------------------RESULTADOS------------------\n");

            Dealer.MostrarCartas();

            int puntajeDealer = CalcularPuntaje(Dealer.DevolverTodasLasCartas());
            bool dealerSePaso = puntajeDealer > 21;

            Console.Write($"El dealer tiene {puntajeDealer} puntos");

            if(dealerSePaso) Console.WriteLine(" y perdió.\n");
            else Console.WriteLine("\n");

            

            foreach(var jugador in _jugadores)
            {
                var jugadorActual = jugador as JugadorBlackJack ?? throw new Exception("El jugador no es de tipo JugadorBlackJack");
                
                jugadorActual.MostrarCartas();

                int puntajeJugador = CalcularPuntaje(jugador.DevolverTodasLasCartas());

                Console.WriteLine($"El jugador {jugadorActual.Nombre} tiene {puntajeJugador} puntos");

                if(puntajeJugador > 21) 
                {
                    Console.WriteLine($"El jugador {jugadorActual.Nombre} perdio");
                }
                else
                {
                    if(dealerSePaso || puntajeJugador > puntajeDealer)
                    {
                        Console.WriteLine($"El jugador {jugadorActual.Nombre} gano");
                    }
                    else if(puntajeJugador == puntajeDealer)
                    {
                        Console.WriteLine($"El jugador {jugadorActual.Nombre} empato");
                    }
                    else
                    {
                        Console.WriteLine($"El jugador {jugadorActual.Nombre} perdio");
                    }
                }

                Console.WriteLine();
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