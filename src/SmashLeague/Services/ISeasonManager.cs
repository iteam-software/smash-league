using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmashLeague.Data;

namespace SmashLeague.Services
{
    public interface ISeasonManager
    {
        Task<Season> GetCurrentSeasonAsync();
    }
}
