using AutoMapper;
using BookStoreAPI.Context;
using BookStoreAPI.Models;

namespace BookStoreAPI.Helper{
    public class AppilicationMapper : Profile{
         public AppilicationMapper(){
             CreateMap<Books,BookstoreModel>().ReverseMap();
         }
    }
}