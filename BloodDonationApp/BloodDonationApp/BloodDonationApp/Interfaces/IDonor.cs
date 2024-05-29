using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;

namespace Job_Portal_API.Interfaces
{
    public interface IDonor
    {
        public Task<ReturnDonorDTO> AddDonorDetails(AddDonorDTO donor);
        public Task<ReturnDonorDTO> UpdateHospitalName(int id, String hospitalName);
        public Task<ReturnDonorDTO> UpdateHospitalDescription(int id, String hospitalDescription);

        public Task<ReturnDonorDTO> UpdateHospitalLocation(int id, String hospitalLocation);
    }
}
