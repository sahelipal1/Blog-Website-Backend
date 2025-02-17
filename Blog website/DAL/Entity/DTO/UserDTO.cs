using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_website.DAL.Entity.DTO
{
    public class UserDTO
    {
        public class ResDTO
        {
            public int Id { get; set; }


            public string Name { get; set; }

            public string? Email { get; set; } = string.Empty; // Default value for null emails
            public string? Phonenumber { get; set; }


            public DateOnly? Createddate { get; set; }

      
            public DateOnly? Updateddate { get; set; }

     
            public int? Createdby { get; set; }

            public int? Updatedby { get; set; }



            public bool? IsActive { get; set; }
            public bool? IsDeleted { get; set; }

        }
        public class ReqDTO
        {
            public string Name { get; set; }

            public string Email { get; set; }
            public string Phonenumber { get; set; }
           // public string Password { get; set; }
            public int? Updatedby { get; internal set; }
        }
    }
}