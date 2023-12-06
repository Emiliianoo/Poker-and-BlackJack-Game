using System;
namespace CardGame.Interfaces
{
	public interface IComparadorDeManos
	{
		List<ICarta> ObtenerManoGanadora(List<List<ICarta>> manosDeCartas);
	}
}

