using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreAPI.Repository{
    public interface IBookStore{
        Task<List<BookstoreModel>> GetAllBooksAsync();
        Task<BookstoreModel> GetBooksByIdAsync(int id);
        Task<int> AddBookAsync(BookstoreModel bookstoreModel);
        Task UpdateBookAsync(int id, BookstoreModel bookstoreModel);
        Task DeleteBookAsync(int id);
        Task UpdateBookPatchAsync(int id,JsonPatchDocument bookstoreModel);
    }
}