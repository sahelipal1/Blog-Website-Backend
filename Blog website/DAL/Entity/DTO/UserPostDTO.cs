﻿namespace Blog_website.DAL.Entity.DTO
{
    public class UserPostDTO
    {
        public int UserId { get; set; }
        public int PostId { get; set; }

        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsPublished { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
     
        
        public List<string> PostTitles { get; set; }  // This will contain titles of related posts
    }
}



