using AutoMapper;
using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using Bootcamp.Core.UnitOfWork;
using Bootcamp.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public class InstructorManager : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InstructorManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<InstructorResponseDto> CreateAsync(InstructorRequestDto instructorRequestDto)
        {
            // Check if email already exists
            var existingInstructor = await _unitOfWork.Instructors.GetByEmailAsync(instructorRequestDto.Email);
            if (existingInstructor != null)
                throw new Exception("Email already in use");
            
            var instructor = _mapper.Map<Instructor>(instructorRequestDto);
            
            // Password hashing would be handled here in a real application
            instructor.PasswordHash = new byte[0]; // Placeholder
            instructor.PasswordSalt = new byte[0]; // Placeholder
            
            await _unitOfWork.Instructors.AddAsync(instructor);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<InstructorResponseDto>(instructor);
        }

        public async Task DeleteAsync(int id)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(id);
            if (instructor == null)
                throw new Exception("Instructor not found");
                
            _unitOfWork.Instructors.Remove(instructor);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<InstructorResponseDto>> GetAllAsync()
        {
            var instructors = await _unitOfWork.Instructors.GetAllAsync();
            return _mapper.Map<IEnumerable<InstructorResponseDto>>(instructors);
        }

        public async Task<InstructorResponseDto> GetByIdAsync(int id)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(id);
            if (instructor == null)
                throw new Exception("Instructor not found");
                
            return _mapper.Map<InstructorResponseDto>(instructor);
        }

        public async Task<InstructorResponseDto> UpdateAsync(int id, InstructorRequestDto instructorRequestDto)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(id);
            if (instructor == null)
                throw new Exception("Instructor not found");
                
            // Check if email is being changed and if it already exists
            if (instructor.Email != instructorRequestDto.Email)
            {
                var existingInstructor = await _unitOfWork.Instructors.GetByEmailAsync(instructorRequestDto.Email);
                if (existingInstructor != null)
                    throw new Exception("Email already in use");
            }
                
            _mapper.Map(instructorRequestDto, instructor);
            
            _unitOfWork.Instructors.Update(instructor);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<InstructorResponseDto>(instructor);
        }
    }
} 