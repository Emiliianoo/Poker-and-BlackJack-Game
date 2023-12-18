using System;
using CardGame.Models;
using CardGame.Enumeradores;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Preguntar que juego quiere jugar
            Console.WriteLine("Bienvenido, que juego desea jugar?");
            Console.WriteLine("1) Poker, 2) BlackJack");
            
            int opcion = Convert.ToInt32(Console.ReadLine());

            if(opcion == 1)
            {
                //Crear un juego de poker
                var juegoPoker = new JuegoPoker();

                //Preguntar cuantos jugadores van a jugar
                Console.WriteLine("Cuantos jugadores van a jugar?");
                int numeroJugadores = Convert.ToInt32(Console.ReadLine());

                //Crear los jugadores
                for(int i = 0; i < numeroJugadores; i++)
                {
                    string nombreJugador = CrearNombreJugador();
                    juegoPoker.AgregarJugador(new JugadorPoker(nombreJugador, juegoPoker.Dealer));                    
                }

                //Iniciar el juego
                juegoPoker.IniciarJuego();
                
            } 
            else if(opcion == 2)
            {
                //Crear un juego de BlackJack
                var juegoBlackJack = new JuegoBlackJack(1);

                //Preguntar cuantos jugadores van a jugar
                Console.WriteLine("Cuantos jugadores van a jugar?");
                int numeroJugadores = Convert.ToInt32(Console.ReadLine());

                //Crear los jugadores
                for(int i = 0; i < numeroJugadores; i++)
                {
                    string nombreJugador = CrearNombreJugador();
                    juegoBlackJack.AgregarJugador(new JugadorBlackJack(nombreJugador, juegoBlackJack.Dealer));                    
                }

                //Iniciar el juego
                juegoBlackJack.IniciarJuego();
            } 
            else 
            {
                throw new Exception("Opcion no valida");
            }
        }
        
        static string CrearNombreJugador()
        {
            Console.WriteLine("Dame el nombre del jugador");
            string nombreJugador = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombreJugador))
            {
                throw new Exception("El nombre del jugador no puede ser espacios en blanco");
            } else if (nombreJugador.Length > 20)
            {
                throw new Exception("El nombre del jugador no puede ser mayor a 20 caracteres");
            } 

            return nombreJugador;
        }
    }
}