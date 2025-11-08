
The primary challenge I faced was effectively executing the rules of the physical card game. A bug I encountered at the end after thinking I considered everything was in the game's main loop. The game would get stuck in a state where there were no possible moves on the tableau, but because the stock deck was not yet empty, the IsGameLost() method would return false. This prevented the game from ending, and the player would be stuck, unable to make a move.

Solution: I overcame this by refactoring the Main method's while loop.

I removed the direct call to board.IsGameLost().

Instead, after checking for a win (board.IsGameWon()), I added a new condition: if (!board.Hint().Any()).

This uses the existing Hint() method (which finds possible moves) as the single source of truth for a loss condition. If Hint() returns an empty list, it means no moves are possible, and the game correctly triggers the DisplayLossScreen(), regardless of how many cards are left in the stock. This ensures the game always ends properly when the player can no longer make a move.

Updated CLI : <img width="309" height="374" alt="Screenshot 2025-11-08 at 10 39 49 AM" src="https://github.com/user-attachments/assets/a1812a02-592e-4b33-9340-4d4f9fd95694" />
Gameplay: <img width="309" height="374" alt="Screenshot 2025-11-08 at 10 34 18 AM" src="https://github.com/user-attachments/assets/cd2e635f-a494-4514-9655-5685e1e55dbb" />

# Elevens Solitaire (Console Game)

## Requirements

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (or any compatible .NET SDK)

# How to Run
To compile and run in one step, use the following command from the project's root directory:

dotnet run


The game will start, and you can play directly in your terminal.

# Gameplay

Type a number 1-9 to select or deselect a card.

Type play to submit your selected cards.

Type hint to see a possible move.

Type restart to start a new game.

Type quit to exit the game.
