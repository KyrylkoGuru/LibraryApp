using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace Library
{
    public partial class Form1 : Form
    {
        private Library _library;
        public Form1()
        {
            InitializeComponent();
            _library = new Library();
            _library.LoadFromFile(@"D:\Images\Saves\libraryData.xml"); // Завантажуємо дані при запуску
            InitializeBookPanel();
            DisplayBooks();
        }
        private void DisplayBooks()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var book in _library.GetAllBooks())
            {
                flowLayoutPanel1.Controls.Add(CreateBookPanel(book));
            }
        }

        private void InitializeBookPanel()
        {
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false; // Щоб нові книги розміщувались під наявними
            flowLayoutPanel1.AutoScroll = true;
        }
        public void UpdateBookPanel(Book updatedBook)
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is Panel panel)
                {
                    var pictureBox = panel.Controls.OfType<PictureBox>().FirstOrDefault();
                    var lblTitle = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Назва:"));
                    var lblAuthor = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Автор:"));
                    var lblYear = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Text.StartsWith("Рік випуску:"));

                    if (lblTitle != null && lblAuthor != null && lblYear != null)
                    {
                        if (lblTitle.Text.Contains(updatedBook.ISBN))
                        {
                            lblTitle.Text = $"Назва: {updatedBook.Title}";
                            lblAuthor.Text = $"Автор: {updatedBook.Author}";
                            lblYear.Text = $"Рік випуску: {updatedBook.YearPublished}";

                            if (pictureBox != null && File.Exists(updatedBook.ImagePath))
                            {
                                pictureBox.Image = Image.FromFile(updatedBook.ImagePath);
                                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                            return;
                        }
                    }
                }
            }
        }

        public void AddBookToPanel(Book book)
        {
            _library.AddBook(book);
            flowLayoutPanel1.Controls.Add(CreateBookPanel(book));
        }

        private Panel CreateBookPanel(Book book)
        {
            Panel panel = new Panel
            {
                Size = new Size(485, 100),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5),
                Margin = new Padding(5)
            };
            PictureBox pictureBox = new PictureBox
            {
                Image = Image.FromFile(book.ImagePath),
                Size = new Size(100, 90),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            Label lblTitle = new Label
            {
                Text = $"Назва: {book.Title}",
                AutoSize = true
            };

            Label lblAuthor = new Label
            {
                Text = $"Автор: {book.Author}",
                AutoSize = true
            };

            Label lblYear = new Label
            {
                Text = $"Рік випуску: {book.YearPublished}",
                AutoSize = true
            };

            Button btnDetails = new Button
            {
                Text = "Подробиці",
                Size = new Size(75, 30)
            };

            Button btnDelete = new Button
            {
                Text = "Видалити",
                Size = new Size(75, 30)
            };

            // Елементи панелі
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblAuthor);
            panel.Controls.Add(lblYear);
            panel.Controls.Add(btnDetails);
            panel.Controls.Add(btnDelete);

            // Положення елементів
            pictureBox.Location = new Point(5, 5);
            lblTitle.Location = new Point(110, 5);
            lblAuthor.Location = new Point(110, 25);
            lblYear.Location = new Point(110, 45);
            btnDetails.Location = new Point(400, 10);
            btnDelete.Location = new Point(400, 55); 

            btnDetails.Click += (sender, e) =>
            {
                Form2 detailsForm = new Form2(book); // Створення Form2 з об'єктом Book
                detailsForm.ShowDialog(); 
            };

            btnDelete.Click += (sender, e) =>
            {
                if (MessageBox.Show("Ви впевнені, що хочете видалити цю книгу?", "Підтвердження видалення", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _library.RemoveBook(book.ISBN); // Видалити з бібліотеки
                    flowLayoutPanel1.Controls.Remove(panel);
                    panel.Dispose(); // Звільнення ресурсів
                    _library.SaveToFile(@"D:\Images\Saves\libraryData.xml");
                }
            };

            label1.Hide();
            return panel;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void FilterBooks()
        {
            string searchText = textBox1.Text.ToLower();
            var filteredBooks = _library.SearchBooks(searchText);

            flowLayoutPanel1.Controls.Clear();

            foreach (var book in filteredBooks)
            {
                flowLayoutPanel1.Controls.Add(CreateBookPanel(book)); // Додаємо тільки відфільтровані панелі книг
            }

            if (filteredBooks.Count == 0)
            {
                label1.Show();
                label1.Text = "Книги за запитом не знайдено";
            }
        }


        public void SetBook(Book book)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Відкриття Form2 без передачі книги для створення нової книги
            Form2 addBookForm = new Form2();
            addBookForm.ShowDialog(); // Відкриття Form2
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _library.SaveToFile(@"D:\Images\Saves\libraryData.xml");
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterBooks();
        }
    }

    public class Library
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public bool RemoveBook(string isbn)
        {
            var book = books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
                books.Remove(book);
                return true;
            }
            return false;
        }

        public List<Book> SearchBooks(string query)
        {
            return books.Where(b => b.Title.ToLower().Contains(query) ||
                                    b.Author.ToLower().Contains(query) ||
                                    b.YearPublished.ToString().Contains(query) ||
                                    b.ISBN.ToString().Contains(query)).ToList();
        }


        public List<Book> GetAllBooks()
        {
            return books;
        }
        public void SaveToFile(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, books);
            }
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Book>));
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    books = (List<Book>)serializer.Deserialize(stream);
                }
            }
        }


    }
}
