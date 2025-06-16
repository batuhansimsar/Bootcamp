using Bootcamp.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Bootcamp.Business.Rules
{
    public class BlacklistBusinessRules
    {
        private readonly IBlacklistRepository _blacklistRepository;
        private readonly IApplicantRepository _applicantRepository;

        public BlacklistBusinessRules(IBlacklistRepository blacklistRepository, IApplicantRepository applicantRepository)
        {
            _blacklistRepository = blacklistRepository;
            _applicantRepository = applicantRepository;
        }

        public async Task CheckIfApplicantExistsAsync(int applicantId)
        {
            var applicant = await _applicantRepository.GetByIdAsync(applicantId);
            if (applicant == null)
                throw new Exception("Applicant not found");
        }

        public async Task CheckIfApplicantAlreadyBlacklistedAsync(int applicantId)
        {
            var exists = await _blacklistRepository.ExistsByApplicantIdAsync(applicantId);
            if (exists)
                throw new Exception("This applicant is already blacklisted");
        }

        public void CheckIfReasonIsValid(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new Exception("Reason cannot be empty");
        }
    }
} 