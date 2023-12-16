using System;
using CardGame.Interfaces;
using CardGame.Enumeradores;
using System.IO.Compression;

namespace CardGame.Models
{
    public class JuegoPoker : IJuego
    {

        private IDealer _dealer;
        public IDealer Dealer 
        {
            get
            {
                return _dealer;
            }
            private set
            {
                _dealer = value;
            }
        }

        private List<IJugador> _jugadores;

        public bool JuegoTerminado { get; private set; } = false;

        public JuegoPoker()
        {
            _dealer = new Dealer();
            _jugadores = new List<IJugador>();
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
            // Cada jugador recibe inicialmente 5 cartas
            foreach(var jugador in _jugadores)
            {
                List<ICarta> cartas = _dealer.RepartirCartas(5);
                jugador.ObtenerCartas(cartas);
            }
        }

        public void JugarRonda()
        {
            // Cada jugador realiza su jugada
            foreach(var jugador in _jugadores)
            {
                jugador.RealizarJugada();
            }

            // Una vez que todos los jugadores han realizado su jugada, se muestran sus cartas
            foreach(var jugador in _jugadores)
            {
                jugador.MostrarCartas();
            }

            // Se determina el ganador
            MostrarGanador();
        }

        public void MostrarGanador()
        {
            throw new NotImplementedException();
        }
        
    }
}