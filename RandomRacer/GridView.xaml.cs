using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RandomRacer
{
    /// <summary>
    /// Interaction logic for GridView.xaml
    /// </summary>
    public partial class GridView : Window
    {
        const int INCREMENT_VALUE_MIN = 1;
        const int INCREMENT_VALUE_MAX = 5;
        const int MAX_WIDTH = 100;
        const int THREAD_SLEEP = 300;

        public GridView()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            // Starts the race by creating a new Thread with start method, changes buttons and Txtbox status
            Thread t = new Thread(StartRace);
            t.Start();

            ChangeButtonStatus();

            ChangeTxtStatus();
        }

        private void ChangeTxtStatus()
        {
            // status to on or off
            TxtFirst.IsEnabled = !TxtFirst.IsEnabled;
            TxtSecond.IsEnabled = !TxtSecond.IsEnabled;
        }

        private void ChangeButtonStatus()
        {
            // status to on or off
            BtnStart.IsEnabled = !BtnStart.IsEnabled;
            BtnReset.IsEnabled = !BtnReset.IsEnabled;
        }

        private void StartRace()
        {
            // starts the race, is used in a new thread
            // increments both rectangles widht with a random number
            int count1 = 0;
            int count2 = 0;
            int first, second;

            while (count1 <= MAX_WIDTH && count2 <= MAX_WIDTH)
            {
                Dispatcher.Invoke(() =>
                {
                    first = Randomizer.GenerateNumber(INCREMENT_VALUE_MIN, INCREMENT_VALUE_MAX);
                    second = Randomizer.GenerateNumber(INCREMENT_VALUE_MIN, INCREMENT_VALUE_MAX);

                    count1 += first;
                    count2 += second;

                    // Tähän pitäisi laittaa jokin tarkistus voitosta 
                    LblFirst.Content = count1;
                    LblSecond.Content = count2;

                    RecFirst.Width = count1;
                    RecSecond.Width = count2;
                });
                Thread.Sleep(THREAD_SLEEP);
            }

            Dispatcher.Invoke(ChangeButtonStatus);
        }


        private void ClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    static public class Randomizer
    {
        // Random logic to increase rectancle's size
        static readonly Random randObj = new();

        static public int GenerateNumber(int nunMin, int numMAx)
        {
            var re = randObj.Next(nunMin, numMAx);

            return re;
        }
    }
}

