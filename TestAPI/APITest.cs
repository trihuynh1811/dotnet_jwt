using DemoPresentation;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace TestAPI
{
    [TestClass]
    public class APITest
    {
        List<Book> _books = new List<Book>
    {
        new Book { Id = 1, Title = "Book 1", Author = "Author 1" },
        new Book { Id = 2, Title = "Book 2", Author = "Author 2" },
        new Book { Id = 3, Title = "Book 3", Author = "Author 3" }
    };
        [TestMethod]
        public async Task GetAllBook()
        {

            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateClient();

            var res = await httpClient.GetAsync("/api/Books");
            var listResult = await res.Content.ReadAsStringAsync();
            List<Book>? books = JsonConvert.DeserializeObject<List<Book>>(listResult);
            bool same = books != null ?
                _books.Select(x => x.Id).ToHashSet().SetEquals(books.Select(x => x.Id)) : false;
            Assert.AreEqual(true, same);
        }

        [TestMethod]
        public async Task GetBookById()
        {

            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateClient();

            var res = await httpClient.GetAsync("/api/Books/1");
            var result = await res.Content.ReadAsStringAsync();
            Book? book = JsonConvert.DeserializeObject<Book>(result);
            bool same = book != null ? _books[0].Id.Equals(book.Id) : false;
            Assert.AreEqual(true, same);
        }

        [TestMethod]
        public async Task AddBook()
        {
            var newBook = new Book
            {
                Id = 4,
                Title = "New Book",
                Author = "New Author"
            };
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(newBook), System.Text.Encoding.UTF8, "application/json");
            var res = await httpClient.PostAsync("/api/Books", content);
            res.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual(System.Net.HttpStatusCode.Created, res.StatusCode);
        }

        [TestMethod]
        public async Task UpdateBook()
        {
            var newBook = new Book
            {
                Title = "gg",
                Author = "lol"
            };
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(newBook), System.Text.Encoding.UTF8, "application/json");
            var res = await httpClient.PutAsync("/api/Books/1", content);
            res.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, res.StatusCode);
        }

        [TestMethod]
        public async Task DeleteBook()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateClient();

            var res = await httpClient.DeleteAsync("/api/Books/1");
            res.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, res.StatusCode);
        }
    }
}