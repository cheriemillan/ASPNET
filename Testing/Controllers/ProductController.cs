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
    
    // GET
    public IActionResult Index()
    {
        var products = _repo.GetAllProducts();

        ViewBag.SomethingProperty = "Hello";
        
        return View(products);
    }

    public IActionResult ViewProduct(int id)
    {
        var product = _repo.GetProduct(id);
        
        return View(product);
    }

    public IActionResult UpdateProduct(int id)
    {
        var product = _repo.GetProduct(id);

        if (product is null)
        {
            return View("Error", new ErrorViewModel());
        }

        return View(product);
    }

    public IActionResult UpdateProductToDataBAse(Product product)
    {
        _repo.UpdateProduct(product);

        return RedirectToAction("ViewProduct", new { id = product.ProductID });
    }

    public IActionResult InsertProduct()
    {
        var product = _repo.AssignCategory();
        
        return View(product);
    }

    public IActionResult InsertProductToDatabase(Product product)
    {
        var id= _repo.InsertProduct(product);

        return RedirectToAction("ViewProduct", new {id});
    }

    public IActionResult DeleteProduct(Product product)
    {
        _repo.DeleteProduct(product);

        return RedirectToAction("Index");
    }
}