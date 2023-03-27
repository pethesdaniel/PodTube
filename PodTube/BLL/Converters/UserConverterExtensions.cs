using PodTube.DataAccess.Entities;
using PodTube.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.BLL.Converters {
    public static class UserConverterExtensions {
        public static UserInfo ToUserInfoDto(this User user) {
            return new UserInfo {
                Id = user.Id,
                Username = user.Name,
                ProfilePic = user.ProfilePicture?.ResourceURI ?? string.Empty
            };
        }
    }
}
