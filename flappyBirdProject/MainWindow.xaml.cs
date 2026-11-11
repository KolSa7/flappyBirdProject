using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace flappyBirdProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //alapból folyamatosan lefele megy az objektum, ha space, akkor felfele megy 0,5 mpig x mpig
        //oszlopok konstans mozognak balra
        //oszlop osztaly

        class Obstacle
        {
            public int gapLocation;

            public Obstacle(int gapLocation)
            {
                this.gapLocation = gapLocation;
            }
        }
        DispatcherTimer timer;
        Random rnd = new Random();
        int jumCounter = 0;
        int difficulty = -12;
        double speed = 1;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(this);
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            double currentPos = Canvas.GetTop(player);
            Canvas.SetTop(player, currentPos + speed);
            if (speed <= 6)
            {
                speed += 1;
            }
        }
        private void GenerateObstackle(Obstacle o)
        {

        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                speed = difficulty;
            }
        }
    }
}