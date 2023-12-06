using System;
namespace CardGame.Interfaces
{
	public interface IJuego
	{
		IDealer Dealer { get; }
		IComparadorDeManos ComparadorDeManos { get; }
		void AgregarJugador(IJugador jugador);
		void IniciarJuego();
		void MostrarGanador();
	}
}

