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
using System.Windows.Shapes;
using System.Threading;

namespace Threads
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<int> primeNumbers;
        public MainWindow()
        {
            InitializeComponent();
            primeNumbers = new List<int>();
        }

        private void FindPrimeNumbers(object param)
        {
            //multi thread
            int numberOfPrimesToFind = (int)param;

            int primeCount = 0; int currentPossiblePrime = 1;
            while (primeCount < numberOfPrimesToFind)
            {
                currentPossiblePrime++; int possibleFactor = 2; bool isPrime = true;
                while ((possibleFactor <= currentPossiblePrime / 2) && (isPrime == true))
                {
                    int possibleFactor2 = currentPossiblePrime / possibleFactor;
                    if (currentPossiblePrime == possibleFactor2 * possibleFactor)
                    {
                        isPrime = false;
                    }
                    possibleFactor++;
                }
                if (isPrime)
                {
                    primeCount++;
                    primeNumbers.Add(currentPossiblePrime);
                }
            }
        }

        //single thread
        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    FindPrimeNumbers(20000);
        //    outputTextBox.Text = primeNumbers[9999].ToString();

        //}

        //multi thread
        //private async void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    await Task.Run(() => FindPrimeNumbers(20000));
        //    outputTextBox.Text = primeNumbers[9999].ToString();
        //}

        //task 22 Asynchronous thread callbacks
        private void Button_Click_1(object sender, EventArgs e)
        {
            ParameterizedThreadStart ts = new ParameterizedThreadStart(FindPrimeNumbers);
            Task t = Task.Run(() => ts.Invoke(20000));
            t.ContinueWith(FindPrimesFinished);
        }
        private void FindPrimesFinished(IAsyncResult iar)
        {
            this.Dispatcher.Invoke(
            new Action<int>(UpdateTextBox),
            new object[] { primeNumbers[19999] });
        }


        private void UpdateTextBox(int number)
        {
            outputTextBox.Text = number.ToString();
        }
    } 

}
