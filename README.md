# ExceptionDemo
How to handle Exception in submethods or classes in most cases. It could be necessary to catch exception in main or exposed method (for an api) for some reasons. This demo shows how exceptions an theirs stack trace behave. 
Main method call various methods named Method1* that handles exception in differents ways. Methods Method1* call :

1. Method2
2. Method3
3. Method4
4. Method5
5. Method6

Method6 throws a CustomException. Main method catch CustomException :
```cs
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
			.../...
		}
```

## You should not do following catch because it cut stack trace:
```cs
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
```
nor
```cs
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
```

## You should better do :
### Let exception propagates
```cs
        static void Method1Ok()
        {
            Method2();
        }
```
### Log then rethrow:
```cs
        static void Method1GoodRethrow()
        {
            try
            {
                Method2();
            }
            catch (CustomException)
            {
			    # log something relevant
				Console.WriteLine("Something went wrong, you should debug.");
                throw;
            }
        }
```
### Enrich Exception with Data (```cs Dictionary<string, string> ```)
```cs
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
```
