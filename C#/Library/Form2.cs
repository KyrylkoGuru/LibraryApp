using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing; // Додано для класу Image
using Library;

namespace Library
{
    public partial class Form2 : Form
    {
        private Book _book;
        private string _selectedImagePath;
        private bool _isEditMode;

        public Form2(Book book = null)
        {
            InitializeComponent();
            if (book != null)
            {
                _book = book;
                _isEditMode = true;
                LoadBookData();
            }
            else
            {
                _book = new Book();
                _isEditMode = false;
            }
        }

        private void LoadBookData()
        {
            // Заповнюємо поля форми даними з об'єкта Book
            textBox1.Text = _book.Title;
            textBox2.Text = _book.Author;
            textBox4.Text = _book.YearPublished.ToString();
            textBox5.Text = _book.ISBN;
            textBox3.Text = _book.Description;

            // Завантажити зображення книги, якщо воно існує
            if (File.Exists(_book.ImagePath))
            {
                pictureBox1.Image = Image.FromFile(_book.ImagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.Image = null; // Очистити зображення, якщо файл не знайдено
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Сховати форму без закриття
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Оновлюємо або створюємо книгу з полів форми
            _book.Title = textBox1.Text;
            _book.Author = textBox2.Text;
            int yearPublished;
            if (int.TryParse(textBox4.Text, out yearPublished))
            {
                _book.YearPublished = yearPublished;
            }
            _book.ISBN = textBox5.Text;
            _book.Description = textBox3.Text;

            // Встановлюємо шлях до зображення, якщо воно вибране
            _book.ImagePath = _selectedImagePath ?? _book.ImagePath;

            // Оновити або додати книгу у Form1, якщо потрібно
            Form1 mainForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (mainForm != null)
            {
                if (_isEditMode)
                {
                    mainForm.UpdateBookPanel(_book);
                }
                else
                {
                    mainForm.AddBookToPanel(_book);
                }
            }

            this.Close(); // Закриття Form2 після збереження
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Відкрити діалогове вікно для вибору файлу
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Зберегти шлях до вибраного файлу
                _selectedImagePath = openFileDialog.FileName;

                // Відобразити вибране фото у PictureBox
                pictureBox1.Image = Image.FromFile(_selectedImagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Ваш код для обробки кліку по label1
        }
    }
}
