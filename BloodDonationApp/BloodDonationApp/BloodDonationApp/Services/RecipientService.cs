using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_API.Services
{
    public class RecipientService : IRecipient
    {
        private readonly IRepository<int, Recipient> _jobSeekerRepo;
        private readonly IRangeRepository<int, RecipientBlood> _jobSeekerBloodRepository;

        public RecipientService(IRepository<int, Recipient> jobSeekerRepo, IRangeRepository<int, RecipientBlood> jobSeekerBloodRepository)
        {
            _jobSeekerRepo = jobSeekerRepo;
            _jobSeekerBloodRepository = jobSeekerBloodRepository;
        }

        public async Task<RecipientResponseDTO> GetResumeByRecipientId(int jobSeekerID)
        {
            try
            {
                var jobSeeker = await _jobSeekerRepo.GetById(jobSeekerID);
                return MapToDTO(jobSeeker);
            }
            catch (RecipientNotFoundException e)
            {
                throw new RecipientNotFoundException(e.Message);
            }
        }

        private RecipientResponseDTO MapToDTO(Recipient jobSeeker)
        {
            return new RecipientResponseDTO
            {
                RecipientID = jobSeeker.RecipientID,
                UserID = jobSeeker.UserID,
                bloodtype = jobSeeker.RecipientBloods.Select(skill => new RecipientBloodResponseDTO { bloodtype = skill.bloodtype }).ToList()
            };
        }

        public async Task<IEnumerable<RecipientBloodResponseDTO>> AddReciepientBloodAsync(RecipientBloodDTO jobSeekerBloodDto)
        {
            try
            {
                var jobSeeker = await _jobSeekerRepo.GetById(jobSeekerBloodDto.RecipientID);
                var jobSeekerBloods = jobSeekerBloodDto.bloodtype.Select(skillType => new RecipientBlood
                {
                    RecipientID = jobSeekerBloodDto.RecipientID,
                    bloodtype = skillType
                });
                var addedRecipientBloods = await _jobSeekerBloodRepository.AddRange(jobSeekerBloods);

                return addedRecipientBloods.Select(skill => new RecipientBloodResponseDTO
                {
                    bloodtype = skill.bloodtype
                });
            }
            catch (RecipientNotFoundException e)
            {
                throw new RecipientNotFoundException(e.Message);
            }
        }
    }
}
