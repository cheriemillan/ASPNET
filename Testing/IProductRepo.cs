
using System.Collections.Generic;
using Testing.Models;

namespace Testing;

public interface IProductRepo
{
    public IEnumerable<Product> GetAllProducts();
    public Product GetProduct(int id);
    
    //Update
    public void UpdateProduct(Product product);

    public int InsertProduct(Product productToInsert);
    public IEnumerable<Category> GetCategories();
    public Product AssignCategory();
    public void DeleteProduct(Product productId);
}