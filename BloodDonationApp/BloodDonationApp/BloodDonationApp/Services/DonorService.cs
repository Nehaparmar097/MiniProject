using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Models.Enums;

namespace Job_Portal_API.Services
{
    public class DonorService : IDonor
    {
        private readonly IRepository<int, Donor> _repository;
        private readonly IRepository<int, User> _UserRepo;

        public DonorService(IRepository<int,Donor> repository,IRepository<int,User> userRepo) { 
                _repository= repository;
                _UserRepo = userRepo;
        }
        public async Task<ReturnDonorDTO> AddDonorDetails(AddDonorDTO donor)
        {
            try
            {    var user = await _UserRepo.GetById(donor.UserID);
                if(user.UserType!=UserType.Donor)
                {
                    throw new UserTypeNotAllowedException();
                }
                var newDonor = new Donor
                {
                    UserID = donor.UserID,
                    HospitalName = donor.HospitalName,
                    HospitalDescription = donor.HospitalDescription,
                    HospitalLocation = donor.HospitalLocation
                };
                var result = await _repository.Add(newDonor);
                return await MapDonoroDTO(result);
            }
            catch (UserNotFoundException e)
            {

                throw new UserNotFoundException(e.Message);
            }
            catch (UserAlreadyExistException e)
            {
                throw new UserAlreadyExistException(e.Message);
            }
            catch (UserTypeNotAllowedException e)
            {
                throw new UserTypeNotAllowedException(e.Message);
            }

        }

        public async Task<ReturnDonorDTO> UpdateHospitalDescription(int id, string hospitalDescription)
        {
            try
            {
                var donor = await _repository.GetById(id);
                donor.HospitalDescription = hospitalDescription;
                var result = await _repository.Update(donor);
                return await MapDonoroDTO(result);
            }
            catch (UserNotFoundException e)
            {

                throw new UserNotFoundException("Donor Not Exist!!");
            }
        }

        public async Task<ReturnDonorDTO> UpdateHospitalLocation(int id, string hospitalLocation)
        {
            try
            {
                var donor = await _repository.GetById(id);
                donor.HospitalLocation = hospitalLocation;
                var result = await _repository.Update(donor);
                return await MapDonoroDTO(result);
            }
            catch(UserNotFoundException e)
            {
                throw new UserNotFoundException("Donor Not Exist!!");
            }
        }

        public async Task<ReturnDonorDTO> UpdateHospitalName(int id, string hospitalName)
        {
            try
            {
                var donor = await _repository.GetById(id);
                donor.HospitalName = hospitalName;
                var result = await _repository.Update(donor);
                return await MapDonoroDTO(result);
            }
            catch (UserNotFoundException e)
            {

                throw new UserNotFoundException("Donor Not Exist!!");
            }
        }
        public async Task<ReturnDonorDTO> MapDonoroDTO(Donor donor)
        {
            return new ReturnDonorDTO
            {
                DonorID = donor.DonorID,
                UserID = donor.UserID,
                HospitalName = donor.HospitalName,
                HospitalDescription = donor.HospitalDescription,
                HospitalLocation = donor.HospitalLocation
            };
        }
    }
}
