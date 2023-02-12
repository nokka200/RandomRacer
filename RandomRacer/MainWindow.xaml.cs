using System;
using System.CodeDom;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RandomRacer
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

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(StartRace);
            t.Start();
        }
        private void StartRace()
        {
            int count1 = 0;
            int count2 = 0;
            int first, second;

            while (count1 <= 100 && count2 <= 100)
            {
                Dispatcher.Invoke(() =>
                {
                    first = Randomizer.GenerateNumber(5);
                    second = Randomizer.GenerateNumber(5);

                    count1 += first;
                    count2 += second;

                    LblFirst.Content = count1;
                    LblSecond.Content = count2;

                    RecFirst.Width = count1;
                    RecSecond.Width = count2;
                });
                
                Thread.Sleep(300);
            }
            
        }
    }

    static public class Randomizer
    {
        static readonly Random randObj = new();

        static public int GenerateNumber(int numMAx)
        {
            var re = randObj.Next(1, numMAx);
        
            return re;
        }
    }
}
