using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SASMobileApp1.Interfaces
{
   public interface IAuthenticationService
    {

        bool Login(string username, string password);

        Task<bool> LoginAsync(string username, string password, string selectedWard);


        void Logout();
    }
}
