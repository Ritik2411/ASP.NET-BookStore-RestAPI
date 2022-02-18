using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Context{
    public class BookStoreContext : DbContext{
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options){
            
        }

        public DbSet<Books> Books { get;set; }
    }
}