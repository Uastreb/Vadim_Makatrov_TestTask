using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vadim_Makatrov_TestTask
{
    class DataOperations
    {
        DB_connection dB_Connection = new DB_connection();

        private string InsertBookName()
        {
            Console.Write("Введите название книги: ");
            string str = Console.ReadLine();
            if (str.Length <= 30)
            {
                return str;
            }
            else
            {
                Console.WriteLine("Название не должно содержать больше 30 символов");
                return InsertBookName();
            }
        }

        private static int InsertYearOfWritingBook()
        {
            int val = 0;
            bool result = false;
            Console.Write("Введите год написания книги: ");
            result = int.TryParse(Console.ReadLine(), out val);
            if (result == true && val > 0 && val <= DateTime.Now.Year + 10) // на случай, если книга еще пишется
                return val;
            else
            {
                Console.WriteLine("Проверьте вводимое значение");
                return InsertYearOfWritingBook();
            }
        }

        private string InsertAuthorSurname()
        {
            Console.Write("Введите автора: ");
            string str = Console.ReadLine();
            Console.WriteLine();
            if (str.Length <= 20 && str.Length != 0)
            {
                return str;
            }
            else
            {
                Console.WriteLine("Фамилия атвора не должна содержать больше 20 символов");
                return InsertAuthorSurname();
            }
        }

        private void GetBooks()
        {
            dB_Connection.ReadBooks();
        }

        private void GetAuthors()
        {
            dB_Connection.ReadAuthors();
        }

        private void AnotherAuthor(string name_book, int year_of_writing_book, int id_Book)
        {
            while (true)
            {
                Console.WriteLine("\nЧто бы добавить еще одного автора нажмите '1' что бы перейти в меню нажмите '2'");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1)
                {
                    Console.WriteLine();
                    this.ChooseAuthor(id_Book, name_book, year_of_writing_book);  
                }
                if (key.Key == ConsoleKey.D2)
                {
                    Console.Clear();
                    this.Menu();
                    break;
                }

            }
        }

        private void ChooseAuthor(int id_Book, string name_book, int year_of_writing_book)
        {
            while (true)
            {
                this.GetAuthors();
                Console.WriteLine("Что бы выбрать одного из уже добавленных авторов нажмите '1' что бы добавить нового автора нажмите '2'");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1)
                {
                    Console.Write("\nВведите id автора: ");
                    int id_Author = Convert.ToInt32(Console.ReadLine());
                    dB_Connection.AddLink(id_Author, id_Book);
                    this.AnotherAuthor(name_book, year_of_writing_book, id_Book);
                }
                if (key.Key == ConsoleKey.D2)
                {
                    Console.WriteLine();
                    string surname_Authors = InsertAuthorSurname();
                    dB_Connection.AddAuthor(surname_Authors);
                    int id_Author = dB_Connection.GetIdAuthor(surname_Authors);
                    dB_Connection.AddLink(id_Author, id_Book);
                    this.AnotherAuthor(name_book, year_of_writing_book, id_Book);
                }
            }
        }

        private void GetAuthorBooks()
        {
            try
            {
                this.GetAuthors();
                Console.Write("Введите номер автора: ");
                int id_Author = Convert.ToInt32(Console.ReadLine());
                dB_Connection.GetBooksAuthors(id_Author);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Insert()
        {
            try
            {
                string name_book = InsertBookName();
                int year_of_writing_book = InsertYearOfWritingBook();
                dB_Connection.AddBook(name_book, year_of_writing_book);
                Console.WriteLine("Выберите автора:");
                int id_Book = dB_Connection.GetIdBook(name_book, year_of_writing_book);
                ChooseAuthor(id_Book, name_book, year_of_writing_book);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void delete()
        {
            try
            {
                this.GetBooks();
                Console.Write("Введите номер книги:");
                int id = Convert.ToInt32(Console.ReadLine());
                dB_Connection.DeleteBook(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Menu()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Чтобы посмотреть список книг нажмите '1'");
                Console.WriteLine("Чтобы посмотреть список авторов нажмите '2'");
                Console.WriteLine("Чтобы добавить новую книгу в список нажмите '3'");
                Console.WriteLine("Чтобы посмотреть список книг автора нажмите '4'");
                Console.WriteLine("Чтобы удалить книгу нажмите '5'");
                Console.WriteLine("Чтобы выйти нажмите '6'");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine();
                        this.GetBooks();
                        Console.WriteLine("\nДля продолжения нажмите любую клавишу");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine();
                        this.GetAuthors();
                        Console.WriteLine("\nДля продолжения нажмите любую клавишу");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine();
                        this.Insert();
                        Console.WriteLine("\nДля продолжения нажмите любую клавишу");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D4:
                        Console.WriteLine();
                        this.GetAuthorBooks();
                        Console.WriteLine("\nДля продолжения нажмите любую клавишу");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D5:
                        Console.WriteLine();
                        this.delete();
                        Console.WriteLine("\nДля продолжения нажмите любую клавишу");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D6:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
