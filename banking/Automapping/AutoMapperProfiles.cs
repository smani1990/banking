using AutoMapper;
using banking.Models;
using banking.Models.DTO;

namespace banking.Automapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<NewUserAccountDTO, User>().ReverseMap();
            CreateMap<AccountDTO, Account>().ReverseMap();
            CreateMap<User, NewUserAccountDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();
        }
        }
}
