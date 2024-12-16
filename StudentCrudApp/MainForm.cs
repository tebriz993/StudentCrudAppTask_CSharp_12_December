using System;
using System.Linq;
using System.Windows.Forms;

namespace StudentCrudApp
{
    public partial class MainForm : Form
    {
        private AppDbContext _context;

        public MainForm()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        // ADD
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            var student = new Student
            {
                StudentName = textBox1.Text,
                Surname = textBox2.Text,  
                Email = textBox3.Text
            };

            try
            {
               
                if (_context.Students.Any(s => s.Email == textBox3.Text))
                {
                    MessageBox.Show("Email already exists.");
                    return;
                }

                _context.Students.Add(student);
                _context.SaveChanges();
                MessageBox.Show("Student added Successfully!");
                ClearFields();
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\nInner Exception: " + ex.InnerException.Message;
                }
                MessageBox.Show("Error: " + errorMessage);
            }
        }


        // UPDATE
        private void button2_Click(object sender, EventArgs e)
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == textBox3.Text);
            if (student != null)
            {
                student.StudentName = textBox1.Text;
                student.Surname = textBox2.Text;  
                student.Email = textBox3.Text;

                try
                {
                    _context.SaveChanges();
                    MessageBox.Show("Student updated Successfully!");
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Student not found.");
            }
        }


        // DELETE
        private void button3_Click(object sender, EventArgs e)
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == textBox3.Text);
            if (student != null)
            {
                try
                {
                    _context.Students.Remove(student);
                    _context.SaveChanges();
                    MessageBox.Show("Student deleted Successfully!");
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Student not found.");
            }
        }

        // Helper Method
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        // Dispose of the context when the form is closing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _context.Dispose();
            base.OnFormClosing(e);
        }
    }
}
