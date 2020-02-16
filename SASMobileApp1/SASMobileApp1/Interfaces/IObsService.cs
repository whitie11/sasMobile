using System;
using System.Threading.Tasks;
using SASMobileApp1.Models;
namespace SASMobileApp1.Interfaces
{
    public interface IObsService
    {
        Task<ObservationDTO> SaveObs(ObservationDTO dto);
    }
}
