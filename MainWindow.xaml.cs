using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookCatalogApp.Data;

namespace BookCatalogApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DatabaseService db = new DatabaseService();
        public MainWindow()
        {
            InitializeComponent();
            LoadBooks();
        }
        private void LoadBooks()
        {
            BooksGrid.ItemsSource = db.GetAllBooks();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int year;

            if (!int.TryParse(YearBox.Text, out year))
            {
                MessageBox.Show("Год должен быть числом!");
                return;
            }

            var book = new Book
            {
                Title = TitleBox.Text,
                Author = AuthorBox.Text,
                Year = year
            };

            db.AddBook(book);
            LoadBooks();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (BooksGrid.SelectedItem is Book selectedBook)
            {
                db.DeleteBook(selectedBook.Id);
                LoadBooks();
            }
            else
            {
                MessageBox.Show("Выбери книгу!");
            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            BooksGrid.ItemsSource = db.SearchBooks(TitleBox.Text);
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }
    }
}
