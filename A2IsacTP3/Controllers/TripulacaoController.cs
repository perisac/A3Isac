using A2IsacTP3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2IsacTP3.DTOs;

namespace A2IsacTP3.Controllers
{
    [Route("api/tripulacao")]
    [ApiController]
    public class TripulacaoController : ControllerBase
    {

        private readonly AppDbContext _context;

        public TripulacaoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tripulacoes
        /// <summary>
        /// Retorna todas as tripulações do banco de dados
        /// </summary>
        /// <remarks>Retorna todas as tripulações do banco de dados, incluindo suas informações e informações do piloto, copiloto e comissários</remarks>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tripulacao>>> GetTripulacoes()
        {
            return await _context.Tripulacoes
                .Include(t => t.Piloto)
                .Include(t => t.CoPiloto)
                .Include(t => t.Comissarios) // Carrega os comissários
                .ToListAsync();
        }

        // GET: api/Tripulacoes/5
        /// <summary>
        /// Retorna uma tripulação específica
        /// </summary>
        /// <param name="id">Informe o id de uma tripulação para visualiza-lo</param>
        /// <remarks>Retorna uma tripulação específica, com suas informações, informações do piloto, copiloto e comissários</remarks>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Tripulacao>> GetTripulacao(int id)
        {
            var tripulacao = await _context.Tripulacoes
                .Include(t => t.Piloto)
                .Include(t => t.CoPiloto)
                .Include(t => t.Comissarios)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tripulacao == null)
            {
                return NotFound();
            }

            return tripulacao;
        }

        // POST: api/Tripulacoes
        /// <summary>
        /// Cria uma tripulação no banco de dados
        /// </summary>
        /// <remarks>Método para criação de uma tripulação, é necessário que haja piloto e copiloto existentes, assim como comissários</remarks>>
        /// <param name="tripulacaoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Tripulacao>> CreateTripulacao(TripulacaoDto tripulacaoDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tripulacao = new Tripulacao();

            tripulacao.NomeTripulacao = tripulacaoDto.NomeTripulacao;

            var copiloto = await _context.Pilotos.FindAsync(tripulacaoDto.CoPilotoId);

            if (copiloto == null) {

                return BadRequest("O ID fornecido não corresponde ao ID do CoPiloto.");
            }

            tripulacao.CoPiloto = copiloto;

            var piloto = await _context.Pilotos.FindAsync(tripulacaoDto.PilotoId);

            if (piloto == null)
            {

                return BadRequest("O ID fornecido não corresponde ao ID do Piloto.");
            }

            tripulacao.Piloto = piloto;

            tripulacao.Comissarios = new List<Comissario>();

            if (tripulacaoDto.comissariosIds != null && tripulacaoDto.comissariosIds.Any())
            {
                foreach (var comissarioId in tripulacaoDto.comissariosIds)
                {
                    // Verifica se o comissário existe
                    var comissario = await _context.Comissarios.FindAsync(comissarioId);
                    if (comissario == null)
                    {
                        return BadRequest($"O comissário com ID {comissarioId} não existe.");
                    }

                    // Adiciona o comissário à tripulação
                    tripulacao.Comissarios.Add(comissario);
                }
            }

            _context.Tripulacoes.Add(tripulacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTripulacao), new { id = tripulacao.Id }, tripulacao);
        }

        // PUT: api/Tripulacoes/5
        /// <summary>
        /// Atualiza as informações de uma tripulação
        /// </summary>
        /// <remarks>Atualiza as informações de uma tripulação, atualizando seu piloto e copiloto, e também os comissários</remarks>
        /// <param name="id">Informe o id da tripulação para alterá-lo</param>
        /// <param name="tripulacao"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTripulacao(int id, Tripulacao tripulacao)
        {
            if (id != tripulacao.Id)
            {
                return BadRequest("O ID fornecido não corresponde ao ID da tripulação.");
            }

            var existingTripulacao = await _context.Tripulacoes
                .Include(t => t.Comissarios)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existingTripulacao == null)
            {
                return NotFound();
            }

            // Atualiza os campos principais
            existingTripulacao.NomeTripulacao = tripulacao.NomeTripulacao;
            existingTripulacao.PilotoId = tripulacao.PilotoId;
            existingTripulacao.CoPilotoId = tripulacao.CoPilotoId;

            // Atualiza os comissários (remove os antigos e adiciona os novos)
            existingTripulacao.Comissarios.Clear();
            foreach (var comissario in tripulacao.Comissarios)
            {
                existingTripulacao.Comissarios.Add(comissario);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripulacaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Tripulacoes/5
        /// <summary>
        /// Deleta uma tripulação do banco de dados
        /// </summary>
        /// <param name="id">Informe o id da tripulação para deletá-la</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripulacao(int id)
        {
            var tripulacao = await _context.Tripulacoes
                .Include(t => t.Comissarios)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tripulacao == null)
            {
                return NotFound();
            }

            _context.Tripulacoes.Remove(tripulacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripulacaoExists(int id)
        {
            return _context.Tripulacoes.Any(t => t.Id == id);
        }

    }
}
