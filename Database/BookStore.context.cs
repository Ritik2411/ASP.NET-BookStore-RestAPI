using BookStoreAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Context{
    public class BookStoreContext : IdentityDbContext<UserModel>{
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options){
            
        }

        public DbSet<Books> Books { get;set; }
    }
}