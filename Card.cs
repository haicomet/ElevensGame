using System;
using System.Collections.Generic;



public class Card
{
    private string rank;
    private string suit;
    private int value;

    // properties for deck building
    public static string[] Suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
    public static string[] Ranks = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

    
    /// initializes a new card
    
    public Card(string rank, string suit, int value)
    {
        this.rank = rank;
        this.suit = suit;
        this.value = value;
    }

    
    /// returns the rank of the card
    
    public string getRank()
    {
        return rank;
    }

    
    /// returns the suit of the card
    
    public string getSuit()
    {
        return suit;
    }

    
    /// returns the numerical value of the card
    public int getValue()
    {
        return value;
    }

    /// returns a string representation of the card
    public override string ToString()
    {
        return $"{rank} of {suit}";
    }
}