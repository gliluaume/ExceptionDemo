using System;

namespace ExceptionDemo
{
    class CustomException : Exception
    {
        public CustomException(string msg) : base(msg) { }
    }
}
