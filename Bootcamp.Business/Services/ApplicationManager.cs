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
    public class ApplicationManager : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApplicationBusinessRules _rules;

        public ApplicationManager(IUnitOfWork unitOfWork, IMapper mapper, ApplicationBusinessRules rules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<ApplicationResponseDto> CreateAsync(ApplicationRequestDto applicationRequestDto)
        {
            await _rules.CheckIfApplicantAlreadyAppliedToBootcampAsync(
                applicationRequestDto.ApplicantId, 
                applicationRequestDto.BootcampId);
                
            await _rules.CheckIfBootcampIsActiveAsync(applicationRequestDto.BootcampId);
            await _rules.CheckIfApplicantIsBlacklistedAsync(applicationRequestDto.ApplicantId);
            
            var application = _mapper.Map<Application>(applicationRequestDto);
            
            await _unitOfWork.Applications.AddAsync(application);
            await _unitOfWork.CompleteAsync();
            
            // Get the application with related details
            var createdApplication = await _unitOfWork.Applications.GetByIdAsync(application.Id);
            return _mapper.Map<ApplicationResponseDto>(createdApplication);
        }

        public async Task DeleteAsync(int id)
        {
            var application = await _unitOfWork.Applications.GetByIdAsync(id);
            if (application == null)
                throw new Exception("Application not found");
                
            _unitOfWork.Applications.Remove(application);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<ApplicationResponseDto>> GetAllAsync()
        {
            var applications = await _unitOfWork.Applications.GetAllAsync();
            return _mapper.Map<IEnumerable<ApplicationResponseDto>>(applications);
        }

        public async Task<ApplicationResponseDto> GetByIdAsync(int id)
        {
            var application = await _unitOfWork.Applications.GetByIdAsync(id);
            if (application == null)
                throw new Exception("Application not found");
                
            return _mapper.Map<ApplicationResponseDto>(application);
        }

        public async Task<ApplicationResponseDto> UpdateStatusAsync(ApplicationStatusUpdateRequestDto updateRequestDto)
        {
            await _rules.CheckIfApplicationExistsAsync(updateRequestDto.ApplicationId);
            await _rules.CheckIfStatusUpdateIsValidAsync(
                updateRequestDto.ApplicationId, 
                updateRequestDto.NewState);
                
            var application = await _unitOfWork.Applications.GetByIdAsync(updateRequestDto.ApplicationId);
            application.ApplicationState = updateRequestDto.NewState;
            
            _unitOfWork.Applications.Update(application);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<ApplicationResponseDto>(application);
        }
    }
} 