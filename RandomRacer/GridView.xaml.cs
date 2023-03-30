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
            ChangeButtonStatus();
            ChangeTxtStatus();

            Thread t = new Thread(StartRace);
            t.Start();

            
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
            //bool even;

            while (count1 < MAX_WIDTH && count2 < MAX_WIDTH)
            {
                Dispatcher.Invoke(() =>
                {
                    first = Randomizer.GenerateNumber(INCREMENT_VALUE_MIN, INCREMENT_VALUE_MAX);
                    second = Randomizer.GenerateNumber(INCREMENT_VALUE_MIN, INCREMENT_VALUE_MAX);

                    count1 += first;
                    count2 += second;

                    // Tähän pitäisi laittaa jokin tarkistus voitosta 
                    CheckStatus(ref count1, ref count2, DebugFlag);
                    // Tarkisus loppuu tähän, siirretään omana metodiin

                    // Tähän tarkistus jos molemmat ovat 100 samaan aikaan
                    CheckOverMax(ref count1, ref count2, ref first, ref second);
                    /*
                     Jos on tasapeli MAX_WIDTH arvoon verrattaessa pitäisi heittää luvut uusiksi kunnes toinen on suurempi
                    sitten muuttaaa voittaja 100 ja jättää häviäjä edelliseen arvoon
                     */
                    // loppuu

                    // tarkastaa taas jos on yli 100
                    CheckStatus(ref count1, ref count2, DebugFlag);

                    LblFirst.Content = count1;
                    LblSecond.Content = count2;

                    RecFirst.Width = count1;
                    RecSecond.Width = count2;
                });
                Thread.Sleep(THREAD_SLEEP);
            }

            Dispatcher.Invoke(ChangeButtonStatus);
            Dispatcher.Invoke(ChangeTxtStatus);
        }

        private static void CheckOverMax(ref int count1, ref int count2, ref int first, ref int second)
        {
            // tarkistaa jos tulos on on tasa peli heittää nopat uudestaan että saadaan voittaja
            bool even = ValueChecker.CheckIfEvenOnMax(count1, count2, MAX_WIDTH);
            while (even)
            {
                // Ensin vähennetään arvot jotka johtivat tasapeliin
                count1 -= first;
                count2 -= second;

                first = Randomizer.GenerateNumber(INCREMENT_VALUE_MIN, INCREMENT_VALUE_MAX);
                second = Randomizer.GenerateNumber(INCREMENT_VALUE_MIN, INCREMENT_VALUE_MAX);
                count1 += first;
                count2 += second;

                even = ValueChecker.CheckIfEvenOnMax(count1, count2, MAX_WIDTH);
            }
        }

        private static void CheckStatus(ref int count1, ref int count2, bool debug)
        {
            // checks if count is over 100 will reset it to 100 if 
            // even does nothing
            int checkSum1, checkSum2;

            checkSum1 = ValueChecker.CheckIfOverFlow(count1, MAX_WIDTH);
            checkSum2 = ValueChecker.CheckIfOverFlow(count2, MAX_WIDTH);

            count1 -= checkSum1;
            count2 -= checkSum2;

            if(debug)
            {
                var caller = DebugInf.FormatFunctionCall("DebugInf.CheckStatus");
                // Debug lines
                var re1 = DebugInf.FormatVariables(count1, "count1");
                var re2 = DebugInf.FormatVariables(count2, "count2");
                Debug.WriteLine(caller);
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

            TxtFirst.Text = String.Empty;
            TxtSecond.Text = String.Empty;
            LblFirst.Content = String.Empty;
            LblSecond.Content = String.Empty;

            RecFirst.Width = 1;
            RecSecond.Width = 1;
            
        }

        private void Btn_ChangeColorBar1(object sender, RoutedEventArgs e)
        {
            // logic to change color
            var re = (MenuItem)e.Source;
            var debugInfo = DebugInf.FormatVariables(re.Header, "MenuItem header");

            Debug.WriteLine(debugInfo);
            ChangeColor(re.Header, RecFirst);

            e.Handled = true;
        }

        private void Btn_ChangeColorBar2(object sender, RoutedEventArgs e)
        {
            // logic to change color
            var re = (MenuItem)e.Source;
            var debugInfo = DebugInf.FormatVariables(re.Header, "MenuItem header");

            Debug.WriteLine(debugInfo);
            ChangeColor(re.Header, RecSecond);

            e.Handled = true;
        }

        private void ChangeColor(object colorHeader, Rectangle gritToChange)
        {
            string color = (string)colorHeader;

            if (color == "Black")
                gritToChange.Fill = Brushes.Black;
            else if (color == "Purple")
                gritToChange.Fill = Brushes.Purple;
            else if (color == "Blue")
                gritToChange.Fill = Brushes.Blue;
            else if (color == "Red")
                gritToChange.Fill = Brushes.Red;
            else if (color == "Yellow")
                gritToChange.Fill = Brushes.Yellow;
            else if (color == "Brown")
                gritToChange.Fill = Brushes.Brown;
            else if (color == "Pink")
                gritToChange.Fill = Brushes.Pink;
            else if (color == "Green")
                gritToChange.Fill = Brushes.Green;
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

    public class StatsRepository
    {

    }
}

