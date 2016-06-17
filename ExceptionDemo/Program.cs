using System;

namespace ExceptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NewSection("Full exception stack trace : no try catch in called methods");
                Method1Ok();
            }
            catch (CustomException oe)
            {
                Console.WriteLine(oe);
            }

            try
            {
                NewSection("Bad rethrow : throw oe cut exception stack trace");
                Method1BadRethrowCutStack();
            }
            catch (CustomException oe)
            {
                Console.WriteLine(oe);
            }

            try
            {
                NewSection("Bad rethrow : throw new Exception cut exception stack trace");
                Method1BadExceptionHandling();
            }
            catch (CustomException oe)
            {
                Console.WriteLine(oe);
            }

            try
            {
                NewSection("Good rethrow : keep stack trace");
                Method1GoodRethrow();
            }
            catch (CustomException oe)
            {
                Console.WriteLine(oe);
            }

            try
            {
                NewSection("Enriched exception");
                Method1EnrichException();
            }
            catch (CustomException oe)
            {
                foreach(string key in oe.Data.Keys)
                {
                    Console.WriteLine(string.Format("key : {0}, value : {1}", key, oe.Data[key]));
                }
                
                Console.WriteLine(oe);
            }


            Console.ReadLine();
        }
        static void NewSection(string desc)
        {
            Console.WriteLine("\n------------------------------------------");
            Console.WriteLine(desc);
            Console.WriteLine("------------------------------------------");
        }

        static void Method1Ok()
        {
            Method2();
        }

        static void Method1BadRethrowCutStack()
        {
            try
            {
                Method2();
            }
            catch (CustomException oe)
            {
                throw oe;
            }
        }

        static void Method1BadExceptionHandling()
        {
            try
            {
                Method2();
            }
            catch (CustomException)
            {
                throw new CustomException("Something went wrong. I don't know where");
            }
        }

        static void Method1GoodRethrow()
        {
            try
            {
                Method2();
            }
            catch (CustomException)
            {
                throw;
            }
        }

        static void Method1EnrichException()
        {
            try
            {
                Method2();
            }
            catch (CustomException e)
            {
                e.Data.Add("one key", "one value");
                throw;
            }
        }

        static void Method2()
        {
            Method3();
        }
        static void Method3()
        {
            Method4();
        }
        static void Method4()
        {
            Method5();
        }
        static void Method5()
        {
            Method6();
        }
        
        static void Method6()
        {
            throw new CustomException("I fail whatever I try to do");
        }
    }
}
