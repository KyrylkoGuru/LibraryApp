using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }

        public Book(string title, string author, int yearPublished, string isbn, string description)
        {
            Title = title;
            Author = author;
            YearPublished = yearPublished;
            ISBN = isbn;
            Description = description;
        }
    }
}
