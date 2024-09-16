
using System.Collections.Generic;
using Testing.Models;

namespace Testing;
//properties 
public interface IProductRepo
{
    // Get all products
    public IEnumerable<Product> GetAllProducts();
    
    // Get a specific product by ID
    public Product GetProduct(int id);
    
    //Update
    public void UpdateProduct(Product product);

    // Insert a new product and return the new product's ID
    public int InsertProduct(Product productToInsert);
    
    // Get all categories
    public IEnumerable<Category> GetCategories();
    
    // Prepare a product with categories assigned (e.g., for insertion)
    public Product AssignCategory();
    
    // Delete a product by its ID
    public void DeleteProduct(Product productId);
}