using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thread_Safety_part3
{
    class ThreadRunner
    {
        private readonly int numberToGenerate = 100;
        private int index = 0;
        private int[] orderedNumbers;
        private Random rng = new Random();
        public void Run()
        {
            orderedNumbers = new int[numberToGenerate];
            Thread[] threads = new Thread[numberToGenerate];

            // Create threads
            for (int i = 0; i < numberToGenerate; i++)
            {
                ParameterizedThreadStart threadStart =
                    new ParameterizedThreadStart(AddValue);

                threads[i] = new Thread(threadStart);
            }

            // Start threads
            for (int i = 0; i < numberToGenerate; i++)
            {
                threads[i].Start(i + 1);
            }

            // Wait for all to finish
            bool allThreadsFinished = false;

            while (!allThreadsFinished)
            {
                allThreadsFinished = true;

                foreach (Thread t in threads)
                {
                    if (t.IsAlive)
                    {
                        allThreadsFinished = false;
                        break;
                    }
                }
            }

            // Print results
            foreach (int i in orderedNumbers)
            {
                Console.WriteLine(i);
            }
        }
        private object lockObject = new object();
        private void AddValue(object value)
        {
            lock (lockObject)
            {
                orderedNumbers[index] = (int)value;
                Thread.Sleep(rng.Next(6));
                index++;
            }
        }
    }
}
