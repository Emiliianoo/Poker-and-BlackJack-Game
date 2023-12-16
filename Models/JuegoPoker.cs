using System;
using CardGame.Interfaces;
using CardGame.Enumeradores;
using System.IO.Compression;

namespace CardGame.Models
{
    public class JuegoPoker : IJuego
    {
        private IJugador Ganador { get; set; }
        private enum TipoDeManoEnum
        {
            CartaAlta = 1,
            Par = 2,
            DoblePar = 3,
            Trio = 4,
            Escalera = 5,
            Color = 6,
            FullHouse = 7,
            Poker = 8,
            EscaleraDeColor = 9,
            EscaleraReal = 10
        }

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
        
        /* Se utiliza una clase privada para determinar el resultado de la mano de cada jugador
        para que en el caso de que 2 jugadores tengan el mismo resultado de mano, se pueda determinar
        el ganador en base a sus cartas.
        */
        private class ResultadoMano
        {
            public TipoDeManoEnum TipoDeMano { get; set; }
            public IEnumerable<ICarta> Cartas { get; set; }

            public ResultadoMano(TipoDeManoEnum tipoDeMano, IEnumerable<ICarta> cartas)
            {
                TipoDeMano = tipoDeMano;
                Cartas = cartas;
            }
        }

        private ResultadoMano ObtenerCartaAlta(List<ICarta> cartas)
        {
            var cartasMayorAMenor = cartas.OrderByDescending(c => c.Valor);

            // Se retorna el resultado de la mano con la primera carta de la lista 
            // de cartas ordenadas siendo la carta más alta
            return new ResultadoMano(TipoDeManoEnum.CartaAlta, cartasMayorAMenor);
        }
    }
}