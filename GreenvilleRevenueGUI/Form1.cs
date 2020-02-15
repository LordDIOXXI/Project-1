using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenvilleRevenueGUI
{
    public partial class Form1 : Form
    {
        Cards DeckofCards = new Cards();
        Hand PlayerHand;
        Hand DealerHand;
        Cards ACardBack;
        Boolean firsttimethru = true;
        int hit = 1;
        Boolean winLose;

        public Form1()
        {
            InitializeComponent();
            DeckofCards.ShuffleCards();
            PlayerHand = new Hand("Jacob West");
            DealerHand = new Hand("Dealer");
            button12.Enabled = false;
            button13.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            button12.Enabled = true;
            button13.Enabled = true;
            handReset();
            Deal();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Boolean lose;
            Card aCard = DeckofCards.getNextCard();
            switch (hit)
            {
                case 1:
                    aCard = DeckofCards.getNextCard();
                    button9.Image = aCard.GetCardImage();
                    PlayerHand.dealCard(aCard);
                    label11.Text = aCard.GetCardValue().ToString();
                    label3.Text = "Player Card Value: " + PlayerHand.getTotalValue().ToString();
                    hit++;
                    checkHit();
                    bust();
                    break;
                case 2:
                    aCard = DeckofCards.getNextCard();
                    button10.Image = aCard.GetCardImage();
                    PlayerHand.dealCard(aCard);
                    label12.Text = aCard.GetCardValue().ToString();
                    label3.Text = "Player Card Value: " + PlayerHand.getTotalValue().ToString();
                    hit++;
                    checkHit();
                    bust();
                    break;
                case 3:
                    aCard = DeckofCards.getNextCard();
                    button11.Image = aCard.GetCardImage();
                    PlayerHand.dealCard(aCard);
                    label13.Text = aCard.GetCardValue().ToString();
                    label3.Text = "Player Card Value: " + PlayerHand.getTotalValue().ToString();
                    hit++;
                    checkHit();
                    bust();
                    break;
                case 4:
                    break;
            }
        }

        public void handReset()
        {
            button12.Enabled = false;
            button13.Enabled = false;
            Cards ACardBack = new Cards();
            Card aCard = ACardBack.getTheBackOfTheCard();
            PlayerHand.resetHand();
            DealerHand.resetHand();
            hit = 1;
            label1.Text = "";
            label4.Text = "";
            label5.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            button2.Image = aCard.GetCardImage();
            button3.Image = aCard.GetCardImage();
            button4.Image = aCard.GetCardImage();
            button5.Image = aCard.GetCardImage();
            button6.Image = aCard.GetCardImage();
            button7.Image = aCard.GetCardImage();
            button8.Image = aCard.GetCardImage();
            button9.Image = aCard.GetCardImage();
            button10.Image = aCard.GetCardImage();
            button11.Image = aCard.GetCardImage();
            label3.Text = "Player Card Value: " + PlayerHand.getTotalValue().ToString();
            label2.Text = "Dealer Card Value: ???";
        }

        public Boolean isBust()
        {
            int playerValue = PlayerHand.getTotalValue();
            Boolean isBust = false;
            if (playerValue > 21)
            {
                isBust = true;
                return isBust;
            }
            else
            {
                isBust = false;
                return isBust;
            }
        }

        public void bust()
        {
            if (isBust())
            {
                Boolean lose = false;
                winOrLose(lose);
            }
        }

        //deal out initial cards for dealer and player for deal button.
        public void Deal()
        {

            button12.Enabled = true;
            button13.Enabled = true;
            //dealer hand
            Card aCard = DeckofCards.getNextCard();
            button2.Image = aCard.GetCardImage();
            DealerHand.dealCard(aCard);
            label1.Text = aCard.GetCardValue().ToString();

            aCard = DeckofCards.getNextCard();
            DealerHand.dealCard(aCard);
            label4.Text = "???";
            label2.Text = "Dealer Card Value: ???";
            

            //player hand
            aCard = DeckofCards.getNextCard();
            button7.Image = aCard.GetCardImage();
            PlayerHand.dealCard(aCard);
            label9.Text = aCard.GetCardValue().ToString();

            aCard = DeckofCards.getNextCard();
            button8.Image = aCard.GetCardImage();
            PlayerHand.dealCard(aCard);
            label10.Text = aCard.GetCardValue().ToString();
            label3.Text = "Player Card Value: " + PlayerHand.getTotalValue().ToString();
            bust();
        }

        public void winOrLose(Boolean winOrLose)
        {
            if (winOrLose)
            {
                DialogResult dialogResult = MessageBox.Show("You win! Would you like to play again?", "Win!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    handReset();
                    Deal();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }

            else
            {
                DialogResult dialogResult = MessageBox.Show("You lose! Would you like to play again?", "Lose!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    handReset();
                    Deal();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
        }

        public void checkHit()
        {
            int playerTotal = PlayerHand.getTotalValue();
            if (playerTotal == 21)
            {
                Boolean win = true;
                winOrLose(win);
            }
        }

        public Boolean evaluation()
        {
            int playerTotal = PlayerHand.getTotalValue();
            int dealerTotal = DealerHand.getTotalValue();
            Boolean dealing = true;
            Boolean winLose = false;

            if(playerTotal > 21)
            {
                dealing = false;
                return dealing;
                winOrLose(winLose);
            }

            if(dealerTotal > playerTotal)
            {
                dealing = false;
                return dealing;
                winOrLose(winLose);
            }
            return dealing;
        }

        /*
         *********Dealer Section*********
         */

        //Stay button, activates dealer
        private void button13_Click(object sender, EventArgs e)
        {
            button12.Enabled = false;
            button13.Enabled = false;
            showDealerCard();
            evaluation();
            Boolean dealing = evaluation();
            int hit = 0;

            while (dealing)
            {
                dealing = dealerHit(hit);
                hit++;
            }
            winOrLose(winLose);
        }

        public void showDealerCard()
        {
            Card aCard = DealerHand.GetaCard(1);
            label4.Text = aCard.GetCardValue().ToString();
            button3.Image = aCard.GetCardImage();
            label2.Text = "Dealer Card Value: " + DealerHand.getTotalValue().ToString();
        }

        //hits for the dealer
        public Boolean dealerHit(int hit)
        {
            Card aCard = DeckofCards.getNextCard();
            Boolean dealing = true;

            switch (hit)
            {
                case 0:
                    DealerHand.dealCard(aCard);
                    button4.Image = aCard.GetCardImage();
                    label5.Text = aCard.GetCardValue().ToString();
                    label2.Text = "Dealer Card Value: " + DealerHand.getTotalValue();
                    dealing = checkDealer();
                    return dealing;
                case 1:
                    aCard = DeckofCards.getNextCard();
                    DealerHand.dealCard(aCard);
                    button5.Image = aCard.GetCardImage();
                    label7.Text = aCard.GetCardValue().ToString();
                    label2.Text = "Dealer Card Value: " + DealerHand.getTotalValue();
                    dealing = checkDealer();
                    return dealing;
                case 2:
                    aCard = DeckofCards.getNextCard();
                    DealerHand.dealCard(aCard);
                    button6.Image = aCard.GetCardImage();
                    label8.Text = aCard.GetCardValue().ToString();
                    label2.Text = "Dealer Card Value: " + DealerHand.getTotalValue();
                    dealing = checkDealer();
                    return dealing;
            }
            return dealing;
        }

        //Checks dealer to see if he wins or busts
        public Boolean checkDealer()
        {
            int playerTotal = PlayerHand.getTotalValue();
            int dealerTotal = DealerHand.getTotalValue();
            int dealerHand = DealerHand.getNumberofCards();
            Boolean dealing = true;

            if (dealerHand == 5 && dealerTotal == playerTotal)
            {
                DialogResult dialogResult = MessageBox.Show("It's a tie! Would you like to play again?", "Tie!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    handReset();
                    Deal();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }

            if (dealerTotal > 21)
            {
                dealing = false;
                winLose = true;
            }

            if (dealerTotal == 21 && playerTotal < 21)
            {
                dealing = false;
                winLose = false;
            }

            if (dealerTotal > playerTotal && dealerTotal <= 21)
            {
                dealing = false;
                winLose = false;
            }
            return dealing;
        }

        //----------------------------------------------------
        //GIVE ME ACES BUTTONS - 2 ACES TO DEALER 2 TO PLAYER
        //----------------------------------------------------
        private void button14_Click(object sender, EventArgs e)
        {
            DeckofCards.loadCards();
            DeckofCards.ShuffleCards();
            //DeckofCards.PutAcesFirst();
            //BigPurpleBucsButton();
        }

    }
}

