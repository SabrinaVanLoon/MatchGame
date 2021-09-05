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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetUpGame();

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
                int index = random.Next(animalEmoji.Count); // fick a random number between 0 and the number of emoji left in the list and call it "index"
                string nextEmoji = animalEmoji[index]; // use the random number called "index" to get a random emoji from the list
                textBlock.Text = nextEmoji; // update the textblock with the random emoji from the list
                animalEmoji.RemoveAt(index); // remove the random emoji from the list
            }
        }
    }
}
