using System;
using CardGame.Interfaces;
using CardGame.Models;

namespace CardGame.Models
{
    public class JuegoBlackJack : IJuego
    {
        private List<IJugador> _jugadores;
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
            // TODO: Implementar

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
            throw new NotImplementedException();
        }
    }
}