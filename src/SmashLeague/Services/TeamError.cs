using System;

namespace SmashLeague.Services
{
    public class TeamError : ManagerErrorBase
    {
        public override string Message
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void AddError(string code, string message)
        {
            throw new NotImplementedException();
        }
    }
}
