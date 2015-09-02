using SmashLeague.Services;
using System;
using System.Text;

namespace SmashLeague
{
    public class ManagerException<T> : Exception where T : ManagerErrorBase
    {
        public ManagerResult<T> Result { get; private set; }

        private ManagerException(string message) : base(message) { }

        public static ManagerException<T> Create(ManagerResult<T> result)
        {
            var messageSb = new StringBuilder("An error occured");
            foreach (var error in result.Errors)
            {
                messageSb.AppendLine(error.Message);
            }

            var exception = new ManagerException<T>(messageSb.ToString());
            exception.Result = result;

            return exception;
        }
    }
}
