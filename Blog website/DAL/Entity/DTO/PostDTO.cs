namespace Blog_website.DAL.Entity.DTO
{
    public class PostDTO

    {
        public class ResDTO
        {
            //public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int CategoryId { get; set; }
            public bool IsPublished { get; set; }
            public bool IsDeleted { get; set; }

        }
        public class UpdateDTO
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public int? CategoryId { get; set; }
            public bool? IsPublished { get; set; }
            public bool IsDeleted {  get; set; }
            //public string CategoryName { get; set; }
            //public string CategoryDescription { get; set; }
        }
    }
}
