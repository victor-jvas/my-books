using System;

namespace my_books.Exceptions
{
    public class BookAlreadyExistException : Exception
    {
        public BookAlreadyExistException():base("The book already exists")
        {
            
        }
    }
}