using System;
using CardGame.Interfaces;
using CardGame.Enumeradores;
using System.IO.Compression;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace CardGame.Models
{
    public class JuegoPoker : IJuego
    {
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
            // Se barajea el deck
            _dealer.BarajearDeck();

            // Cada jugador recibe inicialmente 5 cartas
            foreach(var jugador in _jugadores)
            {
                List<ICarta> cartas = _dealer.RepartirCartas(5);
                jugador.ObtenerCartas(cartas);
            }

            JugarRonda();
        }

        public void JugarRonda()
        {
            // Cada jugador realiza su jugada
            foreach(var jugador in _jugadores)
            {
                jugador.RealizarJugada();
                Console.WriteLine();
            }

            // Una vez que todos los jugadores han realizado su jugada, se muestran sus cartas
            foreach(var jugador in _jugadores)
            {
                jugador.MostrarCartas();
                Console.WriteLine();
            }

            // Se determina el ganador
            MostrarGanador();
        }

        public void MostrarGanador()
        {
            List<ResultadoMano> resultados = new List<ResultadoMano>();
            foreach(var jugador in _jugadores)
            {
                var cartas = jugador.DevolverTodasLasCartas();
                ResultadoMano resultado = ObtenerResultadoMano(cartas);

                resultado.Jugador = jugador; 

                resultados.Add(resultado);
            }

            // Se ordenan los resultados de mayor a menor
            resultados = resultados.OrderByDescending(r => r.TipoDeMano).ToList();

            List<ResultadoMano> resultadosMayores = resultados.Where(r => r.TipoDeMano == resultados[0].TipoDeMano).ToList();

            // Mostrar los resultados de cada jugador
            Console.WriteLine("\n\nResultados de la ronda:");
            foreach(var resultado in resultados)
            {
                var jugador = resultado.Jugador as JugadorPoker;
                Console.WriteLine($"{jugador.Nombre} tiene {resultado.TipoDeMano}");
            }
            Console.WriteLine();

            // Si hay más de un jugador con el mismo resultado de mano, se determina el ganador en base a sus cartas
            if(resultadosMayores.Count > 1)
            {
                ObtenerGanadorPorCartas(resultadosMayores);
            }
            else
            {
                Console.WriteLine("El ganador es:");
                var ganador = resultados[0].Jugador as JugadorPoker;
                Console.WriteLine($"{ganador.Nombre} tiene {resultados[0].TipoDeMano}");
            }
        }

        private void ObtenerGanadorPorCartas(List<ResultadoMano> resultados)
        {
            List<ResultadoMano> ganadores = new List<ResultadoMano>{ resultados[0] };

            // Se comparan las cartas de cada jugador
            for(int i = 1; i < resultados.Count; i++)
            {
                ResultadoMano resultado = resultados[i];
                List<ICarta> cartasGanador = ganadores[0].Cartas.ToList();
                List<ICarta> cartasJugadorActual = resultado.Cartas.ToList();

                // Se comparan las cartas de cada jugador
                for(int j = 0; j < cartasGanador.Count; j++)
                {
                    if(j == cartasGanador.Count - 1)
                    {
                        // Si se llega a la última carta y son iguales, se determina que hay más de un ganador
                        if(cartasGanador[j].Valor == cartasJugadorActual[j].Valor)
                        {
                            ganadores.Add(resultado);
                        }
                    }
                    
                    if(cartasGanador[j].Valor > cartasJugadorActual[j].Valor)
                    {
                        break;
                    }
                    else if(cartasGanador[j].Valor < cartasJugadorActual[j].Valor)
                    {
                        ganadores.RemoveAll(g => true);
                        ganadores.Add(resultado);
                        break;
                    }
                }
            }

            // Imprimir los ganadores
            if(ganadores.Count > 1)
            {
                Console.WriteLine("Empate entre los siguientes jugadores:");
            }
            else
            {
                Console.WriteLine("El ganador es:");
            }

            foreach(var ganador in ganadores)
            {
                var jugador = ganador.Jugador as JugadorPoker;
                Console.WriteLine($"{jugador.Nombre} con {ganador.TipoDeMano} mayor");
            }
        }
        
        /* Se utiliza una clase privada para determinar el resultado de la mano de cada jugador
        para que en el caso de que 2 jugadores tengan el mismo resultado de mano, se pueda determinar
        el ganador en base a sus cartas.
        */
        private class ResultadoMano
        {
            public IJugador Jugador { get; set; } = null;
            public TipoDeManoEnum TipoDeMano { get; set; }
            public IEnumerable<ICarta> Cartas { get; set; }

            public ResultadoMano(TipoDeManoEnum tipoDeMano, IEnumerable<ICarta> cartas)
            {
                TipoDeMano = tipoDeMano;
                Cartas = cartas;
            }
        }

        private ResultadoMano ObtenerResultadoMano(List<ICarta> cartas)
        {

            // Se obtiene el resultado de la mano del jugador
            var resultadoMano = ObtenerEscaleraReal(cartas) ??
                                ObtenerEscaleraColor(cartas) ??
                                ObtenerPoker(cartas) ??
                                ObtenerFullHouse(cartas) ??
                                ObtenerColor(cartas) ??
                                ObtenerEscalera(cartas) ??
                                ObtenerTrio(cartas) ??
                                ObtenerDoblePareja(cartas) ??
                                ObtenerPareja(cartas) ??
                                ObtenerCartaAlta(cartas);

            return resultadoMano;
        }

        private ResultadoMano? ObtenerEscaleraReal(List<ICarta> cartas)
        {
            var escaleraDeColor = ObtenerEscaleraColor(cartas);

            if(escaleraDeColor != null)
            {
                // Si las cartas son de tipo escalera real, se retorna el resultado de la mano con el tipo de mano Escalera Real
                if((int) escaleraDeColor.Cartas.First().Valor == 1)
                {
                    return new ResultadoMano(TipoDeManoEnum.EscaleraReal, escaleraDeColor.Cartas);
                }
            }

            return null;
        }

        private ResultadoMano? ObtenerEscaleraColor(List<ICarta> cartas)
        {
            var escalera = ObtenerEscalera(cartas);

            if(escalera != null)
            {
                var color = ObtenerColor(cartas);

                if(color != null)
                {
                    // Si las cartas son de tipo escalera de color, se retorna el resultado de la mano con el tipo de mano Escalera de Color
                    return new ResultadoMano(TipoDeManoEnum.EscaleraDeColor, escalera.Cartas);
                }
            }

            return null;
        }

        //poker = 4 cartas del mismo valor
        private ResultadoMano? ObtenerPoker(List<ICarta> cartas)
        {
            var poker = cartas.GroupBy(c => c.Valor)
                                .Where(g => g.Count() == 4)
                                .SelectMany(g => g)
                                .ToList();

            var cartasSinPoker = cartas.Except(poker).OrderByDescending(c => c.Valor).ToList();
            cartasSinPoker = MoverAsAlPrincipio(cartasSinPoker);

            // Se añaden al final de la lista de cartas las cartas que no son poker
            var cartasOrdenadas = poker.Concat(cartasSinPoker).ToList();

            // Si las cartas son de tipo poker, se retorna el resultado de la mano con el tipo de mano Poker
            return poker.Count == 4 ? new ResultadoMano(TipoDeManoEnum.Poker, cartasOrdenadas) : null;
        }

        private ResultadoMano? ObtenerFullHouse(List<ICarta> cartas)
        {
            var trio = ObtenerTrio(cartas);

            if(trio != null)
            {
                //cartas sin trio (trio devuelve las 5 cartas asi que se quitan las primeras 3)
                var cartasSinTrio = trio.Cartas.Skip(3).ToList();

                // Si las cartas restantes tienen son pareja, se retorna el resultado de la mano con el tipo de mano Full House
                if(cartasSinTrio[0].Valor == cartasSinTrio[1].Valor)
                {
                    return new ResultadoMano(TipoDeManoEnum.FullHouse, trio.Cartas);
                }
            }

            return null;
        }

        private ResultadoMano? ObtenerColor(List<ICarta> cartas)
        {
            // Se seleccionan las cartas que tienen el mismo palo
            var cartasDeMismaFigura = cartas.GroupBy(c => c.Figura)
                                            .Where(g => g.Count() >= 5)
                                            .SelectMany(g => g)
                                            .OrderByDescending(c => c.Valor)
                                            .ToList();

            // Los Ases pueden ser tanto la carta más alta como la más baja, por lo que se debe
            // mover cada As al principio de la lista
            cartasDeMismaFigura = MoverAsAlPrincipio(cartasDeMismaFigura);

            // Si las cartas son de tipo color, se retorna el resultado de la mano con el tipo de mano Color
            return cartasDeMismaFigura.Count >= 5 ? new ResultadoMano(TipoDeManoEnum.Color, cartasDeMismaFigura) : null;
        }

        private ResultadoMano? ObtenerEscalera(List<ICarta> cartas)
        {
            // Se ordenan las cartas de mayor a menor
            var cartasOrdenadas = cartas.OrderByDescending(c => c.Valor).ToList();

            // Se checa si las cartas son consecutivas
            foreach(var carta in cartasOrdenadas)
            {
                // Si la carta no es la primera y no es consecutiva a la carta anterior, no es una escalera
                if(carta.Valor != cartasOrdenadas[0].Valor - cartasOrdenadas.IndexOf(carta))
                {
                    // si es la ultima carta y es un as, pero la escalera es de K, Q, J, 10
                    // se debe mover el as al principio de la lista
                    bool esUnAs = carta.Valor == ValoresCartasEnum.As;
                    bool esUltimaCarta = cartasOrdenadas.IndexOf(carta) == cartasOrdenadas.Count - 1;
                    if(esUnAs && cartasOrdenadas[0].Valor == ValoresCartasEnum.Rey && esUltimaCarta)
                    {
                        // Se mueve la carta al principio de la lista
                        cartasOrdenadas.Insert(0, cartasOrdenadas.Last());
                        cartasOrdenadas.RemoveAt(cartasOrdenadas.Count - 1);

                        return new ResultadoMano(TipoDeManoEnum.Escalera, cartasOrdenadas);
                    }

                    return null;
                }
            }

            // Si las cartas son de tipo escalera, se retorna el resultado de la mano con el tipo de mano Escalera
            return new ResultadoMano(TipoDeManoEnum.Escalera, cartasOrdenadas);
        }

        private ResultadoMano? ObtenerTrio(List<ICarta> cartas)
        {
            // Se selecciona el trio y se pone delante de la lista de cartas
            var trio = cartas.GroupBy(c => c.Valor) // Se agrupan las cartas por su valor
                             .Where(g => g.Count() == 3) // Se seleccionan las cartas que se repiten 3 veces
                             .SelectMany(g => g) // Se seleccionan las cartas que se repiten 3 veces
                             .ToList(); // Se convierte el resultado en una lista

            var cartasSinTrio = cartas.Except(trio).OrderByDescending(c => c.Valor).ToList();
            
            cartasSinTrio = MoverAsAlPrincipio(cartasSinTrio);

            // Se crea una nueva lista donde se coloca el trio al frente y el resto de las cartas sigue
            var cartasOrdenadas = trio.Concat(cartasSinTrio);

            // Si las cartas son de tipo trio, se retorna el resultado de la mano con el tipo de mano Trio
            return trio.Count == 3 ? new ResultadoMano(TipoDeManoEnum.Trio, cartasOrdenadas) : null;
        }

        private ResultadoMano? ObtenerDoblePareja(List<ICarta> cartas)
        {
            // Se seleccionan las cartas que se repiten 2 veces
            var parejas = cartas.GroupBy(c => c.Valor)
                                .Where(g => g.Count() == 2)
                                .SelectMany(g => g)
                                .ToList();

            parejas.OrderByDescending(c => c.Valor);
            parejas = MoverAsAlPrincipio(parejas);

            // se le añade al final de la lista de cartas las cartas que no son parejas
            var cartasOrdenadas = parejas.Concat(
                cartas.Except(parejas).OrderByDescending(c => c.Valor)
                ).ToList();

            // Si las cartas son de tipo doble pareja, se retorna el resultado de la mano con el tipo de mano Doble Par
            return parejas.Count == 4 ? new ResultadoMano(TipoDeManoEnum.DoblePar, cartasOrdenadas) : null;
        }

        private ResultadoMano? ObtenerPareja(List<ICarta> cartas)
        {
            // Se selecciona la pareja y se pone delante de la lista de cartas
            var pareja = cartas.GroupBy(c => c.Valor) // Se agrupan las cartas por su valor
                               .Where(g => g.Count() == 2) // Se seleccionan las cartas que se repiten 2 veces
                               .SelectMany(g => g) // Se seleccionan las cartas que se repiten 2 veces
                               .ToList(); // Se convierte el resultado en una lista

            var cartasSinPareja = cartas.Except(pareja).OrderByDescending(c => c.Valor).ToList();
            cartasSinPareja = MoverAsAlPrincipio(cartasSinPareja);

            var cartasOrdenadas = pareja.Concat(cartasSinPareja).ToList();

            // Si las cartas son de tipo pareja, se retorna el resultado de la mano con el tipo de mano Par
            return pareja.Count == 2 ? new ResultadoMano(TipoDeManoEnum.Par, cartasOrdenadas) : null;
        }

        private ResultadoMano ObtenerCartaAlta(List<ICarta> cartas)
        {
            var cartasOrdenadas = cartas.OrderByDescending(c => c.Valor).ToList();

            // Mover cada As al principio de la lista
            cartasOrdenadas = MoverAsAlPrincipio(cartasOrdenadas);

            // Se retorna el resultado de la mano con la primera carta de la lista 
            // de cartas ordenadas siendo la carta más alta
            return new ResultadoMano(TipoDeManoEnum.CartaAlta, cartasOrdenadas);
        }

        private List<ICarta> MoverAsAlPrincipio(List<ICarta> cartas)
        {
            var cartasOrdenadas = cartas.OrderByDescending(c => c.Valor).ToList();

            // Mover cada As al principio de la lista
            var listConAses = new List<ICarta>();
            var listSinAses = new List<ICarta>();
            for(int i = 0; i <= cartasOrdenadas.Count - 1; i++)
            {
                if(cartasOrdenadas[i].Valor == ValoresCartasEnum.As)
                {
                    listConAses.Add(cartasOrdenadas[i]);
                }
                else
                {
                    // Se añaden al final de la lista las cartas que no son Ases
                    listSinAses.Add(cartasOrdenadas[i]);
                }
            }

            var listaFinal = listConAses.Concat(listSinAses).ToList();

            return listaFinal;
        }
    }
}