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

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    using System.Windows.Threading;

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>() // create a list of eight pairs of emoji
            {
                "🐙", "🐙",
                "🐟", "🐟",
                "🐘", "🐘",
                "🐳", "🐳",
                "🐪", "🐪",
                "🦕", "🦕",
                "🦘", "🦘",
                "🦔", "🦔",
            };

            Random random = new Random(); // create a new random number generator

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) // find every textblock in the main grid and repeat the following statements for each of them
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    int index = random.Next(animalEmoji.Count); // pick a random number between 0 and the number of emoji left in the list and call it "index"
                    string nextEmoji = animalEmoji[index]; // use the random number called "index" to get a random emoji from the list
                    textBlock.Text = nextEmoji; // update the textblock with the random emoji from the list
                    animalEmoji.RemoveAt(index); // remove the random emoji from the list
                }
                
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false; // it keeps track of whether or not the player just clicked on the first animal in a pair and is now trying to find its match

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /*if it's the first in the pair being clicked, keep track of which TextBlock was clicked and make the animal disappear.
             * if it's the second one, either make it disappear (if it's a match) or bring back the first one (if it's not)*/

            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)// the player just clicked the first animal in a pair, so it makes that animal invisible and keeps track of its TextBlock in case it needs to make it visible again
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }else if (textBlock.Text == lastTextBlockClicked.Text)// the player found a match! So it makes the second animal in the pair invisible(and unclickable) too, and resets findingMatch so the next animal clicked on is the first one in a pair again
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else // the player clicked on an animal that doesn't match, so it makes the first animal that was clicked visible again and resets findingMatch
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)// this resets the game if all 8 matched pairs have been found(otherwise it does nothing because the game is still running)
            {
                SetUpGame();
            }
        }
    }
}
