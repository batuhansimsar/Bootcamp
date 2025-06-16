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
    public class BlacklistManager : IBlacklistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly BlacklistBusinessRules _rules;

        public BlacklistManager(IUnitOfWork unitOfWork, IMapper mapper, BlacklistBusinessRules rules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<BlacklistResponseDto> CreateAsync(BlacklistRequestDto blacklistRequestDto)
        {
            _rules.CheckIfReasonIsValid(blacklistRequestDto.Reason);
            await _rules.CheckIfApplicantExistsAsync(blacklistRequestDto.ApplicantId);
            await _rules.CheckIfApplicantAlreadyBlacklistedAsync(blacklistRequestDto.ApplicantId);
            
            var blacklist = _mapper.Map<Blacklist>(blacklistRequestDto);
            
            await _unitOfWork.Blacklists.AddAsync(blacklist);
            await _unitOfWork.CompleteAsync();
            
            // Get the blacklist with applicant details
            var createdBlacklist = await _unitOfWork.Blacklists.GetByIdAsync(blacklist.Id);
            return _mapper.Map<BlacklistResponseDto>(createdBlacklist);
        }

        public async Task DeleteAsync(int id)
        {
            var blacklist = await _unitOfWork.Blacklists.GetByIdAsync(id);
            if (blacklist == null)
                throw new Exception("Blacklist entry not found");
                
            _unitOfWork.Blacklists.Remove(blacklist);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<BlacklistResponseDto>> GetAllAsync()
        {
            var blacklists = await _unitOfWork.Blacklists.GetAllAsync();
            return _mapper.Map<IEnumerable<BlacklistResponseDto>>(blacklists);
        }

        public async Task<BlacklistResponseDto> GetByIdAsync(int id)
        {
            var blacklist = await _unitOfWork.Blacklists.GetByIdAsync(id);
            if (blacklist == null)
                throw new Exception("Blacklist entry not found");
                
            return _mapper.Map<BlacklistResponseDto>(blacklist);
        }

        public async Task<BlacklistResponseDto> UpdateAsync(int id, BlacklistRequestDto blacklistRequestDto)
        {
            var blacklist = await _unitOfWork.Blacklists.GetByIdAsync(id);
            if (blacklist == null)
                throw new Exception("Blacklist entry not found");
                
            _rules.CheckIfReasonIsValid(blacklistRequestDto.Reason);
            
            // If applicant is being changed, check if the new applicant exists and is not already blacklisted
            if (blacklist.ApplicantId != blacklistRequestDto.ApplicantId)
            {
                await _rules.CheckIfApplicantExistsAsync(blacklistRequestDto.ApplicantId);
                await _rules.CheckIfApplicantAlreadyBlacklistedAsync(blacklistRequestDto.ApplicantId);
            }
                
            _mapper.Map(blacklistRequestDto, blacklist);
            
            _unitOfWork.Blacklists.Update(blacklist);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<BlacklistResponseDto>(blacklist);
        }

        public async Task<bool> IsApplicantBlacklistedAsync(int applicantId)
        {
            return await _unitOfWork.Blacklists.ExistsByApplicantIdAsync(applicantId);
        }
    }
} 