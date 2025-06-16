using AutoMapper;
using Bootcamp.Business.DTOs.Requests;
using Bootcamp.Business.DTOs.Responses;
using Bootcamp.Entities;

namespace Bootcamp.Business.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>();
            
            // Applicant mappings
            CreateMap<ApplicantRequestDto, Applicant>();
            CreateMap<Applicant, ApplicantResponseDto>();
            
            // Instructor mappings
            CreateMap<InstructorRequestDto, Instructor>();
            CreateMap<Instructor, InstructorResponseDto>();
            
            // Employee mappings
            CreateMap<EmployeeRequestDto, Employee>();
            CreateMap<Employee, EmployeeResponseDto>();
            
            // Bootcamp mappings
            CreateMap<BootcampRequestDto, BootcampEntity>()
                .ForMember(dest => dest.BootcampState, opt => opt.MapFrom(src => BootcampState.PREPARING));
                
            CreateMap<BootcampEntity, BootcampResponseDto>()
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => 
                    $"{src.Instructor.FirstName} {src.Instructor.LastName}"));
            
            // Application mappings
            CreateMap<ApplicationRequestDto, Application>()
                .ForMember(dest => dest.ApplicationState, opt => opt.MapFrom(src => ApplicationState.PENDING));
                
            CreateMap<Application, ApplicationResponseDto>()
                .ForMember(dest => dest.ApplicantName, opt => opt.MapFrom(src => 
                    $"{src.Applicant.FirstName} {src.Applicant.LastName}"))
                .ForMember(dest => dest.BootcampName, opt => opt.MapFrom(src => src.Bootcamp.Name));
            
            // Blacklist mappings
            CreateMap<BlacklistRequestDto, Blacklist>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => System.DateTime.Now));
                
            CreateMap<Blacklist, BlacklistResponseDto>()
                .ForMember(dest => dest.ApplicantName, opt => opt.MapFrom(src => 
                    $"{src.Applicant.FirstName} {src.Applicant.LastName}"));
        }
    }
} 