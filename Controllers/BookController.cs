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

    public async Task<IActionResult> ByYear(int year)
    {
        var books = await BookStoreMemory.Books.Where(b => b.PublicationDate == year).ToListAsync();
       /* filtruj listę po roku, zwróć widok Index */
        return View("index", books);

    }

    public async Task<IActionResult> ByIsbn(string isbn)
    {
        if(string.IsNullOrWhiteSpace(isbn)){
            return NotFound();
        }

        var book = await BookStoreMemory.Books.FirstOrDefault(b => b.Isbn == isbn);
        if (book == null)
        {
            return NotFound();
        }
        /* znajdź książkę, przekieruj na Details lub NotFound() */
    }
}