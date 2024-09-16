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
    
    // Retrieve a specific product by its ID
    public Product GetProduct(int id)
    {
        return _connection.QuerySingle<Product>("SELECT * FROM PRODUCTS WHERE PRODUCTID = @id", new { id = id });
    }

    // Update an existing product in the database
    public void UpdateProduct(Product product)
    {
        _connection.Execute(
            "UPDATE PRODUCTS SET NAME = @updatedName, PRICE = @updatedPrice WHERE ProductID = @productID",
        new
        {
            updatedName = product.Name, updatedPrice = product.Price, updatedCategoryID = product.CategoryID,
            updatedOnSale = product.OnSale, updatedStockLevel = product.StockLevel, productID = product.ProductID
        }); // Execute SQL query to update product details
    }
    
    // Insert a new product into the database and return its ID
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

    
    // Retrieve all categories from the database
    public IEnumerable<Category> GetCategories()
    {
        var categories = _connection.Query<Category>("SELECT * FROM CATEGORIES");

        return categories;
    }

    // Prepare a new product with categories assigned
    public Product AssignCategory()
    {
        var categories = GetCategories(); // Retrieve all categories

        var product = new Product(); // Create a new product instance
        product.Categories = categories; // Assign the list of categories to the product

        return product;  // Return the product with categories
    }

    // Delete a product and its associated records from the database
    public void DeleteProduct(Product productToDelete)
    {
        _connection.Execute("DELETE FROM REVIEWS WHERE PRODUCTID = @productId", new {productId = productToDelete.ProductID});
        _connection.Execute("DELETE FROM SALES WHERE PRODUCTID = @productId", new {productId = productToDelete.ProductID});
        _connection.Execute("DELETE FROM PRODUCTS WHERE PRODUCTID = @productId", new {productId = productToDelete.ProductID});
    }
}

