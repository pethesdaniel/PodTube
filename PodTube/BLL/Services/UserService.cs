﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PodTube.DataAccess.Contexts;
using PodTube.Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Services
{
    public class UserService {

        private PodTubeDbContext dbContext;
        private IMapper mapper;
        public UserService(PodTubeDbContext dbContext, IMapper mapper) {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public UserDto? GetUserById(long id) {
            return dbContext.Users
                .ProjectTo<UserDto>(mapper.ConfigurationProvider)
                .FirstOrDefault(u => u.Id == id);
        }
    }
}
