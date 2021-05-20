using System;

namespace my_books.Exceptions
{
    public class BookNotRegisteredException : Exception
    {
        public BookNotRegisteredException():base("This book is not registered")
        {
            
        }
    }
}