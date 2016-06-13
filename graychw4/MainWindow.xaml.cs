// Name: Caleb Gray

// Description: A GUI Craps game build using WPF. The Users Enters a Starting amount for their bank
// account. They then choose an amount to bet and then rolls the dice the game can continue until 
// the back account reaches 0, then the option the re-start the game is given. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace graychw4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int pointTotal;
        private bool inPointMode = false;

        private int playerWinCount;
        private int houseWinCount;
        private int accountTotal;
        private int bet;

        private void rollDice()
        {
            //Gets two random numbers from 1-6 and assigns them to dice. 
            Random die = new Random();
            int dieOne = die.Next(1,7);
            Die1textBox.Text = dieOne.ToString();
            int dieTwo = die.Next(1,7);
            Die2textBox.Text = dieTwo.ToString();

            //Comuptes the total and updates the total textbox
            int total = dieOne + dieTwo;
            TotaltextBox.Text = total.ToString();
            bool isWinner = checkForWinner(total);

            //If there is now winner on the first roll, set the point field
            if(isWinner == false && pointTotal == 0)
            {
                pointTotal = total;
                PointTextBox.Text = pointTotal.ToString();
                inPointMode = true;
            }

            //If there is a winner set the play field for a new game
            if(isWinner == true)
            {
                RollDiceButton.IsEnabled = false;
                PlayAgainButton.IsEnabled = true;

            }
        }  
        private bool checkForWinner(int total)
        {
            bool isWinner = false;

            //The player wins if the total of the dice equals 7 or 11
            //checks ifs psayer wins and reports accordingly
            if((total == 7 || total == 11) && inPointMode == false)
            {
                playerWinCount++;
                PlayerWintextBox.Text = playerWinCount.ToString();
                WinnerLabel.Foreground = System.Windows.Media.Brushes.Green;
                WinnerLabel.Content = "Winner!";
                isWinner = true;
                accountTotal += (bet * 2);
                AccountBalanceTextBox.Text = accountTotal.ToString();
            }

            //The house wins if the total of the dice equals 2, 3 or 12
            //checks ifs psayer wins and reports accordingly
            if ((total == 2 || total ==3 || total == 12) && inPointMode == false)
            {
                houseWinCount++;
                HouseWintextBox.Text = houseWinCount.ToString();
                isWinner = true;
                WinnerLabel.Foreground = System.Windows.Media.Brushes.Red;
                WinnerLabel.Content = "Loser";
                if (accountTotal <= 0)
                {
                    GameOverBox.Visibility = Visibility.Visible;
                }
            }

            //After first roll if there is no win, game goes into point mode. 
            //the user wins if he/she rolls the point(the total of the first roll)
            if(inPointMode == true && total == pointTotal)
            {
                playerWinCount++;
                PlayerWintextBox.Text = playerWinCount.ToString();
                isWinner = true;
                inPointMode = false;
                pointTotal = 0;
                WinnerLabel.Foreground = System.Windows.Media.Brushes.Green;
                WinnerLabel.Content = "Winner!";
                accountTotal += (bet * 2);
                AccountBalanceTextBox.Text = accountTotal.ToString();
            }

            //In point mode the house wins if the dice total is 7
            if(inPointMode == true && total == 7)
            {
                houseWinCount++;
                HouseWintextBox.Text = houseWinCount.ToString();
                isWinner = true;
                inPointMode = false;
                pointTotal = 0;
                WinnerLabel.Foreground = System.Windows.Media.Brushes.Red;
                WinnerLabel.Content = "Loser";
                if(accountTotal <= 0)
                {
                    GameOverBox.Visibility = Visibility.Visible;
                }
            }
            return isWinner;
        }
        private void RollDiceButton_Click(object sender, RoutedEventArgs e)
        {
            rollDice();
        }
        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            Die1textBox.Text = "";
            Die2textBox.Text = "";
            TotaltextBox.Text = "";
            PointTextBox.Text = "";
            PlayAgainButton.IsEnabled = false;
            WinnerLabel.Content = "";
            Betbutton.IsEnabled = true;
        }
        private void StartMenu_Click(object sender, RoutedEventArgs e)
        {
            DialogBox.Visibility = Visibility.Visible;
            PlayerWintextBox.Text = "0";
            playerWinCount = 0;
            HouseWintextBox.Text = "0";
            houseWinCount = 0;
            BetTextBox.Text = "0";
            Betbutton.IsEnabled = true;
            StartMenu.IsEnabled = false;
            ResetMenu.IsEnabled = true;
            
        }
        private void ResetMenu_Click(object sender, RoutedEventArgs e)
        {
            playerWinCount = 0;
            houseWinCount = 0;
            PlayerWintextBox.Text = "0";
            HouseWintextBox.Text = "0";
            Die1textBox.Text = "";
            Die2textBox.Text = "";
            TotaltextBox.Text = "";
            PointTextBox.Text = "";
            WinnerLabel.Content = "";
            StartMenu.IsEnabled = false;
            PlayAgainButton.IsEnabled = false;
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Author: Caleb Gray\n" +
                            "Version: 1.0.1\n" +
                            ".Net version: 4.6.01038");
        }

        //Displays the rules of the game of Craps
        private void RulesMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("A player rolls two dice where each die has six faces in the usual way (values 1 through 6).\n" +
                            "After the dice have come to rest the sum of the two upward faces is calculated.\n" +
                            "The first roll\n" +
                            "If the sum is 7 or 11 on the first throw the roller / player wins.\n" +
                            "If the sum is 2, 3 or 12 the roller/ player loses, that is the house wins.\n" +
                            "If the sum is 4, 5, 6, 8, 9, or 10, that sum becomes the roller / player's \"point\".\n" +
                            "Continue given the player's point\n" +
                            "Now the player must roll the \"point\" total before rolling a 7 in order to win.\n" +
                            "If they roll a 7 before rolling the point value they got on the first roll the roller / player loses(the 'house' wins).");
        }
        
        private void confirmationbutton_Click(object sender, RoutedEventArgs e)
        {
            AccountBalanceTextBox.Text = UserAmountTextBox.Text;
            try
            {
                accountTotal = int.Parse(AccountBalanceTextBox.Text);
                if(accountTotal <= 0)
                {
                    throw new Exception("Account must be greater than 0");
                }
                DialogBox.Visibility = Visibility.Collapsed;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }    
        }

        private void startOverButton_Click(object sender, RoutedEventArgs e)
        {
            StartMenu_Click(sender, e);
            GameOverBox.Visibility = Visibility.Collapsed;
        }

        private void GameoverQuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Betbutton_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                bet = int.Parse(BetTextBox.Text);
                if ((accountTotal - bet) < 0)
                {
                    throw new Exception("Bet cannot exceed " + accountTotal);
                }
                if(bet < 0)
                {
                    throw new Exception("Bet must be a positive number!");
                }
                accountTotal -= bet;
                AccountBalanceTextBox.Text = accountTotal.ToString();
                Betbutton.IsEnabled = false;
                RollDiceButton.IsEnabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StartMenu_Click(sender, e);
        }
    }
}
