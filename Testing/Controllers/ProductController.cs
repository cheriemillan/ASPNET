using Microsoft.AspNetCore.Mvc;

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
        
        return View(products);
    }

    public IActionResult ViewProduct(int id)
    {
        var product = _repo.GetProduct(id);
        return View(product);
    }
}