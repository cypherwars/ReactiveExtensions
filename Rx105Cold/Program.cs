using System.Reactive.Linq;
using System;

namespace Rx105Cold
    {
    class Program
        {
        static void Main(string[] args)
        {
            ColdAccess();
            Console.ReadLine();
        }

        private static void ColdAccess()
        {
            var products = ProductRepository.Create().ToObservable().Take(5);
            products.Subscribe(x => Console.WriteLine(x.Name));
         }
        }
    }