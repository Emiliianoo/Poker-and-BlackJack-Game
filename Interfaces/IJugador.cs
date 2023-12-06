using System;
namespace CardGame.Interfaces
{
	public interface IJugador
	{
		void RealizarJugada();
		void ObtenerCartas(List<ICarta> cartas);
		ICarta DevolverCarta(int indiceCarta);
		List<ICarta> DevolverTodasLasCartas();
		List<ICarta> MostrarCartas();
		ICarta MostrarCarta(int indiceCarta);
	}
}

