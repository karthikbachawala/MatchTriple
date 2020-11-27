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

namespace MatchTriple
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            StartGame();
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

        private void StartGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐵","🐵","🐵",
                "🐶","🐶","🐶",
                "🦁","🦁","🦁",
                "🦊","🦊","🦊",
                "🐮","🐮","🐮",
                "🐷","🐷","🐷",
                "🐰","🐰","🐰",
                "🐼","🐼","🐼",
            };
            Random random = new Random();

            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock" && textBlock.Name != "matchTriple")
                {
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                    textBlock.Visibility = Visibility.Visible;
                }
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        TextBlock lastTextBlockClicked;
        TextBlock recentTextBlockClicked;
        bool match1 = false;
        bool match2 = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (match1 == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = recentTextBlockClicked = textBlock;
                match1 = true;
            }
            else if (textBlock.Text == recentTextBlockClicked.Text && match2 == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = recentTextBlockClicked;
                recentTextBlockClicked = textBlock;
                match2 = true;
            }
            else if (textBlock.Text == recentTextBlockClicked.Text && textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                match1 = false;
                match2 = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                recentTextBlockClicked.Visibility = Visibility.Visible;
                match1 = match2 = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                StartGame();
            }
        }
    }
    
}
