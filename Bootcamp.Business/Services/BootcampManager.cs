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
    public class BootcampManager : IBootcampService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly BootcampBusinessRules _rules;

        public BootcampManager(IUnitOfWork unitOfWork, IMapper mapper, BootcampBusinessRules rules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rules = rules;
        }

        public async Task<BootcampResponseDto> CreateAsync(BootcampRequestDto bootcampRequestDto)
        {
            _rules.CheckIfStartDateBeforeEndDate(bootcampRequestDto.StartDate, bootcampRequestDto.EndDate);
            await _rules.CheckIfBootcampNameExistsAsync(bootcampRequestDto.Name);
            await _rules.CheckIfInstructorExistsAsync(bootcampRequestDto.InstructorId);
            
            var bootcamp = _mapper.Map<BootcampEntity>(bootcampRequestDto);
            
            await _unitOfWork.Bootcamps.AddAsync(bootcamp);
            await _unitOfWork.CompleteAsync();
            
            // Get the bootcamp with instructor details
            var createdBootcamp = await _unitOfWork.Bootcamps.GetByIdAsync(bootcamp.Id);
            return _mapper.Map<BootcampResponseDto>(createdBootcamp);
        }

        public async Task DeleteAsync(int id)
        {
            var bootcamp = await _unitOfWork.Bootcamps.GetByIdAsync(id);
            if (bootcamp == null)
                throw new Exception("Bootcamp not found");
                
            _unitOfWork.Bootcamps.Remove(bootcamp);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<BootcampResponseDto>> GetAllAsync()
        {
            var bootcamps = await _unitOfWork.Bootcamps.GetAllAsync();
            return _mapper.Map<IEnumerable<BootcampResponseDto>>(bootcamps);
        }

        public async Task<BootcampResponseDto> GetByIdAsync(int id)
        {
            var bootcamp = await _unitOfWork.Bootcamps.GetByIdAsync(id);
            if (bootcamp == null)
                throw new Exception("Bootcamp not found");
                
            return _mapper.Map<BootcampResponseDto>(bootcamp);
        }

        public async Task<BootcampResponseDto> UpdateAsync(int id, BootcampRequestDto bootcampRequestDto)
        {
            var bootcamp = await _unitOfWork.Bootcamps.GetByIdAsync(id);
            if (bootcamp == null)
                throw new Exception("Bootcamp not found");
                
            _rules.CheckIfStartDateBeforeEndDate(bootcampRequestDto.StartDate, bootcampRequestDto.EndDate);
            
            // Check if name is being changed and if it already exists
            if (bootcamp.Name != bootcampRequestDto.Name)
                await _rules.CheckIfBootcampNameExistsAsync(bootcampRequestDto.Name);
                
            // Check if instructor exists
            await _rules.CheckIfInstructorExistsAsync(bootcampRequestDto.InstructorId);
                
            _mapper.Map(bootcampRequestDto, bootcamp);
            
            _unitOfWork.Bootcamps.Update(bootcamp);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<BootcampResponseDto>(bootcamp);
        }

        public async Task<BootcampResponseDto> UpdateStateAsync(int id, BootcampState newState)
        {
            var bootcamp = await _unitOfWork.Bootcamps.GetByIdAsync(id);
            if (bootcamp == null)
                throw new Exception("Bootcamp not found");
                
            bootcamp.BootcampState = newState;
            
            _unitOfWork.Bootcamps.Update(bootcamp);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<BootcampResponseDto>(bootcamp);
        }
    }
} 