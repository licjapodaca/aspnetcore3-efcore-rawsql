using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EFCore3.Context;
using EFCore3.Entities;
using EFCore3.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EFCore3.Repositories.Implementations
{
    public class ConsultaRepository : IConsultaRepository
    {
		private readonly MyDbContextReadOnly _ctxReadOnly;
		private readonly MyDbContext _ctx;

        public ConsultaRepository(
			MyDbContextReadOnly ctxReadOnly,
			MyDbContext ctx
			)
		{
			_ctxReadOnly = ctxReadOnly;
			_ctx = ctx;
		}

		public async Task<List<ConsultaTodo>> ObtenerDatos()
		{
			try
			{
				var datos = await _ctxReadOnly.ConsultaTodo.FromSqlRaw(@"
					SELECT
						a.Id AuthorId,
						a.AuthorName,
						b.Id BookId,
						b.BookName,
						b.ISBN
					FROM
						dbo.Authors a
						INNER JOIN dbo.Books b ON
							b.AuthorId = a.Id
					WHERE
						1 = 1
				").ToListAsync();

				return datos;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public async Task<List<Author>> ObtenerDatosDos()
		{
			try
			{
				var datos = await _ctx.Authors.Include(p => p.Books).ToListAsync();
				return datos;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
    }
}