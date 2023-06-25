using BookWebApp.Helpers;
using BookWebApp.Models;
using BookWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookWebApp.Services
{
    public class BookService
    {
        private readonly HttpClient _client;
        Uri uri = new Uri(Setting.baseAddress);

        public BookService()
        {
            _client = new HttpClient();
            _client.BaseAddress = uri;
        }
        //public BookVM GetAuthorsIds(BookVM bookVM)
        //{
        //    BookVM _book = new BookVM
        //    {
        //        Title = bookVM.Title,
        //        Description = bookVM.Description,
        //        IsRead = bookVM.IsRead,
        //        DateRead = bookVM.IsRead ? bookVM.DateRead.Value : null,
        //        Rate = bookVM.IsRead ? bookVM.Rate.Value : null,
        //        Genre = bookVM.Genre,
        //        CoverUrl = bookVM.CoverUrl,
        //        publisherId = bookVM.publisherId,
        //        //Authors=bookVM.Authors
               
        //    };
        //    foreach (var authorId in _book.Authors)
        //    {
        //        var _Author_Book = new Book_author()
        //        {
        //            bookId = _book.Id,
        //            AuthorId = authorId
        //        };
        //    }

        //    return bookVM;

        //}

    }
}
