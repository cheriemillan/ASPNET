using Microsoft.AspNetCore.Mvc;
using Testing.Models;

namespace Testing.Controllers;

public class ProductController : Controller
{
    //Field
    private readonly IProductRepo _repo;
    
    //Constructor
    public ProductController(IProductRepo repo)
    {
        _repo = repo;
    }
    
    // GET: Display all products
    public IActionResult Index()
    {
        var products = _repo.GetAllProducts();

        ViewBag.SomethingProperty = "Hello";
        
        return View(products);
    }

    // GET: Display a specific product by ID
    public IActionResult ViewProduct(int id)
    {
        var product = _repo.GetProduct(id);
        
        return View(product);
    }

    // GET: Prepare to update a product by ID
    public IActionResult UpdateProduct(int id)
    {
        var product = _repo.GetProduct(id);

        if (product is null)
        {
            return View("Error", new ErrorViewModel());
        }

        return View(product);
    }

    // POST: Update product in the database
    public IActionResult UpdateProductToDataBase(Product product)
    {
        _repo.UpdateProduct(product);

        return RedirectToAction("ViewProduct", new { id = product.ProductID });
    }

    // GET: Prepare to insert a new product
    public IActionResult InsertProduct()
    {
        var product = _repo.AssignCategory();
        
        return View(product);
    }

    // POST: Insert a new product into the database
    public IActionResult InsertProductToDatabase(Product product)
    {
        var id= _repo.InsertProduct(product);

        return RedirectToAction("ViewProduct", new {id});
    }
    
    // POST: Delete a product from the database
    public IActionResult DeleteProduct(Product product)
    {
        _repo.DeleteProduct(product);

        return RedirectToAction("Index");
    }
}