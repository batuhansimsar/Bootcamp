using Bootcamp.Core.Repositories;
using Bootcamp.Core.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Bootcamp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BootcampDbContext _context;
        
        public IApplicantRepository Applicants { get; private set; }
        public IInstructorRepository Instructors { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public IBootcampRepository Bootcamps { get; private set; }
        public IApplicationRepository Applications { get; private set; }
        public IBlacklistRepository Blacklists { get; private set; }
        
        public UnitOfWork(BootcampDbContext context)
        {
            _context = context;
            
            Applicants = new ApplicantRepository(_context);
            Instructors = new InstructorRepository(_context);
            Employees = new EmployeeRepository(_context);
            Bootcamps = new BootcampRepository(_context);
            Applications = new ApplicationRepository(_context);
            Blacklists = new BlacklistRepository(_context);
        }
        
        public int Complete()
        {
            return _context.SaveChanges();
        }
        
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
} 