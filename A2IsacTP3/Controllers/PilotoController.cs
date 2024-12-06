using A2IsacTP3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2IsacTP3.DTOs;

namespace A2IsacTP3.Controllers
{
    [Route("api/piloto")]
    [ApiController]
    public class PilotoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public PilotoController(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Obterá todos os pilotos cadastrados.
        /// </summary>
        /// <remarks>Get all de todos os pilotos</remarks>
        // GET: api/Pilotos
        [HttpGet]
        public async Task<IActionResult> GetPilotos()
        {

            var piloto = await _context.Pilotos.ToListAsync();

            return Ok(piloto);
        }
        /// <summary>
        /// Obterá o piloto de acordo com seu Id.
        /// </summary>
        /// <response code="200"> Retorna o piloto caso seu Id exista</response>
        /// <response code="400"> Caso tenha algum erro </response>
        /// <param name="id">Informe o Id do Piloto</param>
        /// <remarks>Um get by id com informações de um piloto específico</remarks>
        // GET: api/Pilotos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPiloto(int id)
        {
            var piloto = await _context.Pilotos.FindAsync(id);

            if (piloto == null)
            {
                return NotFound();
            }

            return Ok(piloto);
        }

        /// <summary>
        ///     Criação de um novo piloto
        /// </summary>
        /// <param name="pilotoDto"></param>
        /// <response code="200">Retornará um piloto criado</response>
        /// <response code="400">Retornará um erro </response>
        /// <remarks>Um método post para criação de um novo piloto, com validações nos campos</remarks>
        // POST: api/Pilotos
        [HttpPost]
        public async Task<IActionResult> CreatePiloto([FromBody] PilotoDto pilotoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var piloto = new Piloto
            {
                Cpf = pilotoDto.Cpf,
                DataNascimento = pilotoDto.DataNascimento,
                HorasVoo = pilotoDto.HorasVoo,
                TempoExperiencia = pilotoDto.TempoExperiencia,
                TipoLicenca = pilotoDto.TipoLicenca,
                UltimaAvaliacaoMedica = pilotoDto.UltimaAvaliacaoMedica,
                Nome = pilotoDto.Nome,
                UltimoTreinamento = pilotoDto.UltimoTreinamento

            };

            _context.Pilotos.Add(piloto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPiloto), new { id = piloto.Id }, piloto);
        }

        /// <summary>
        ///     Atualiza as informações de um piloto já existente.
        /// </summary>
        /// <param name="id">Informe o Id do piloto para atualização</param>
        /// <param name="pilotoDto"></param>
        /// <response code="200">Piloto atualizado</response>
        /// <response code="400"> Erro no update</response>
        /// <remarks>Método para a atualização de um piloto existente no banco de dados, com validações nos campos</remarks>
        // PUT: api/Pilotos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePiloto(int id, [FromBody] PilotoDto pilotoDto)
        {

            var piloto = await _context.Pilotos.FindAsync(id);

            if (piloto == null)
            {
                return BadRequest("ID do Piloto não corresponde ao ID fornecido.");

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            piloto.Nome = pilotoDto.Nome;
            piloto.DataNascimento = pilotoDto.DataNascimento;
            piloto.Cpf = pilotoDto.Cpf;
            piloto.TipoLicenca = pilotoDto.TipoLicenca;
            piloto.TempoExperiencia = pilotoDto.TempoExperiencia;
            piloto.HorasVoo = pilotoDto.HorasVoo;
            piloto.UltimaAvaliacaoMedica = pilotoDto.UltimaAvaliacaoMedica;
            piloto.UltimoTreinamento = pilotoDto.UltimoTreinamento;

            _context.Entry(piloto).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        ///     Deleta um piloto do banco de dados
        /// </summary>
        /// <param name="id">Informe o id do piloto para exclusão</param>
        /// <remarks>Método para a exclusão de um piloto do banco de dados</remarks>
        // DELETE: api/Pilotos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePiloto(int id)
        {
            var piloto = await _context.Pilotos.FindAsync(id);
            if (piloto == null)
            {
                return NotFound();
            }

            _context.Pilotos.Remove(piloto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}