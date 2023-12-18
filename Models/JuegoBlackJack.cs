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
            throw new NotImplementedException();
        }

        public void JugarRonda()
        {
            throw new NotImplementedException();
        }

        public void MostrarGanador()
        {
            throw new NotImplementedException();
        }
    }
}