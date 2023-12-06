using System;
namespace CardGame.Interfaces
{
	public interface IDeckDeCartas
	{
		void BarajearDeck();
		ICarta VerCarta(int indiceCarta);
		ICarta SacarCarta(int indiceCarta);
		void MeterCarta(ICarta carta);
		void MeterCarta(List<ICarta> cartas);
	}
}

