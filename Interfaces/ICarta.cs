using System;
using CardGame.Enumeradores;

namespace CardGame.Interfaces
{
	public interface ICarta
	{
		FigurasCartasEnum Figura { get; }
		ValoresCartasEnum Valor { get; }
	}
}

