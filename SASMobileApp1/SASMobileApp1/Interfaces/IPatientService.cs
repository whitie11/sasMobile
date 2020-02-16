using SASMobileApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SASMobileApp1.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsWard();
    }
}
