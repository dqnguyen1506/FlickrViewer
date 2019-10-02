using System;

public class Program
{

    public static void Main()
    {
        Number myNumber = new Number(100000);
        myNumber.PrintMoney();
        myNumber.PrintNumber();
    }
}

class Number //consumer
{
    private PrintHelper _printHelper;

    public Number(int val)
    {
        _value = val;

        _printHelper = new PrintHelper();
        _printHelper.beforePrintEvent += printHelper_beforePrintEvent;
        _printHelper.beforePrintEventNoD += printHelper_beforePrintEventNoD;
    }
    //beforePrintevent handler
    static void printHelper_beforePrintEvent(object sender, beforePrintEventArgs e)
    {
        Console.WriteLine("BeforPrintEventNoDHandler: " + e.name + " is going to print a value");
    }
    static void printHelper_beforePrintEventNoD(object sender, EventArgs e)
    {
        Console.WriteLine("BeforPrintEventHandler: beforePrintEvent is going to print a value");
    }

    private int _value;

    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public void PrintMoney()
    {     
        _printHelper.PrintMoney(_value);
    }

    public void PrintNumber()
    {
        _printHelper.PrintNumber(_value);
    }
}

public class PrintHelper //Publisher
{

    //declare event of EventHandler
    public event EventHandler<beforePrintEventArgs> beforePrintEvent;

    public event EventHandler beforePrintEventNoD;

    public PrintHelper()
    {

    }

    public void PrintNumber(int num)
    {
        //call delegate method before going to print
        beforePrintEventArgs args = new beforePrintEventArgs();
        args.value = num;
        args.name = "PrintNumber";
        beforePrintEvent?.Invoke(this,args);
        beforePrintEventNoD?.Invoke(this, EventArgs.Empty);

        Console.WriteLine("Number: {0,-12:N0}", num);
    }

    public void PrintMoney(int num)
    {
        beforePrintEventArgs args = new beforePrintEventArgs();
        args.value = num;
        args.name = "PrintMoney";
        beforePrintEvent?.Invoke(this, args);
        beforePrintEventNoD?.Invoke(this, EventArgs.Empty);
        Console.WriteLine("Money: {0:C}", num);
    }
    /* OPTIONAL (alternative of ?.Invoke)
    protected virtual void letsPrint(beforePrintEventArgs e)
    {
        EventHandler <beforePrintEventArgs> handler = beforePrintEvent;
        if(handler != null)
        {
            handler(this, e);
        }
    }
    protected virtual void letsPrintNoD()
    {
        EventHandler handler = beforePrintEventNoD;
        if(handler != null)
        {
            handler(this, EventArgs.Empty);
        }
    }
    */
}
public class beforePrintEventArgs : EventArgs
{
    public int value { get; set; }
    public string name { get; set; }
}

