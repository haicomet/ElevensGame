using System;
using System.Collections.Generic;
using System.Linq;

public class Deck
{
     private List<Card> cards = new List<Card>(); 
    private int currentCardIndex;
    private static Random Rng = new Random(); 

    // initializes a 52-card deck 
    public Deck()
    {
        InitializeDeck();
    }

    // populates the deck with 52 standard playing cards
    private void InitializeDeck()
    {
        cards = new List<Card>(52);
        currentCardIndex = 0;

        for (int i = 0; i < Card.Suits.Length; i++)
        {
            for (int j = 0; j < Card.Ranks.Length; j++)
            {
                string rank = Card.Ranks[j];
                string suit = Card.Suits[i];
                int value;

                if (j >= 10) 
                {
                    value = 10;
                }
                else
                {
                    value = j + 1; 
                }

                cards.Add(new Card(rank, suit, value));
            }
        }
    }

    // randomly rearranges the cards in the deck
    public void shuffle()
    {
        currentCardIndex = 0; // reset index

        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = Rng.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    // deals and returns the next card from the deck 
    public Card? dealCard() 
    {
        if (currentCardIndex < cards.Count)
        {
            return cards[currentCardIndex++]; 
        }
        return null; 
    }

    // returns true if no cards remain in the deck.
    public bool isEmpty()
    {
        return currentCardIndex >= cards.Count;
    }
}