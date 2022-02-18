using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models{
    public class BookstoreModel{
        public int Id { get;set; }

        [Required]
        [MinLength(4)]
        public string Title { get;set; }
        public string Description { get;set; }
    }
}