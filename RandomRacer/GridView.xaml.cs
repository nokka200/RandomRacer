using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
using ValueTester;
using DebugInformation;
using System.Diagnostics;
using System.Printing;

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

        public bool DebugFlag { get; set; } = true;

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
            int checkSum1, checkSum2;
            bool even;

            while (count1 < MAX_WIDTH && count2 < MAX_WIDTH)
            {
                Dispatcher.Invoke(() =>
                {
                    first = Randomizer.GenerateNumber(INCREMENT_VALUE_MIN, INCREMENT_VALUE_MAX);
                    second = Randomizer.GenerateNumber(INCREMENT_VALUE_MIN, INCREMENT_VALUE_MAX);

                    count1 += first;
                    count2 += second;

                    // Tähän pitäisi laittaa jokin tarkistus voitosta 
                    CheckStatus(ref count1, ref count2, out checkSum1, out checkSum2, DebugFlag);
                    //

                    // Tarkisus loppuu tähän, siirretään omana metodiin
                    LblFirst.Content = count1;
                    LblSecond.Content = count2;

                    RecFirst.Width = count1;
                    RecSecond.Width = count2;
                });
                Thread.Sleep(THREAD_SLEEP);
            }

            Dispatcher.Invoke(ChangeButtonStatus);
        }

        private static void CheckStatus(ref int count1, ref int count2, out int checkSum1, out int checkSum2, bool debug)
        {
            // checks if count is over 100 will reset it to 100 if 
            // even does nothing

            checkSum1 = ValueChecker.CheckIfOverFlow(count1, MAX_WIDTH);
            checkSum2 = ValueChecker.CheckIfOverFlow(count2, MAX_WIDTH);

            count1 -= checkSum1;
            count2 -= checkSum2;

            if(debug)
            {
                // Debug lines
                var re1 = DebugInf.FormatVariables(count1, "count1");
                var re2 = DebugInf.FormatVariables(count2, "count2");
                Debug.WriteLine(re1);
                Debug.WriteLine(re2);
            }

        }

        private void ClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            LblFirst.Content = string.Empty;
            LblSecond.Content = string.Empty;
            RecFirst.Width = 1;
            RecSecond.Width = 1;
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

