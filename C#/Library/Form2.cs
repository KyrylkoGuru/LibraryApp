using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library;

namespace Library
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text;
            string author = textBox2.Text;
            int yearPublished = 0;

            if (!int.TryParse(textBox4.Text, out yearPublished))
            {
                MessageBox.Show("Введіть правильний рік видання.");
                return;
            }

            string isbn = textBox5.Text;
            string description = textBox3.Text;

            Book newBook = new Book(title, author, yearPublished, isbn, description);

            Form1 mainForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (mainForm != null)
            {
                mainForm.SetBook(newBook);
            }

            this.Close(); // Закриття Form2 після збереження
        }

    }
    
}
