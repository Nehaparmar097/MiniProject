using Job_Portal_API.Models.DTOs;

namespace Job_Portal_API.Interfaces
{
    public interface IBloodStock
    {
        public Task<BloodStockResponseDTO> AddBloodStockAsync(BloodStockDTO BloodStockDto);
        public  Task<IEnumerable<BloodStockResponseDTO>> GetAllBloodStocksAsync();
        public Task<BloodStockResponseDTO > GetBloodStockByIdAsync(int id);
        public Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksByDonorIdAsync(int donorId);

        public Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksByLocationAsync(string location);

        public Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksBySalaryAsync(double sRange, double eRange);
        public Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksByTitleAsync(string title);
        public Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksBystatusAsync(string status);


        public Task<IEnumerable<BloodStockResponseDTO>> GetBloodStocksByJobTypeAsync(string jobType);


    }
}
