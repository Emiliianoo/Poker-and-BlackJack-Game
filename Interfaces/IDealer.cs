using System;
namespace CardGame.Interfaces
{
	public interface IDealer
	{
		List<ICarta> RepartirCartas(int numeroDeCartas);
		void RecogerCartas(List<ICarta> cartas);
		void BarajearDeck();
	}
}

