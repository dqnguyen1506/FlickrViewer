using System;

namespace Lab_1
{

    public class IntegerSet
    {
        //attributes for integer a
        private bool[] a = new bool[101];
        /*constructor with default values
         * 
         */
        public IntegerSet()
        {
            int i;
            //Default is empty set
            //all values set to false
            for (i = 0; i< 101; i++)
            {
                a[i] = false;
            }
        
        }
        public IntegerSet(int[] intArray)
        {
            int i;
            //Default is empty set
            //all values set to false
            for (i = 0; i < 101; i++)
            {
                a[i] = false;
            }
            for (i = 0; i < intArray.Length; i++)
            {
                if(intArray[i] >= 0 && intArray[i] < 101)
                    this.InsertElement(intArray[i]);
                else
                {
                    Console.WriteLine("\nValue " + intArray[i] + " didn't get added" );
                }
            }
        }
        /*Method to insert element
         * @param num - the number to be inserted
         */
         public void InsertElement(int num)
        {
            //index of that element set to True
            a[num] = true;
        }
        /*method to delete the element
         * @param num - the number to be deleted
         */
         public void DeleteElement (int num)
        {
            a[num] = false;
        }
        /* Override method ToString
         * @return returns a string containing the set as
         * a list of numbers
         */
        public override string ToString()
        {
            int i;
            string s = "";
            //interate through the array and
            //find the index to string
            for(i = 0; i < 101; i++)
            {
                if (a[i] == true)
                {
                    s = s + i + " ";
                }
                
            }
            //if it is an empty set
            if (s.Equals(""))
            {
                s = "N/A";
            }
            //return string
            return s;

        }
        /*Method to check for equality of strings and return
         * true of false
         * @param s - the object to be checked
         * @return true if both sets are equal
         */
         public Boolean IsEqualTo (IntegerSet s)
        {
            int i;
            //iterate through sets
            for (i = 0; i < 101; i++)
            {
                //if two values do not match then the
                //sets are not equal thus return false
                if (this.a[i] != s.a[i])
                    return false;
            }
            //if for loop is completed it implies that
            //sets are equal, return true
            return true;
        }
        /* MEthod to get the union of two sets
         * @param s1 is the first set
         * @param s2 is the second set
         * @return res is the resultant set
         */
         public IntegerSet Union (IntegerSet s1)
        {
            int i;
            IntegerSet res = new IntegerSet();
            //iterate through sets
            for(i = 0; i < 101; i++)
            {
                //if either of the sets has ith element
                //it will be inserted into result set
                if (this.a[i] | s1.a[i])
                    res.InsertElement(i);
            }
            return res;
        } 
        /* Method to get the intersection of two sets
         * @param s1 - the first set
         * @param s2 - the second set
         * @return res is the resultant set
         */
         public IntegerSet Intersection(IntegerSet s1)
        {
            int i;
            IntegerSet res = new IntegerSet();
            //iterate through sets
            for (i = 0; i < 101; i++)
            {
                //if either of the sets has ith element, it
                //will be inserted into result set
                if (this.a[i] && s1.a[i])
                    res.InsertElement(i);
            }
            return res;
        }
    }
   

    //main program
    class Program
    {
        static IntegerSet InputSet()
        {
            IntegerSet set1 = new IntegerSet();
            int i;
            Console.WriteLine("Enter amount: ");
            int amount = Convert.ToInt32(Console.ReadLine());
            for (i = 0; i < amount; i++)
            {
                Console.WriteLine("Enter element: ");
                int num = Convert.ToInt32(Console.ReadLine());
                while (num < 0 || num > 100)
                {
                    Console.WriteLine("Error Try again: ");
                    num = Convert.ToInt32(Console.ReadLine());

                }
                set1.InsertElement(num);
            }
       
            return set1;
        }
        static void Main(string[] args)
        {
            // initialize two sets
            Console.WriteLine("Input Set A");
            IntegerSet set1 = InputSet();
            
            Console.WriteLine("\nInput Set B");
            IntegerSet set2 = InputSet();
            

            IntegerSet union = set1.Union(set2);
            IntegerSet intersection = set1.Intersection(set2);

            // prepare output
            Console.WriteLine("\nSet A contains elements:");
            Console.WriteLine(set1.ToString());
            Console.WriteLine("\nSet B contains elements:");
            Console.WriteLine(set2.ToString());
            Console.WriteLine(
            "\nUnion of Set A and Set B contains elements:");
            Console.WriteLine(union.ToString());
            Console.WriteLine(
            "\nIntersection of Set A and Set B contains elements:");
            Console.WriteLine(intersection.ToString());

            // test whether two sets are equal
            if (set1.IsEqualTo(set2))
                Console.WriteLine("\nSet A is equal to set B");
            else
                Console.WriteLine("\nSet A is not equal to set B");

            // test insert and delete
            Console.WriteLine("\nInserting 77 into set A...");
            set1.InsertElement(77);
            Console.WriteLine("\nSet A now contains elements:");
            Console.WriteLine(set1.ToString());

            Console.WriteLine("\nDeleting 77 from set A...");
            set1.DeleteElement(77);
            Console.WriteLine("\nSet A now contains elements:");
            Console.WriteLine(set1.ToString());

            // test constructor
            int[] intArray = { 25, 67, 2, 9, 99, 105, 45, -5, 100, 1 };
            IntegerSet set3 = new IntegerSet(intArray);

            Console.WriteLine("\nNew Set contains elements:");
            Console.WriteLine(set3.ToString());
        }
    }
}
