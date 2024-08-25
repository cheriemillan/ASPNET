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

    public void UpdateProduct(Product product)
    {
        _connection.Execute(
            "UPDATE PRODUCTS SET NAME = @updatedName, PRICE = @updatedPrice WHERE ProductID = @productID",
        new
        {
            updatedName = product.Name, updatedPrice = product.Price, updatedCategoryID = product.CategoryID,
            updatedOnSale = product.OnSale, updatedStockLevel = product.StockLevel, productID = product.ProductID
        });
    }

    public int InsertProduct(Product productToInsert)
    {
        var lastCreatedID= _connection.QuerySingleOrDefault<int>(
            "INSERT INTO PRODUCTS(NAME, PRICE, CATEGORYID) VALUES (@newProductName, @newProductPrice, @newProductCategoryId); SELECT LAST_INSERT_ID();",
            new
            {
                newProductName = productToInsert.Name,
                newProductPrice = productToInsert.Price,
                newProductCategoryId = productToInsert.CategoryID
            });
        
        return lastCreatedID;
    }

    public IEnumerable<Category> GetCategories()
    {
        var categories = _connection.Query<Category>("SELECT * FROM CATEGORIES");

        return categories;
    }

    public Product AssignCategory()
    {
        var categories = GetCategories();

        var product = new Product();
        product.Categories = categories;

        return product;
    }
}

