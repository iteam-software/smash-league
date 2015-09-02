using System;

namespace SmashLeague.Services
{
    public class TeamError : ManagerErrorBase
    {
        private string _message;
        private string _code;

        public TeamError(Exception e)
        {
            AddError(nameof(e), e.Message);
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }

        public override void AddError(string code, string message)
        {
            _code = code;
            _message = message;
        }
    }
}
