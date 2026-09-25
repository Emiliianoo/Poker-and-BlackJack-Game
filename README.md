# Índice / Index

- [Descripción en español](#juego-de-cartas-poker-y-blackjack)
- [English description](#card-game-poker-and-blackjack)

---

# Juego de cartas: Poker y Blackjack

Este proyecto es una aplicación de consola desarrollada en C# con .NET 7 para simular partidas de dos juegos de cartas:

- Poker
- Blackjack

La aplicación permite elegir el juego desde la consola, ingresar la cantidad de jugadores y sus nombres, y luego realizar la lógica del juego con barajado, reparto, turnos y cálculo del ganador.

## Características

- Menú principal para elegir entre Poker y Blackjack
- Soporte para 2 a 6 jugadores
- Validación de nombres de jugadores
- Sistema de baraja, cartas, dealers y jugadores
- Lógica de comparación de manos en Poker
- Lógica de puntaje y ganador en Blackjack
- Estructura orientada a clases e interfaces para facilitar extensiones

## Requisitos

- .NET SDK 7.0 o superior
- Terminal o consola para ejecutar la aplicación

## Estructura del proyecto

- `Program.cs`: punto de entrada de la aplicación
- `Models/`: clases principales del juego, jugadores, dealers y reglas
- `Interfaces/`: contratos para cartas, deck, dealer, jugador y juego
- `Enumeradores/`: definiciones de valores y figuras de las cartas

## Cómo ejecutar el proyecto

Desde la raíz del proyecto, ejecuta:

```bash
dotnet run
```

También puedes restaurar dependencias primero si es necesario:

```bash
dotnet restore
dotnet run
```

## Cómo jugar

1. Ejecuta la aplicación.
2. Elige una opción:
   - `1` para jugar Poker
   - `2` para jugar Blackjack
3. Ingresa la cantidad de jugadores (entre 2 y 6).
4. Escribe el nombre de cada jugador.
5. La partida comenzará automáticamente y mostrará los resultados por consola.

## Notas

Este proyecto está pensado como una simulación educativa y de práctica de programación orientada a objetos en C#. La lógica del juego está separada en modelos, interfaces y enumeradores para mantener el código más organizado.

---

# Card Game: Poker and Blackjack

This project is a console application developed in C# with .NET 7 to simulate matches for two card games:

- Poker
- Blackjack

The application allows the user to choose the game from the console, enter the number of players and their names, and then run the game logic including shuffling, dealing, turns, and winner calculation.

## Features

- Main menu to choose between Poker and Blackjack
- Support for 2 to 6 players
- Validation of player names
- Card deck, cards, dealers, and player system
- Poker hand comparison logic
- Blackjack score and winner logic
- Object-oriented structure with classes and interfaces for easier extension

## Requirements

- .NET SDK 7.0 or higher
- Terminal or console to run the application

## Project structure

- `Program.cs`: application entry point
- `Models/`: main game, player, dealer, and rule classes
- `Interfaces/`: contracts for cards, deck, dealer, player, and game
- `Enumeradores/`: card value and suit definitions

## How to run the project

From the project root, run:

```bash
dotnet run
```

You can also restore dependencies first if needed:

```bash
dotnet restore
dotnet run
```

## How to play

1. Run the application.
2. Choose an option:
   - `1` to play Poker
   - `2` to play Blackjack
3. Enter the number of players (between 2 and 6).
4. Write the name of each player.
5. The match will start automatically and show the results in the console.

## Notes

This project is intended as an educational simulation and practice in object-oriented programming with C#. The game logic is separated into models, interfaces, and enumerators to keep the code more organized.
