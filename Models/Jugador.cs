using System;
using CardGame.Interfaces;

namespace CardGame.Models
{
    public class Jugador : IJugador
    {

        //Atributos del jugador
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
                    throw new NotImplementedException();
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
                    throw new NotImplementedException();
                }
                _cartas = value;
            }
        }

        

        //Constructor del jugador
        public Jugador(string nombre, List<ICarta> cartas )
        {
            Nombre = nombre;
            Cartas = cartas;
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
                throw new NotImplementedException();
            }
        }

        public List<ICarta> DevolverTodasLasCartas()
        {
            //Devuelve todas las cartas del jugador si es que este tiene cartas
            if (Cartas.Count == 0)
            {
                throw new NotImplementedException();
            }
            else
            {
                //obtener todas las cartas del jugador
                List<ICarta> cartasADevolver = Cartas;

                //Quitar todas las cartas de la lista de cartas del jugador
                Cartas.Clear();

                //Devolver todas las cartas
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
                throw new NotImplementedException();
            }
        }

        public List<ICarta> MostrarCartas()
        {
             //Muestra todas las cartas del jugador si es que este tiene cartas
            if (Cartas.Count == 0)
            {
                throw new NotImplementedException();
            }
            else
            {
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
                throw new NotImplementedException();
            }

        }

        public void RealizarJugada()
        {
            throw new NotImplementedException();
        }
    }
}

