using System.Collections.Generic;

namespace SmashLeague.Services
{
    public class ManagerResult<T> where T : ManagerErrorBase
    {
        public bool Succeeded { get; protected set; }
        public IEnumerable<T> Errors { get; protected set; }
    }
}
