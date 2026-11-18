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

        DispatcherTimer timer;
        Random rnd = new Random();
        int difficulty = -12;
        double speed = 1;
        int gap = 50;
        int tickCount = 0;
        List<StackPanel> obstacles;
        public MainWindow()
        {
            InitializeComponent();
            obstacles = new List<StackPanel>();
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

            MoveObstackles();
            if (tickCount%gap==0)
            {
                GenerateObstackle();
            }
            double currentPos = Canvas.GetTop(player);
            
            if (currentPos<0)
            {
                speed = 0;
                Canvas.SetTop(player, 0);

            }
            else if (currentPos>=475&&speed!=difficulty)
            {
                speed = 0;
                Canvas.SetTop(player, 475);
            }
            else
            {
                if (speed <= 6)
                {
                    speed += 1;
                }
                Canvas.SetTop(player, currentPos + speed);
            }
            
            tickCount++;
        }
        private void CheckCollisions() 
        { 
            double playerY=Canvas.GetTop(player);
            double playerX=Canvas.GetLeft(player);
            foreach (var obstacle in obstacles)
            {
                double gapX = Canvas.GetLeft(obstacle);
                double gapY=-1;
                foreach (Rectangle block in obstacle.Children)
                {
                    gapY = block.Tag == "gap" ? Canvas.GetTop(block) : -1;//gapY-100 a range alja
                }
                //if ()//ha x ertekben benne van a csoben
                {
                    


                }
            }
        }
        private void MoveObstackles() 
        {
            foreach (StackPanel obj in obstacles)
            { 
                    double objPos = Canvas.GetLeft(obj);
                    Canvas.SetLeft(obj, objPos + difficulty / 2);
                    if (objPos < -50)
                    {
                        game.Children.Remove(obj);
                    }
            }
        }
        private void GenerateObstackle()
        {
            int gapLocation = rnd.Next(1, 4);
            StackPanel obstacle = new StackPanel
            {
                Width = 50,
                Height = 500
            };

            Canvas.SetLeft(obstacle, 800);

            for (int i = 0; i < 5; i++)
            {
                obstacle.Children.Add(new Rectangle
                {
                    Height = 100,
                    Width = 50,
                    Fill = i == gapLocation ? Brushes.Transparent : new SolidColorBrush(Color.FromRgb(0, 255, 39)),
                    Tag = i == gapLocation ? "gap" : "obstacle "
                });
            }

            game.Children.Add(obstacle);
            obstacles.Add(obstacle);
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