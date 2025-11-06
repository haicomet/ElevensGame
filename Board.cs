using System;
using System.Collections.Generic;
using System.Linq;


public class Board
{

    private List<Card> tableau;
    private Deck stock;
    private List<int> selected;
    private int moves;

    
    public IReadOnlyList<Card> Tableau => tableau;
    
    
    public IReadOnlyList<int> SelectedIndices => selected;
    
    
    /// how many cards are left in the deck
    public int StockCount => stock.Count;
    
    
    /// how many moves the player has made
    public int Moves => moves;

  
    
    /// creates a new game board and starts a new game
    public Board()
    {
        // initialize lists to avoid null reference errors
        stock = new Deck();
        tableau = new List<Card>();
        selected = new List<int>();
        NewGame(); // start the first game
    }

    
    /// resets the board for a new game.
    public void NewGame()
    {
        stock = new Deck();
        stock.Shuffle();
        tableau = stock.Deal(9);
        selected.Clear();
        moves = 0;
    }

    
    /// selects or deselects the card at the given index
    public void Select(int i)
    {
        if (i < 0 || i >= tableau.Count)
        {
            // invalid index, do nothing
            return;
        }

        if (selected.Contains(i))
        {
            selected.Remove(i); // deselect
        }
        else
        {
            selected.Add(i); // select
        }
    }

    
    /// checks if the currently selected cards form a valid move 
    public bool CanRemoveSelected()
    {
        // check for 2-card 11-sum
        if (selected.Count == 2)
        {
            Card c1 = tableau[selected[0]];
            Card c2 = tableau[selected[1]];
            return c1.FaceValue + c2.FaceValue == 11;
        }

        // check for 3-card J-Q-K
        if (selected.Count == 3)
        {
            var ranks = selected.Select(index => tableau[index].Rank).ToList();
            return ranks.Contains(11) && ranks.Contains(12) && ranks.Contains(13);
        }

        // any other selection count is invalid
        return false;
    }


    /// if the selection is valid, removes the cards and returns true
    /// otherwise, clears the selection and returns false
    
    public bool RemoveSelected()
    {
        if (!CanRemoveSelected())
        {
            selected.Clear();
            return false;
        }

        // valid move
        moves++;

        // sort indices from high-to-low before removing
        // prevents "index out of range" errors as the list shrinks
        selected.Sort();
        selected.Reverse();

        foreach (int index in selected)
        {
            tableau.RemoveAt(index);
        }

        selected.Clear();
        return true;
    }

    
    /// fills empty tableau slots (up to 9) with cards from the stock
    public void Replenish()
    {
        int cardsToDeal = 9 - tableau.Count;
        if (cardsToDeal > 0)
        {
            tableau.AddRange(stock.Deal(cardsToDeal));
        }
    }

    
    /// finds one valid move (a pair or a JQK triple) and returns the indices
    /// returns an empty list if no move is possible
    /// (used to determine a loss state)
    public List<int> Hint()
    {
        // 1. check for JQK triple
        var jacks = new List<int>();
        var queens = new List<int>();
        var kings = new List<int>();

        for (int i = 0; i < tableau.Count; i++)
        {
            if (tableau[i].Rank == 11) jacks.Add(i);
            if (tableau[i].Rank == 12) queens.Add(i);
            if (tableau[i].Rank == 13) kings.Add(i);
        }

        if (jacks.Any() && queens.Any() && kings.Any())
        {
            // found a JQK combo
            return new List<int> { jacks.First(), queens.First(), kings.First() };
        }

        // 2. check for 11-sum pair
        for (int i = 0; i < tableau.Count; i++)
        {
            for (int j = i + 1; j < tableau.Count; j++)
            {
                if (tableau[i].FaceValue + tableau[j].FaceValue == 11)
                {
                    // found an 11-sum
                    return new List<int> { i, j };
                }
            }
        }

        // 3. no moves found
        return new List<int>();
    }

    
    /// checks if the game has been won.
    public bool IsGameWon()
    {
        // win condition: No cards left in stock and no cards on the table
        return stock.Count == 0 && tableau.Count == 0;
    }

    /// checks if the game has been lost.
    public bool IsGameLost()
    {
        // loss condition: no cards left in stock and no more moves are possible
        return stock.Count == 0 && Hint().Count == 0;
    }
}