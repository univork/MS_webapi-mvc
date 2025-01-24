using AuthApp.Models;
using AuthApp.Utils;
using AutoMapper;
using EF.Models;

namespace AuthApp.BL
{
    public class UserBL(IMapper mapper)
    {
        private readonly IMapper _mapper = mapper;

        public bool Login(UserLoginViewModel userLoginModel)
        {
            using var context = new AuthdbContext();
            var user = context.Users.FirstOrDefault(
                u => u.Email == userLoginModel.Email
            );
            if (user == null) { 
                return false;
            }

            if (PasswordUtil.VerifyPassword(userLoginModel.Password, user.Password))
            {
                return true;
            }
            return false;
        }

        public void Register(UserCreateViewModel userCreateModel)
        {
            User user = _mapper.Map<User>(userCreateModel);
            using var context = new AuthdbContext();
            context.Users.Add(user);
            context.SaveChanges();
        }

        public UserReadViewModel? Account(string userEmail)
        {
            using AuthdbContext context = new AuthdbContext();
            var user = context.Users.FirstOrDefault(u => u.Email.Equals(userEmail));
            if (user == null) { 
                return null;
            }
            UserReadViewModel userReadModel = _mapper.Map<UserReadViewModel>(user);
            return userReadModel;
        }
    }
}
