using Bootcamp.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Bootcamp.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicantRepository Applicants { get; }
        IInstructorRepository Instructors { get; }
        IEmployeeRepository Employees { get; }
        IBootcampRepository Bootcamps { get; }
        IApplicationRepository Applications { get; }
        IBlacklistRepository Blacklists { get; }
        
        int Complete();
        Task<int> CompleteAsync();
    }
} 