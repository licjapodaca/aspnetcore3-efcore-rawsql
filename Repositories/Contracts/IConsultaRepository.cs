using System.Collections.Generic;
using System.Threading.Tasks;
using EFCore3.Entities;

namespace EFCore3.Repositories.Contracts
{
    public interface IConsultaRepository
    {
        Task<List<ConsultaTodo>> ObtenerDatos();
		Task<List<Author>> ObtenerDatosDos();
    }
}