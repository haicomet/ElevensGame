using System;
using System.Collections.Generic;
using System.Linq;

public class Deck
{
    private List<Card> cards;
    private Random rng = new Random();

    public Deck()
    {
        cards = new List<Card>();
        for (int s = 0; s <= 3; s++)
        {
            for (int r = 1; r <= 13; r++)
            {
                cards.Add(new Card(r, (Suit)s));
            }
        }
    }

    public int Count => cards.Count;

    public void Shuffle()
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    public List<Card> Deal(int n)
    {
        int numToDeal = Math.Min(n, cards.Count);
        if (numToDeal == 0)
        {
            return new List<Card>();
        }

        List<Card> dealtCards = cards.GetRange(0, numToDeal);
        cards.RemoveRange(0, numToDeal);

        return dealtCards;
    }
}