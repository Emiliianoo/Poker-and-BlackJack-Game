using System;
using CardGame.Interfaces;

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
            if(_jugadores.Count < 5)
            {
                if(_jugadores.Contains(jugador))
                {
                    throw new Exception("El jugador ya está en la lista");
                }

                _jugadores.Add(jugador);
            }
            else
            {
                throw new Exception("No se pueden agregar más de 5 jugadores");
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