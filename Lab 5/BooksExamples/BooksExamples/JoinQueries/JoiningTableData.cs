// JoiningTableData.cs
// Using LINQ to perform a join and aggregate data across tables.
using System;
using System.Linq;
using System.Windows.Forms;

namespace JoinQueries
{
    public partial class JoiningTableData : Form
    {
        public JoiningTableData()
        {
            InitializeComponent();
        } // end constructor

        private void JoiningTableData_Load(object sender, EventArgs e)
        {
            // Entity Framework DBContext
            BooksEntities dbcontext =
               new BooksEntities();
            //a. get authors and title and sort by title
            var authorsAndAllTitles =
                from author in dbcontext.Authors
                from book in author.Titles
                orderby book.Title1
                select new { book.Title1, author.FirstName, author.LastName };

            outputTextBox.AppendText("Titles and Authors ");
            //display authors and titles
            foreach (var element in authorsAndAllTitles)
            {
                outputTextBox.AppendText(
                   String.Format("\r\n\t{0} {1} {2}",
                      element.Title1, element.FirstName, element.LastName));
            }
            //b. Get a list of all the titles and the authors who wrote them. Sort the result by title. 
            //For each title sort the authors alphabetically by last name, then first name.
            var authorsAndTitlesAuthorNamesSort =
                from author in dbcontext.Authors
                from book in author.Titles
                orderby book.Title1, author.LastName, author.FirstName
                select new { book.Title1, author.FirstName, author.LastName };

            outputTextBox.AppendText("\r\n\r\nAuthors and titles with authors sorted for each title: ");
            //display
            foreach (var element in authorsAndTitlesAuthorNamesSort)
            {
                outputTextBox.AppendText(
                   String.Format("\r\n\t{0} {1} {2}",
                      element.Title1, element.FirstName, element.LastName));
            }
            //c. Get a list of all the authors grouped by title, sorted by title;
            //for a given title sort the author names alphabetically by last name first then first name.
            var groupedAuthors =
                from author in dbcontext.Authors
                from book in author.Titles
                //orderby book.Title1, author.LastName, author.FirstName
                group author by book.Title1 into GroupedAuthor
                select GroupedAuthor;

            outputTextBox.AppendText("\r\n\r\nTitles grouped by author: ");

            //display
            foreach (var title in groupedAuthors)
            {
                var names =
                    from srt in title
                    orderby srt.LastName, srt.FirstName
                    select new { srt.FirstName, srt.LastName };
                outputTextBox.AppendText(
                    String.Format("\r\n{0}", title.Key));
                foreach (var name in names)
                {
                    outputTextBox.AppendText(
                   String.Format("\r\n\t{0} {1}",
                      name.FirstName, name.LastName));
                }

            }
            // get authors and ISBNs of each book they co-authored
            var authorsAndISBNs =
               from author in dbcontext.Authors
               from book in author.Titles
               orderby author.LastName, author.FirstName
               select new { author.FirstName, author.LastName, book.ISBN };

            outputTextBox.AppendText("\r\n\r\nAuthors and ISBNs:");

            // display authors and ISBNs in tabular format
            foreach (var element in authorsAndISBNs)
            {
                outputTextBox.AppendText(
                   String.Format("\r\n\t{0,-10} {1,-10} {2,-10}",
                      element.FirstName, element.LastName, element.ISBN));
            } // end foreach

            // get authors and titles of each book they co-authored
            var authorsAndTitles =
               from book in dbcontext.Titles
               from author in book.Authors
               orderby author.LastName, author.FirstName, book.Title1
               select new
               {
                   author.FirstName,
                   author.LastName,
                   book.Title1
               };

            outputTextBox.AppendText("\r\n\r\nAuthors and titles:");

            // display authors and titles in tabular format
            foreach (var element in authorsAndTitles)
            {
                outputTextBox.AppendText(
                   String.Format("\r\n\t{0,-10} {1,-10} {2}",
                      element.FirstName, element.LastName, element.Title1));
            } // end foreach

            // get authors and titles of each book 
            // they co-authored; group by author
            var titlesByAuthor =
               from author in dbcontext.Authors
               orderby author.LastName, author.FirstName
               select new
               {
                   Name = author.FirstName + " " + author.LastName,
                   Titles =
                     from book in author.Titles
                     orderby book.Title1
                     select book.Title1
               };

            outputTextBox.AppendText("\r\n\r\nTitles grouped by author:");

            // display titles written by each author, grouped by author
            foreach (var author in titlesByAuthor)
            {
                // display author's name
                outputTextBox.AppendText("\r\n\t" + author.Name + ":");

                // display titles written by that author
                foreach (var title in author.Titles)
                {
                    outputTextBox.AppendText("\r\n\t\t" + title);
                } // end inner foreach
            } // end outer foreach



        } // end method JoiningTableData_Load

        private void outputTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    } // end class JoiningTableData
} // end namespace JoinQueries

