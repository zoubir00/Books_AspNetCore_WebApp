﻿using BookWebApp.Models;

namespace BookWebApp.Helpers
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? Exparedate { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
