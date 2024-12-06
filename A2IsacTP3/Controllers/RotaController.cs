using A2IsacTP3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2IsacTP3.DTOs;

namespace A2IsacTP3.Controllers
{
    [Route("api/rota")]
    [ApiController]
    public class RotaController : ControllerBase
    {

        private readonly AppDbContext _context;

        public RotaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Rotas
        /// <summary>
        ///     Retorna todas as rotas cadastradas no banco
        /// </summary>
        /// <remarks>Retorna todas as rotas cadastradas no banco, com suas informações e informações do avião utilizado</remarks>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rota>>> GetRotas()
        {
            return await _context.Rotas
                .Include(r => r.Aviao) // Inclui o avião relacionado
                .ToListAsync();
        }

        /// <summary>
        /// Retorna uma rota específica de acordo com seu Id.
        /// </summary>
        /// <remarks>Retorna uma rota específica, juntamente com suas informações e informações do avião utilizado</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Rotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rota>> GetRota(int id)
        {
            var rota = await _context.Rotas
                .Include(r => r.Aviao)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rota == null)
            {
                return NotFound();
            }

            return rota;
        }

        // POST: api/Rotas
        /// <summary>
        /// Criação de uma nova rota no banco de dados
        /// </summary>
        /// <remarks>Método para a criação de uma nova rota, com validações nos campos e avião ativo necessário</remarks>
        /// <param name="rotaDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Rota>> CreateRota([FromBody] RotaDto rotaDto)
        {

            var aviao = await _context.Avioes.FindAsync(rotaDto.AviaoId);
            if (!_context.Avioes.Any(a => a.Id == rotaDto.AviaoId))
            {
                return BadRequest("O ID do avião fornecido não é válido.");
            }

            var rota = new Rota
            {
                Origem = rotaDto.Origem,
                Destino = rotaDto.Destino,
                Distancia = rotaDto.Distancia,
                TempoEstimado = rotaDto.TempoEstimado,
                AviaoId = rotaDto.AviaoId,
                Aviao = aviao

            };

            _context.Rotas.Add(rota);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRota), new { id = rota.Id }, rota);
        }

        // PUT: api/Rotas/5
        /// <summary>
        /// Atualiza uma rota existente no banco de dados
        /// </summary>
        /// <remarks>Atualiza uma rota no banco de dados, podendo atualizar suas informações e o avião utilizado </remarks>
        /// <param name="id">Informe o id de uma rota para atualizá-lo</param>
        /// <param name="rotaDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRota(int id, RotaDto rotaDto)
        {
            var rota = await _context.Rotas.FindAsync(id);
            if (rota == null )
            {
                return BadRequest("O ID fornecido não corresponde ao ID da rota.");
            }
            if (!_context.Avioes.Any(a => a.Id == rota.AviaoId))
            {
                return BadRequest("O ID do avião fornecido não é válido.");
            }

            rota.TempoEstimado = rotaDto.TempoEstimado; 
            rota.Destino = rotaDto.Destino;
            rota.Distancia = rotaDto.Distancia;
            rota.AviaoId = rotaDto.AviaoId;
            rota.Origem = rotaDto.Origem;

            var aviao = await _context.Avioes.FindAsync(rotaDto.AviaoId);
            if (!_context.Avioes.Any(a => a.Id == rotaDto.AviaoId))
            {
                return BadRequest("O ID do avião fornecido não é válido.");
            }

            rota.Aviao = aviao;

            _context.Entry(rota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RotaExists(id))
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

        // DELETE: api/Rotas/5
        /// <summary>
        /// Deleta uma rota do banco de dados
        /// </summary>
        /// <param name="id">Informa o id de uma rota para exclusão</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRota(int id)
        {
            var rota = await _context.Rotas.FindAsync(id);
            if (rota == null)
            {
                return NotFound();
            }

            _context.Rotas.Remove(rota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RotaExists(int id)
        {
            return _context.Rotas.Any(r => r.Id == id);
        }
    }
}
