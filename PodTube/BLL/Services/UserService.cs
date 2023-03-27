using Microsoft.EntityFrameworkCore;
using PodTube.BLL.Converters;
using PodTube.DataAccess.Contexts;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Services {
    public class UserService {

        private PodTubeDbContext dbContext;
        public UserService(PodTubeDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public UserInfo? GetUserById(long id) {
            return dbContext.Users
                .Include(u => u.ProfilePicture)
                .Where(u => u.Id == id)
                .Select(u => u.ToUserInfoDto())
                .FirstOrDefault();
        }
    }
}
