using System.Collections.Generic;

namespace Rx105Cold
    {
    public class ProductRepository
        {
            public static IEnumerable<Product> Create()
            {
                var products = new List<Product>
                                   {
                                       new Product{Id = 1,Name="Keyboard",Category="Computers"},
                                       new Product{Id = 2,Name="Mouse",Category="Computers"},
                                       new Product{Id = 3,Name="Monitor",Category="Computers"},
                                       new Product{Id = 4,Name="CPU",Category="Computers"},
                                       new Product{Id = 5,Name="Grapes",Category="Fruits"},
                                       new Product{Id = 6,Name="Mango",Category="Fruits"},
                                       new Product{Id = 7,Name="Orange",Category="Fruits"},
                                       new Product{Id = 8,Name="Sandwich",Category="Food"},
                                       new Product{Id = 9,Name="Burger",Category="Food"},
                                       new Product{Id = 10,Name="Pizza",Category="Food"},
                                       new Product{Id = 11,Name="Donut",Category="Food"},
                                       new Product{Id = 12,Name="Espresso",Category="Coffee"},
                                       new Product{Id = 13,Name="Latte",Category="Coffee"},
                                       new Product{Id = 14,Name="Mocha",Category="Coffee"}
                                   };
                return products;
            }
        }
    }