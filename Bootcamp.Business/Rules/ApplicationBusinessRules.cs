using Bootcamp.Core.Repositories;
using Bootcamp.Entities;
using System;
using System.Threading.Tasks;

namespace Bootcamp.Business.Rules
{
    public class ApplicationBusinessRules
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBootcampRepository _bootcampRepository;
        private readonly IBlacklistRepository _blacklistRepository;

        public ApplicationBusinessRules(
            IApplicationRepository applicationRepository,
            IBootcampRepository bootcampRepository,
            IBlacklistRepository blacklistRepository)
        {
            _applicationRepository = applicationRepository;
            _bootcampRepository = bootcampRepository;
            _blacklistRepository = blacklistRepository;
        }

        public async Task CheckIfApplicationExistsAsync(int applicationId)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId);
            if (application == null)
                throw new Exception("Application not found");
        }

        public async Task CheckIfApplicantAlreadyAppliedToBootcampAsync(int applicantId, int bootcampId)
        {
            var exists = await _applicationRepository.ExistsByApplicantAndBootcampAsync(applicantId, bootcampId);
            if (exists)
                throw new Exception("Applicant has already applied to this bootcamp");
        }

        public async Task CheckIfBootcampIsActiveAsync(int bootcampId)
        {
            var isActive = await _bootcampRepository.IsActiveAsync(bootcampId);
            if (!isActive)
                throw new Exception("This bootcamp is not active");
        }

        public async Task CheckIfApplicantIsBlacklistedAsync(int applicantId)
        {
            var isBlacklisted = await _blacklistRepository.ExistsByApplicantIdAsync(applicantId);
            if (isBlacklisted)
                throw new Exception("This applicant is blacklisted and cannot apply");
        }

        public async Task CheckIfStatusUpdateIsValidAsync(int applicationId, ApplicationState newState)
        {
            var canUpdate = await _applicationRepository.CanUpdateStatusAsync(applicationId, newState);
            if (!canUpdate)
                throw new Exception("Invalid status transition");
        }
    }
} 