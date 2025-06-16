using AutoMapper;
using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using Bootcamp.Core.Security;
using Bootcamp.Core.UnitOfWork;
using Bootcamp.Entities;
using System;
using System.Threading.Tasks;

namespace Bootcamp.Business.Services
{
    public class AuthManager : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtHelper _jwtHelper;

        public AuthManager(IUnitOfWork unitOfWork, IMapper mapper, JwtHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            // Check if user exists with the provided email
            var applicant = await _unitOfWork.Applicants.GetByEmailAsync(loginRequestDto.Email);
            if (applicant != null)
            {
                // Verify password
                if (!HashingHelper.VerifyPasswordHash(loginRequestDto.Password, applicant.PasswordHash, applicant.PasswordSalt))
                    throw new Exception("Invalid email or password");
                
                // Generate JWT token
                var token = _jwtHelper.CreateToken(applicant);
                
                return new LoginResponseDto
                {
                    Id = applicant.Id,
                    FirstName = applicant.FirstName,
                    LastName = applicant.LastName,
                    Email = applicant.Email,
                    Token = token,
                    UserType = "Applicant"
                };
            }
            
            var instructor = await _unitOfWork.Instructors.GetByEmailAsync(loginRequestDto.Email);
            if (instructor != null)
            {
                // Verify password
                if (!HashingHelper.VerifyPasswordHash(loginRequestDto.Password, instructor.PasswordHash, instructor.PasswordSalt))
                    throw new Exception("Invalid email or password");
                
                // Generate JWT token
                var token = _jwtHelper.CreateToken(instructor);
                
                return new LoginResponseDto
                {
                    Id = instructor.Id,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    Email = instructor.Email,
                    Token = token,
                    UserType = "Instructor"
                };
            }
            
            var employee = await _unitOfWork.Employees.GetByEmailAsync(loginRequestDto.Email);
            if (employee != null)
            {
                // Verify password
                if (!HashingHelper.VerifyPasswordHash(loginRequestDto.Password, employee.PasswordHash, employee.PasswordSalt))
                    throw new Exception("Invalid email or password");
                
                // Generate JWT token
                var token = _jwtHelper.CreateToken(employee);
                
                return new LoginResponseDto
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Token = token,
                    UserType = "Employee"
                };
            }
            
            throw new Exception("Invalid email or password");
        }

        public async Task<ApplicantResponseDto> RegisterApplicantAsync(ApplicantRequestDto applicantRequestDto)
        {
            // Check if email already exists
            var existingApplicant = await _unitOfWork.Applicants.GetByEmailAsync(applicantRequestDto.Email);
            if (existingApplicant != null)
                throw new Exception("Email already in use");
            
            var applicant = _mapper.Map<Applicant>(applicantRequestDto);
            
            // Hash the password
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(applicantRequestDto.Password, out passwordHash, out passwordSalt);
            applicant.PasswordHash = passwordHash;
            applicant.PasswordSalt = passwordSalt;
            applicant.UserType = "Applicant";
            
            await _unitOfWork.Applicants.AddAsync(applicant);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<ApplicantResponseDto>(applicant);
        }

        public async Task<EmployeeResponseDto> RegisterEmployeeAsync(EmployeeRequestDto employeeRequestDto)
        {
            // Check if email already exists
            var existingEmployee = await _unitOfWork.Employees.GetByEmailAsync(employeeRequestDto.Email);
            if (existingEmployee != null)
                throw new Exception("Email already in use");
            
            var employee = _mapper.Map<Employee>(employeeRequestDto);
            
            // Hash the password
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(employeeRequestDto.Password, out passwordHash, out passwordSalt);
            employee.PasswordHash = passwordHash;
            employee.PasswordSalt = passwordSalt;
            employee.UserType = "Employee";
            
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<EmployeeResponseDto>(employee);
        }

        public async Task<InstructorResponseDto> RegisterInstructorAsync(InstructorRequestDto instructorRequestDto)
        {
            // Check if email already exists
            var existingInstructor = await _unitOfWork.Instructors.GetByEmailAsync(instructorRequestDto.Email);
            if (existingInstructor != null)
                throw new Exception("Email already in use");
            
            var instructor = _mapper.Map<Instructor>(instructorRequestDto);
            
            // Hash the password
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(instructorRequestDto.Password, out passwordHash, out passwordSalt);
            instructor.PasswordHash = passwordHash;
            instructor.PasswordSalt = passwordSalt;
            instructor.UserType = "Instructor";
            
            await _unitOfWork.Instructors.AddAsync(instructor);
            await _unitOfWork.CompleteAsync();
            
            return _mapper.Map<InstructorResponseDto>(instructor);
        }
    }
} 