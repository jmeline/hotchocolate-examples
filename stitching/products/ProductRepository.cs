using System;
using System.Collections.Generic;
using System.Linq;

namespace Products;

public class ProductRepository
{
    private readonly Dictionary<int, Product> _products;

    public ProductRepository()
    {
        _products = new Product[]
        {
            new Product(1, "Table", 899, 100, new DateOnly(2024, 01, 01), DateTime.Now),
            new Product(2, "Couch", 1299, 1000, new DateOnly(2024, 02, 02), DateTime.Today),
            new Product(3, "Chair", 54, 50, new DateOnly(2024, 03, 03), DateTime.UtcNow)
        }.ToDictionary(t => t.Upc);
    }

    public IEnumerable<Product> GetTopProducts(int first) =>
        _products.Values.OrderBy(t => t.Upc).Take(first);

    public Product GetProduct(int upc) => _products[upc];
}