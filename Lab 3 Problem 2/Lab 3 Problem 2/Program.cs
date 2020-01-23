//Yung Nguyen
//Lab 3 Problem 2
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_3_Problem_2
{
    class Stock
    {
        public string name;
        public int InitialValue; //stock price
        public int MaxChange; //stock price change
        public int notificationThreshold; //the value to measure the change of the stock for raising the event
        public int currentValue;
        public int numberChanges;
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
        string path = @"C:\Users\dungq\Documents\CECS 475\Lab 3 Problem 2\lab 3.txt";
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
        public async void notify(object sender, EventData e)
        {

            await writeFile((Stock) sender);
        }
        public async Task writeFile(Stock e)
        {
            string s;
            String out0 = brokerName.ToString();
            String out1 = e.name.ToString();
            String out2 = e.currentValue.ToString();
            String out3 = e.numberChanges.ToString();

            s = out0.PadRight(10) + out1.PadRight(10) + out2.PadRight(10) + out3.PadRight(10) + DateTime.Now;

            newLockBoi.EnterWriteLock();
            Console.WriteLine(s);
            using (StreamWriter outputFile = new StreamWriter(path, true))
            {
                await outputFile.WriteAsync(s + "\n");
            }
            newLockBoi.ExitWriteLock();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists(@"C:\Users\dungq\Documents\CECS 475\Lab 3 Problem 2\lab 3.txt"))
            {
                File.Delete(@"C:\Users\dungq\Documents\CECS 475\Lab 3 Problem 2\lab 3.txt");
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
