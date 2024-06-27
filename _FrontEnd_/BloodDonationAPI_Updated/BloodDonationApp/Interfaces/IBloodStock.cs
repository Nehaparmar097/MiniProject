using Job_Portal_API.Models.DTOs;

namespace Job_Portal_API.Interfaces
{
    public interface IBloodStock
    {
        public Task<BloodStockResponseDTO> AddBloodStockAsync(BloodStockDTO BloodStockDto);
        public  Task<IList<BloodStockResponseDTO>> GetAllBloodStocksAsync();
        public Task<BloodStockResponseDTO > GetBloodStockByIdAsync(int id);
        public Task<IList<BloodStockResponseDTO>> GetBloodStocksByDonorIdAsync(int donorId);

        public Task<IList<BloodStockResponseDTO>> GetBloodStocksByCityAsync(string city);
        public Task<IList<BloodStockResponseDTO>> GetBloodStocksByStateAsync(string state);
        public Task<IList<BloodStockResponseDTO>> GetBloodStocksByHospitalAsync(string hospitalName);

        public Task<IList<BloodStockResponseDTO>> GetBloodStocksByAvailableAsync();



    }
}
