using allu_decor_be.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using allu_decor_be.Helpers;
using allu_decor_be.Authorization;
using System;

namespace allu_decor_be.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(string id);
        void CreateUser(User user);
    }

    public class UserService : IUserService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UserService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }


        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.Password))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public void CreateUser(User user)
        {
            if (_context.Users.Any(x => x.Email == user.Email))
            {
                throw new AppException("User with email address: " + user.Email + "already exists");
            }

            user.Id = Guid.NewGuid().ToString();
            user.Role = "Client";

            string hashedPassword = BCryptNet.HashPassword(user.Password);
            user.Password = hashedPassword;

            _context.Users.Add(user);
            _context.SaveChanges();            
        }
    }
}
