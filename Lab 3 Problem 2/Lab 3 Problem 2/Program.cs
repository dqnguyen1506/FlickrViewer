//Yung Nguyen
//Lab 3 Problem 2
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Lab_3_Problem_2
{
    class Stock
    {
        string name;
        int InitialValue; //stock price
        int MaxChange; //stock price change
        int notificationThreshold; //the value to measure the change of the stock for raising the event
        int currentValue;
        int numberChanges;
        public Stock(string name, int startingValue, int MaxChange, int threshold)
        {
            this.name = name;
            InitialValue = startingValue;
            currentValue = InitialValue;
            this.MaxChange = MaxChange;
            notificationThreshold = threshold;
            Thread thread = new Thread(new ThreadStart(Activate));
            thread.Start();//start a new thread
        }


        public void Activate()
        {
            for (int i = 0; i < 10 ;i++ )
            {
                Thread.Sleep(500); // 1/2 second
                ChangeStockValue();
            }
                
        }
        //update the stock value
        //if the new value is greater than the previous value
        void ChangeStockValue() 
        {
            Random r = new Random();
            currentValue += r.Next(1, MaxChange);
            numberChanges++;
            if ((currentValue - InitialValue) > notificationThreshold)
            {
                EventData data = new EventData();
                data.name = name;
                data.currentValue = currentValue;
                data.numberChanges = numberChanges;
                stockEvent?.Invoke(this, data);
            }
        }
        public event EventHandler<EventData> stockEvent;
    }
    class EventData: EventArgs
    {
        public string name { get; set; }
        public int currentValue { get; set; }
        public int numberChanges { get; set; }
    }
    class StockBroker //listener
    {
        string path = @"C:\Users\dungq\Documents\WriteLines.txt";
        public static ReaderWriterLockSlim newLockBoi = new ReaderWriterLockSlim();
        Stock stock;
        string brokerName;
        List<Stock> stocks= new List<Stock>();
  
        public StockBroker(string name) 
        {
            brokerName = name;
        }
        public void AddStock(Stock stock) //subscriber
        {
            stocks.Add(stock);
            stock.stockEvent += notify;
        }
        //Event Handler
        void notify(object sender, EventData e)
        {
            string s = this.brokerName.PadRight(10) + e.name.PadRight(15) + e.currentValue.ToString().PadRight(10) + e.numberChanges.ToString().PadRight(10) + DateTime.Now;
            Console.WriteLine(s);
            newLockBoi.EnterWriteLock();
            using(StreamWriter outputFile = new StreamWriter(path, true))
            {
                outputFile.WriteAsync(s + "\n");
            }
            newLockBoi.ExitWriteLock();

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(@"C:\Users\dungq\Documents\WriteLine.txt"))
            {
                File.Delete(@"C:\Users\dungq\Documents\WriteLines.txt");
            }
            Stock stock1 = new Stock("Technology", 160, 5, 15);
            Stock stock2 = new Stock("Retail", 30, 2, 6);
            Stock stock3 = new Stock("Banking", 90, 4, 10);
            Stock stock4 = new Stock("Commodity", 500, 20, 50);

            Console.WriteLine("Broker".PadRight(10) + "Stock".PadRight(15) + "Value".PadRight(10) + "Changes".PadRight(10) + "Timestamp");

            StockBroker b1 = new StockBroker("Broker 1");
            b1.AddStock(stock1);
            b1.AddStock(stock2);

            StockBroker b2 = new StockBroker("Broker 2");
            b2.AddStock(stock1);
            b2.AddStock(stock3);
            b2.AddStock(stock4);

            StockBroker b3 = new StockBroker("Broker 3");
            b3.AddStock(stock1);
            b3.AddStock(stock3);

            StockBroker b4 = new StockBroker("Broker 4");
            b4.AddStock(stock1);
            b4.AddStock(stock2);
            b4.AddStock(stock3);
            b4.AddStock(stock4);
        }
    }
}
