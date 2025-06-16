using Bootcamp.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Bootcamp.Business.Rules
{
    public class ApplicantBusinessRules
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IBlacklistRepository _blacklistRepository;

        public ApplicantBusinessRules(IApplicantRepository applicantRepository, IBlacklistRepository blacklistRepository)
        {
            _applicantRepository = applicantRepository;
            _blacklistRepository = blacklistRepository;
        }

        public async Task CheckIfApplicantExistsAsync(int applicantId)
        {
            var applicant = await _applicantRepository.GetByIdAsync(applicantId);
            if (applicant == null)
                throw new Exception("Applicant not found");
        }

        public async Task CheckIfNationalityIdentityExistsAsync(string nationalityIdentity)
        {
            var exists = await _applicantRepository.ExistsByNationalityIdentityAsync(nationalityIdentity);
            if (exists)
                throw new Exception("An applicant with this nationality identity already exists");
        }

        public async Task CheckIfApplicantIsBlacklistedAsync(int applicantId)
        {
            var isBlacklisted = await _blacklistRepository.ExistsByApplicantIdAsync(applicantId);
            if (isBlacklisted)
                throw new Exception("This applicant is blacklisted and cannot apply");
        }
    }
} 