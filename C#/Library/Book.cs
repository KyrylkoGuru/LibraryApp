using System;
using System.Xml.Serialization;

namespace Library
{
    [Serializable]
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }
        public string ISBN { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }


        public Book() { }

        public Book(string title, string author, int yearPublished, string isbn, string description, string imagePath)
        {
            Title = title;
            Author = author;
            YearPublished = yearPublished;
            ISBN = isbn;
            Description = description;
            ImagePath = imagePath;
        }
    }
}
