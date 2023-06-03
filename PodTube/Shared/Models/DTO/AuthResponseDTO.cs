using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodTube.Shared.Models.DTO
{
    public class AuthResponseDTO
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
