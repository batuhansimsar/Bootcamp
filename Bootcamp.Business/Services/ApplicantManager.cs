using AutoMapper;
using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using Bootcamp.Business.Rules;
using Bootcamp.Core.UnitOfWork;
using Bootcamp.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public class ApplicantManager : IApplicantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApplicantBusinessRules _rules;

        public ApplicantManager(IUnitOfWork unitOfWork, IMapper mapper, ApplicantBusinessRules rules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<ApplicantResponseDto> CreateAsync(ApplicantRequestDto applicantRequestDto)
        {
            await _rules.CheckIfNationalityIdentityExistsAsync(applicantRequestDto.NationalityIdentity);
            
            var applicant = _mapper.Map<Applicant>(applicantRequestDto);
            
            // Password hashing would be handled here in a real application
            applicant.PasswordHash = new byte[0]; // Placeholder
            applicant.PasswordSalt = new byte[0]; // Placeholder
            
            await _unitOfWork.Applicants.AddAsync(applicant);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<ApplicantResponseDto>(applicant);
        }

        public async Task DeleteAsync(int id)
        {
            var applicant = await _unitOfWork.Applicants.GetByIdAsync(id);
            if (applicant == null)
                throw new Exception("Applicant not found");
                
            _unitOfWork.Applicants.Remove(applicant);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<ApplicantResponseDto>> GetAllAsync()
        {
            var applicants = await _unitOfWork.Applicants.GetAllAsync();
            return _mapper.Map<IEnumerable<ApplicantResponseDto>>(applicants);
        }

        public async Task<ApplicantResponseDto> GetByIdAsync(int id)
        {
            var applicant = await _unitOfWork.Applicants.GetByIdAsync(id);
            if (applicant == null)
                throw new Exception("Applicant not found");
                
            return _mapper.Map<ApplicantResponseDto>(applicant);
        }

        public async Task<ApplicantResponseDto> UpdateAsync(int id, ApplicantRequestDto applicantRequestDto)
        {
            var applicant = await _unitOfWork.Applicants.GetByIdAsync(id);
            if (applicant == null)
                throw new Exception("Applicant not found");
                
            // Check if the nationality identity is being changed and if it already exists
            if (applicant.NationalityIdentity != applicantRequestDto.NationalityIdentity)
                await _rules.CheckIfNationalityIdentityExistsAsync(applicantRequestDto.NationalityIdentity);
                
            _mapper.Map(applicantRequestDto, applicant);
            
            _unitOfWork.Applicants.Update(applicant);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<ApplicantResponseDto>(applicant);
        }
    }
} 