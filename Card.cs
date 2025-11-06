using System;

public enum Suit
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}

public class Card
{
    public int Rank { get; private set; }
    public Suit Suit { get; private set; }
    public int FaceValue { get; private set; }

    public Card(int r, Suit s)
    {
        Rank = r;
        Suit = s;

        if (Rank == 11 || Rank == 12 || Rank == 13)
        {
            FaceValue = 0;
        }
        else
        {
            FaceValue = Rank;
        }
    }

    public override string ToString()
    {
        string rankStr;
        switch (Rank)
        {
            case 1:
                rankStr = "Ace";
                break;
            case 11:
                rankStr = "Jack";
                break;
            case 12:
                rankStr = "Queen";
                break;
            case 13:
                rankStr = "King";
                break;
            default:
                rankStr = Rank.ToString();
                break;
        }
        return $"{rankStr} of {Suit}";
    }
}