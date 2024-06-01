using Microsoft.AspNetCore.Mvc;

namespace DemoPresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static List<Book> _books = new List<Book>
    {
        new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
        new Book { Id = 2, Title = "Book 2", Author = "Author 2" },
        new Book { Id = 3, Title = "Book 3", Author = "Author 3" }
    };

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            _books.Add(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var existingBook = _books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
                return NotFound();

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.Id == id);
            if (bookToRemove == null)
                return NotFound();

            _books.Remove(bookToRemove);
            return NoContent();
        }
    }
}
