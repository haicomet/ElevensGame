using System;
using System.Linq;

class Program
{
    private static Board board;

    private static string message = "";

    static void Main(string[] args)
    {
        board = new Board();

        
        while (true)
        {
            // 1. Fill any empty spots from the stock
            board.Replenish();

            // 2. Check for a WIN condition
            if (board.IsGameWon())
            {
                DisplayWinScreen();
                break; // Exit the game loop
            }

            // 3. Check for a LOSS condition
            if (!board.Hint().Any())
            {
                DisplayLossScreen();
                break; // Exit the game loop
            }

            // 4. If not won or lost, display the board and get input
            DisplayBoard();
            string input = Console.ReadLine();

            // 5. Process the user's input
            if (!ProcessInput(input))
            {
                // ProcessInput returns false if the user types 'quit'
                Console.WriteLine("Thanks for playing!");
                break; // Exit the game loop
            }
        }
    }

    private static void DisplayBoard()
    {
        Console.Clear();
        Console.WriteLine("--- 🃏 Elevens Solitaire 🃏 ---");
        Console.WriteLine();
        Console.WriteLine($"Stock: {board.StockCount} cards left");
        Console.WriteLine($"Moves: {board.Moves}");
        Console.WriteLine("---------------------------------");
        
        // Display the cards on the tableau
        for (int i = 0; i < board.Tableau.Count; i++)
        {
            // Show a "*" next to selected cards
            string selectedMarker = board.SelectedIndices.Contains(i + 1) ? "*" : " ";
            
            // Format: * [0] Ace of Spades
            Console.WriteLine($"{selectedMarker} [{i + 1}] {board.Tableau[i]}");
        }
        
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Type 1-9 to select/deselect a card.");
        Console.WriteLine("Type 'play' to remove selected cards.");
        Console.WriteLine("Type 'hint', 'restart', or 'quit'.");
        
        // Display any feedback messages
        if (!string.IsNullOrEmpty(message))
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
            message = ""; // Clear the message after displaying it
        }

        Console.Write("\n> ");
    }


    /// Handles the user's input and calls the appropriate Board methods.

    private static bool ProcessInput(string input)
    {
        input = input.ToLower().Trim();

        switch (input)
        {
            case "quit":
            case "exit":
                return false; // Signal to the main loop to exit

            case "restart":
                board.NewGame();
                message = "New game started!";
                return true;

            case "play":
            case "remove":
            case "submit":
                if (board.RemoveSelected())
                {
                    message = "Success! Cards removed.";
                }
                else
                {
                    // This fulfills Functional Need #9
                    message = "Invalid move! That is not a valid 11-sum or J-Q-K. Selection cleared.";
                }
                return true;
            
            case "hint":
                var hintIndices = board.Hint();
                if (hintIndices.Any())
                {
                    message = $"Hint: Try selecting cards at indices {string.Join(", ", hintIndices)}.";
                }
                else
                {
                    message = "No moves are possible.";
                }
                return true;

            default:
                // Check if the input is a number (a card index)
                if (int.TryParse(input, out int index))
                {
                    // User sees 1-9, but our list is 0-8.
                    // We must validate the 1-based input first.
                    if (index < 1 || index > board.Tableau.Count)
                    {
                        message = $"Invalid index. Please enter a number from 1 to {board.Tableau.Count}.";
                    }
                    else
                    {
                        // Convert the user's 1-based index to our 0-based index
                        board.Select(index - 1); 
                    }
                }
                else
                {
                    message = "Unknown command. Try again.";
                }
                return true;
        }
    }


    /// Displays the final win message.

    private static void DisplayWinScreen()
    {
        DisplayBoard(); // Show the final empty board
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n***********************************");
        Console.WriteLine("🎉 YOU WIN! Congratulations! 🎉");
        Console.WriteLine($"You cleared the deck in {board.Moves} moves.");
        Console.WriteLine("***********************************");
        Console.ResetColor();
    }

 
    /// Displays the final loss message.

    private static void DisplayLossScreen()
    {
        DisplayBoard(); // Show the final blocked board
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n***********************************");
        Console.WriteLine("😥 GAME OVER 😥");
        Console.WriteLine("No more moves are possible.");
        Console.WriteLine("***********************************");
        Console.ResetColor();
    }
}
