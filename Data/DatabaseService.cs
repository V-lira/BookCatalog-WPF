using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace BookCatalogApp.Data
{
    public class DatabaseService
    {
        private string connectionString = "Data Source=books.db";

        public DatabaseService()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
            CREATE TABLE IF NOT EXISTS Books (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Author TEXT NOT NULL,
                Year INTEGER
            );
            ";

            command.ExecuteNonQuery();
        }
        public void AddBook(Book book)
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
            INSERT INTO Books (Title, Author, Year)
            VALUES ($title, $author, $year);
            ";

            command.Parameters.AddWithValue("$title", book.Title);
            command.Parameters.AddWithValue("$author", book.Author);
            command.Parameters.AddWithValue("$year", book.Year);

            command.ExecuteNonQuery();
        }
        public void DeleteBook(int id)
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Books WHERE Id = $id;";
            command.Parameters.AddWithValue("$id", id);

            command.ExecuteNonQuery();
        }
        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();

            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Books;";

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(new Book
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    Year = reader.GetInt32(3)
                });
            }

            return books;
        }
        public List<Book> SearchBooks(string title)
        {
            var books = new List<Book>();

            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            "SELECT * FROM Books WHERE Title LIKE $title;";
            command.Parameters.AddWithValue("$title", "%" + title + "%");

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                books.Add(new Book
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    Year = reader.GetInt32(3)
                });
            }

            return books;
        }
    }
}