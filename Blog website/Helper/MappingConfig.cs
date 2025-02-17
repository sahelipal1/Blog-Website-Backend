using AutoMapper;
using Blog_website.DAL.Entity;
using Blog_website.DAL.Entity.DTO;

namespace Blog_website.Helper
{
    public class MappingConfig : Profile
    {
        public MappingConfig() {
            CreateMap<User, UserDTO.ResDTO>().ReverseMap();
            CreateMap<UserDTO.ReqDTO, User>().ReverseMap();
            CreateMap<UserPostDTO, Post>().ReverseMap();

            CreateMap<Post, PostDTO.ResDTO>().ReverseMap();
            CreateMap<PostDTO.UpdateDTO,Post>().ReverseMap();
            // Mapping Category to CategoryDTO (fixed the target class)
            //CreateMap<Category, UserPostDTO>().ReverseMap();
            //    .ForMember(dest => dest.PostTitles, opt => opt.MapFrom(src => src.Posts.Select(p => p.Title).ToList()))
            //    



        }
    }

}
