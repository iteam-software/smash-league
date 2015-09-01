using System.Collections.Generic;

namespace SmashLeague.Services
{
    public class EmailResult
    {
        public bool Success { get; private set; }
        public IEnumerable<EmailError> Errors { get; private set; }

        private EmailResult(bool success, IEnumerable<EmailError> errors)
        {
            Errors = errors;
            Success = success;
        }

        public static EmailResult Failed(params EmailError[] errors)
        {
            return new EmailResult(false, errors);
        }

        public static EmailResult Default()
        {
            return new EmailResult(true, null);
        }
    }
}
