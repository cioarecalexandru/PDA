using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace ProducatorConsumator
{
    class Program
    {
        
        private static MyInt index = new MyInt();
        private static MyInt consumeIndex = new MyInt();
        static void Main(string[] args)
        {
            int index = 0;
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    new Thread(new ThreadStart(ProducerJob)).Start();
                }
                else
                {
                    new Thread(new ThreadStart(ConsumerJob)).Start();
                }
            }
            Console.Read();
        }

        private static void ProducerJob()
        {
            Random rng = new Random(0);
            ProducerConsumer producatorCons = new ProducerConsumer();
            while (index.Index < 100)
            {
                int number = 0;
                lock (index)
                {
                    number = index.Index;
                    index.Index++;
                }
                if (number < 100)
                {
                    Console.WriteLine("Producing {0}", number);
                    producatorCons.Produce(number);
                    Thread.Sleep(rng.Next(600));
                }
                
            }
        }
        

        static void ConsumerJob()
        {
            Random rng = new Random(1);
            ProducerConsumer producatorCons = new ProducerConsumer();
            while (consumeIndex.Index < 100)
            {
                //lock (consumeIndex)
                //{
                    if (consumeIndex.Index < 100) { 
                        object o = producatorCons.Consume();
                        Console.WriteLine("\t\t\t\tConsuming {0}", o);
                        Thread.Sleep(rng.Next(1000));
                        lock (consumeIndex)
                        {
                            consumeIndex.Index++;
                        }
                    }
                //}
            }
        }
    }
}
