Card Game API
Overview :- The Card Game API allows players to play a card game against a computer opponent. Players can choose cards from their hand, and the computer selects the best possible card to win the round based on game rules. The game logic includes suit-based and rank-based rules to determine the winner for each round.

Features :-

- Play turns between a human and a computer.
- Intelligent computer opponent that tries to win each round.
- Validates and enforces game rules, including:
- Players must play a card of the same suit when possible.
- Higher-ranked cards win rounds.
- Tracks and updates player hands throughout the game.
Technology used
.NET 8: Framework used for building the API.
C#: Programming language.
Clean Architecture Principles: Designed with separation of concerns.
In-Memory Game State: Maintains game state during execution.

Prerequisites

.NET 8 SDK
A code editor (e.g., Visual Studio, Visual Studio Code)
Postman or a similar tool for testing APIs
Steps to run
git clone https://github.com/your-repository/card-game-api.git
cd card-game-api
dotnet restore
dotnet build
dotnet run
The API will be available at http://localhost:5000 or https://localhost:44371 by default.
License This project is licensed under the MIT License.

Game Rules
Turn Rules: The human player must choose a card from their hand. The computer will select the best card to win the round: If the computer has a card of the same suit and higher rank, it will play it. Otherwise, it plays any card of the same suit. If no cards of the same suit are available, the computer plays the lowest-ranked card.

Round Winner: A card of the same suit with a higher rank wins the round. If no cards of the same suit are played, the first player's card wins.
