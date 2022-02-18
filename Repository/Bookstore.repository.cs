using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreAPI.Context;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace BookStoreAPI.Repository{
    public class BookStoreRepository : IBookStore{
        private readonly BookStoreContext _context;
        public BookStoreRepository(BookStoreContext context){
            _context = context;
        }

        public async Task<List<BookstoreModel>> GetAllBooksAsync(){
            var records = await _context.Books.Select(x => new BookstoreModel(){
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).ToListAsync();

            return records;
        }

        public async Task<BookstoreModel> GetBooksByIdAsync(int id){
            var records = await _context.Books.Where(x => x.Id == id).Select(x => new BookstoreModel(){
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).FirstOrDefaultAsync();

            return records;
        }

        public async Task<int> AddBookAsync(BookstoreModel bookstoreModel){
            var book = new Books(){
                Title = bookstoreModel.Title,
                Description = bookstoreModel.Description
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBookAsync(int id,BookstoreModel bookstoreModel){
            var book = await _context.Books.FindAsync(id);
            if(book != null){
                book.Title = bookstoreModel.Title;
                book.Description = bookstoreModel.Description;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id){
            var book = new Books(){
                Id = id
            };

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookPatchAsync(int id,JsonPatchDocument bookstoreModel){
            var book = await _context.Books.FindAsync(id);
            if(book != null){
                bookstoreModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}