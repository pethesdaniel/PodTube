using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
        private IMapper mapper;
        public UserService(PodTubeDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public UserInfo? GetUserById(long id) {
            return dbContext.Users
                .Include(u => u.ProfilePicture)
                .Where(u => u.Id == id)
                .ProjectTo<UserInfo>(mapper.ConfigurationProvider)
                .FirstOrDefault();
        }
    }
}
