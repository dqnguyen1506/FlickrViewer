using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab_4_Problem_2.Model
{
    /// <summary>
    /// A class that uses a text file to store information about the gym members long-term.
    /// </summary>
    class MemberDB : ObservableObject
    {
        /// <summary>
        /// The list of members to be saved.
        /// </summary>
        private ObservableCollection<Member> members;
        /// <summary>
        /// A member to be saved.
        /// </summary>
        private Member member;
        /// <summary>
        /// Where the database is stored.
        /// </summary>
        private const string filepath = "C:/Users/dungq/Documents/CECS 475/Lab 4/Lab 4 Problem 2/members.txt";

        /// <summary>
        /// Creates a new member database.
        /// </summary>
        /// <param name="m">The list to saved from or written to.</param>
        public MemberDB(ObservableCollection<Member> m)
        {
            members = m;
            
        }

        /// <summary>
        /// Reads the saved text file database into the program's list of members.
        /// </summary>
        /// <returns>The list containing the text file data read in.</returns>
        public ObservableCollection<Member> GetMemberships()
        {
            try
            {
                members = new ObservableCollection<Member>();
                StreamReader input = new StreamReader(new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read));
                string line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = input.ReadLine()) != null)
                {
                    string[] infos = line.Split(' ');
                    member = new Member(infos[0],infos[1], infos[2]);
                    members.Add(member);
                }

                input.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid e-mail address format.");
            }
            return members;
        }

        /// <summary>
        /// Saves the program's list of members into the text file database.
        /// </summary>
        public void SaveMemberships()
        {
            StreamWriter output = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write));
            foreach (var member in members)
            {
                output.WriteLine(member.FirstName + " " + member.LastName + " " + member.Email);
            }
            output.Close();
        }
    }
}

