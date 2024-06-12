using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_API.Services
{
    public class BloodStockService : IBloodStock
    {
        private readonly IRepository<int, BloodStock> _bloodStockRepository;
        public BloodStockService(IRepository<int, BloodStock> bloodStockRepository) {
            _bloodStockRepository=bloodStockRepository;
        }
        public async Task<BloodStockResponseDTO> AddBloodStockAsync(BloodStockDTO BloodStockDto)
        {
            BloodStock stock = new BloodStock()
            { 
                BloodType=BloodStockDto.BloodType,
                status="available",
                city=BloodStockDto.city,
                state=BloodStockDto.state,
                hospitalName=BloodStockDto.hospitalName,
                donationDate=DateTime.Now,
                DonorID=BloodStockDto.DonorID
            };
            stock= await _bloodStockRepository.Add(stock);
            BloodStockResponseDTO response = new BloodStockResponseDTO()
            {
                ID=stock.ID,
                BloodType = stock.BloodType,
                status=stock.status
            };
            return response;
        }

        public async Task<IList<BloodStockResponseDTO>> GetAllBloodStocksAsync()
        {
            IList<BloodStockResponseDTO> responseDTOs = new List<BloodStockResponseDTO>();
            IEnumerable<BloodStock> bloodStocks = await _bloodStockRepository.GetAll();
            foreach (var item in bloodStocks)
            {
                BloodStockResponseDTO response = new BloodStockResponseDTO()
                {
                    ID = item.ID,
                    BloodType = item.BloodType,
                    status = item.status
                };
                responseDTOs.Add(response);
            }
            return responseDTOs;
        }

        public async Task<BloodStockResponseDTO> GetBloodStockByIdAsync(int id)
        {
            BloodStock bloodStock=await _bloodStockRepository.GetById(id);
            if (bloodStock == null)
            {
                throw new BloodStockNotFoundException();
            }
            BloodStockResponseDTO response = new BloodStockResponseDTO()
            {
                ID = bloodStock.ID,
                BloodType = bloodStock.BloodType,
                status = bloodStock.status
            };
            return response;
        }

        public async Task<IList<BloodStockResponseDTO>> GetBloodStocksByCityAsync(string city)
        {
            IList<BloodStockResponseDTO> responseDTOs = new List<BloodStockResponseDTO>();
            IList<BloodStock> bloodStocks =  _bloodStockRepository.GetAll().Result.ToList();
            foreach (var item in bloodStocks)
            {
                if (item.city == city)
                {
                    BloodStockResponseDTO response = new BloodStockResponseDTO()
                    {
                        ID = item.ID,
                        BloodType = item.BloodType,
                        status = item.status
                    };
                    responseDTOs.Add(response);
                }
            }
            return responseDTOs;
        }

        public async Task<IList<BloodStockResponseDTO>> GetBloodStocksByDonorIdAsync(int donorId)
        {
            IList<BloodStockResponseDTO> responseDTOs = new List<BloodStockResponseDTO>();
            IList<BloodStock> bloodStocks = _bloodStockRepository.GetAll().Result.ToList();
            foreach (var item in bloodStocks)
            {
                if (item.DonorID == donorId)
                {
                    BloodStockResponseDTO response = new BloodStockResponseDTO()
                    {
                        ID = item.ID,
                        BloodType = item.BloodType,
                        status = item.status
                    };
                    responseDTOs.Add(response);
                }
            }
            return responseDTOs;
        }

        public async Task<IList<BloodStockResponseDTO>> GetBloodStocksByHospitalAsync(string hospitalName)
        {
            IList<BloodStockResponseDTO> responseDTOs = new List<BloodStockResponseDTO>();
            IList<BloodStock> bloodStocks = _bloodStockRepository.GetAll().Result.ToList();
            foreach (var item in bloodStocks)
            {
                if (item.hospitalName == hospitalName)
                {
                    BloodStockResponseDTO response = new BloodStockResponseDTO()
                    {
                        ID = item.ID,
                        BloodType = item.BloodType,
                        status = item.status
                    };
                    responseDTOs.Add(response);
                }
            }
            return responseDTOs;
        }

        public async Task<IList<BloodStockResponseDTO>> GetBloodStocksByStateAsync(string state)
        {
            IList<BloodStockResponseDTO> responseDTOs = new List<BloodStockResponseDTO>();
            IList<BloodStock> bloodStocks = _bloodStockRepository.GetAll().Result.ToList();
            foreach (var item in bloodStocks)
            {
                if (item.state == state)
                {
                    BloodStockResponseDTO response = new BloodStockResponseDTO()
                    {
                        ID = item.ID,
                        BloodType = item.BloodType,
                        status = item.status
                    };
                    responseDTOs.Add(response);
                }
            }
            return responseDTOs;
        }

        public async  Task<IList<BloodStockResponseDTO>> GetBloodStocksByAvailableAsync()
        {
            IList<BloodStockResponseDTO> responseDTOs = new List<BloodStockResponseDTO>();
            IList<BloodStock> bloodStocks = _bloodStockRepository.GetAll().Result.ToList();
            foreach (var item in bloodStocks)
            {
                if (item.status == "available")
                {
                    BloodStockResponseDTO response = new BloodStockResponseDTO()
                    {
                        ID = item.ID,
                        BloodType = item.BloodType,
                        status = item.status
                    };
                    responseDTOs.Add(response);
                }
            }
            return responseDTOs;
        }
    }
}
