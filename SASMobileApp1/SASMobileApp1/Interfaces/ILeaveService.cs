using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SASMobileApp1.Models;

namespace SASMobileApp1.Interfaces
{
    public interface ILeaveService
    {
        Task<IList<LeaveType>> GetLeaveTypes();
    }
}
