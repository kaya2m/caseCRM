using caseCRM.DataAccess.Common;
using caseCRM.Entities.DTOs;
using caseCRM.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace caseCRM.Service.Interfaces
{
    public interface IUserService 
    {
        Task<ResponseDto<EmptyDto>> CreateUserAsync(User user, string password);
        Task<ResponseDto<string>> AuthenticateAsync(string email, string password);
    }
}
