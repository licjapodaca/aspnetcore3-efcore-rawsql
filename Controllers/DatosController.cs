using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EFCore3.Dto;
using EFCore3.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EFCore3.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DatosController : ControllerBase
	{
		private readonly IConsultaRepository _service;
		private readonly IMapper _mapper;

		public DatosController(IConsultaRepository service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> ObtenerDatos()
		{
			try
			{
				return Ok(await _service.ObtenerDatos());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[HttpGet]
		[Route("dos")]
		public async Task<IActionResult> ObtenerDatosDos()
		{
			try
			{
				var resultado = await _service.ObtenerDatosDos();

				return Ok(_mapper.Map<IEnumerable<AuthorDto>>(resultado));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}