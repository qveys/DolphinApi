using DolphinApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinApp.DataAccess
{
    public interface IAccessApi
    {

        Task<IEnumerable<Division>> GetListDivisionAsync();

        Task<IEnumerable<Piscine>> GetListPiscineAsync();

        Task<IEnumerable<Match>> GetListAllMatchsAsync();

        Task DeleteMatchAsync(int idMatch);


    }
}
