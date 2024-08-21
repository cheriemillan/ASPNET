using System.Collections.Generic;
using System.Data;
using Dapper;
using Testing.Models;

namespace Testing;

public class ProductRepo : IProductRepo
{
    //field (class level variable)
    private readonly IDbConnection _connection;
    
    //Constructor
    public ProductRepo(IDbConnection connection)
    {
        // Connect to database
        _connection = connection;
    }
    public IEnumerable<Product> GetAllProducts()
    {
        // Run the SQL command to get the data
        var products = _connection.Query<Product>("SELECT * FROM PRODUCTS");
        
        // Return the data from the database
        return products;
    }

    public Product GetProduct(int id)
    {
        return _connection.QuerySingle<Product>("SELECT * FROM PRODUCTS WHERE PRODUCTID = @id", new { id = id });
    }
}

