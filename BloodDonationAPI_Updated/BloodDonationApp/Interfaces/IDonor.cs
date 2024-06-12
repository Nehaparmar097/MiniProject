using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;

namespace Job_Portal_API.Interfaces
{
    public interface IDonor
    {
        Task<ReturnDonorDTO> AddDonorDetails(AddDonorDTO donor);
        Task<ReturnDonorDTO> UpdateAge(int id, int age);
    }
}
