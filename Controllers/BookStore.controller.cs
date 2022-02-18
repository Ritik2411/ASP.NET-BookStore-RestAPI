using System.Threading.Tasks;
using BookStoreAPI.Models;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers{
    [ApiController]
    [Route("api/[action]")]
    public class BookStoreController : ControllerBase{
        private readonly IBookStore _bookstore;
        public BookStoreController(IBookStore bookStore){
            _bookstore = bookStore;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResult(){
            var books = await _bookstore.GetAllBooksAsync();
            return Ok(books); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooksById([FromRoute]int id){
            var records = await _bookstore.GetBooksByIdAsync(id);

            if(records == null){
                return NotFound();
            }
            else{
                return Ok(records);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody]BookstoreModel bookStoreModel){
            var id = await _bookstore.AddBookAsync(bookStoreModel);
            return CreatedAtAction(nameof(GetBooksById), new{id = id, Controller="BookStore"}, new { id = bookStoreModel.Id, title = bookStoreModel.Title, description = bookStoreModel.Description});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute]int id, [FromBody]BookstoreModel bookstoreModel){
            var response = new ResponseModel(){
                msg = "Updated Successfully" 
            };
            await _bookstore.UpdateBookAsync(id, bookstoreModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute]int id){
            var response = new ResponseModel(){
                msg = "Delete Successfully" 
            };

            await _bookstore.DeleteBookAsync(id);
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBookPatch([FromRoute]int id,[FromBody]JsonPatchDocument bookstoreModel){
            var reponse = new ResponseModel(){
                msg = "Patched Successfully"
            };
            await _bookstore.UpdateBookPatchAsync(id,bookstoreModel);
            return Ok();
        }
    }
}