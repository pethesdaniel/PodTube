using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PodTube.DataAccess.Contexts;
using PodTube.DataAccess.Entities;
using PodTube.Shared.Models.DTO;
using PodTube.Shared.Models.RequestBody;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Services
{
    public class UserService {

        private PodTubeDbContext dbContext;
        private IMapper mapper;
        private UserManager<User> userManager;
        private TokenService tokenService;
        public UserService(PodTubeDbContext dbContext, UserManager<User> userManager, TokenService tokenService, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<UserDto?> GetUserById(long id) {
            return await dbContext.Users
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IdentityResult?> Register(string username, string email, string password) {
            var result = await userManager.CreateAsync(
            new User { UserName = username, Email = email },
                password
            );
            return result;
        }

        public async Task<bool> AreCredentialsValid(string email, string password) {
            var managedUser = await userManager.FindByEmailAsync(email);
            if (managedUser == null) {
                return false;
            }
            return await userManager.CheckPasswordAsync(managedUser, password);
        }

        public async Task<string> Authorize(string email, string password) {
            var areCredentialsValid = await AreCredentialsValid(email, password);

            if (!areCredentialsValid) {
                throw new ArgumentException("Bad credentials");
            }

            var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (userInDb is null)
                throw new InvalidOperationException("User validation error");
            var accessToken = tokenService.CreateToken(userInDb);
            await dbContext.SaveChangesAsync();
            return accessToken;
        }

        public async Task<long> GetAuthorizedUserId(ClaimsPrincipal principal) {
            var user = await userManager.GetUserAsync(principal);
            return user?.Id ?? 0;
        }
    }
}
