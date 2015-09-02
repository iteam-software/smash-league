using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmashLeague.DataTransferObjects
{
    public class Team
    {

        public static implicit operator Team(Data.Team entity)
        {
            return new Team();
        }
    }
}
