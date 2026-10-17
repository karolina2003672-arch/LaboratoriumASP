using LabApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LabApp.Controllers;

public class BookController : Controller
{
    // GET

    public IActionResult Index()
    {
        return View(model:BookStoreMemory.Books);
    }
[HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(BookFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return  View(model);
        }
        BookStoreMemory.Books.Add(new Book()
        {
            Id = BookStoreMemory.Books.Max(x => x.Id)+1,
            Title = model.Title,
            Author = model.Author,
            Price = model.Price,
            NumPages =  model.NumPages,
            Isbn = model.Isbn,
            PublicationDate = model.PublicationDate
        });
        return RedirectToAction(nameof(Index));
    }
}