using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenvilleRevenueGUI
{
    class Hand
    {
        String nameOfPlayer;
        Card[] myCards = new Card[5];
        int numberOfCards = 0;
        int totalValue = 0;

        public Hand(String Name)
        {
            nameOfPlayer = Name;

        }

        public void dealCard(Card aCard)
        {
            if (numberOfCards < 5)
            {
                myCards[numberOfCards] = aCard;
                totalValue = totalValue + aCard.GetCardValue();
                numberOfCards++;
            }
        }

        public int getNumberofCards()
        {
            return numberOfCards;
        }

        public Card GetaCard(int index)
        {
            return myCards[index];
        }

        public int getTotalValue()
        {
            totalValue = 0;
            for (int i = 0; i < numberOfCards; i++)
            {
                totalValue = totalValue + myCards[i].GetCardValue();
            }
            return totalValue;
        }

        public void resetHand()
        {
            totalValue = 0;
            numberOfCards = 0;
            for(int i = 0; i < 5; i++)
            {
                myCards[i] = null;
            }
        }
    }
}
