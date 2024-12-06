using A2IsacTP3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2IsacTP3.DTOs;

namespace A2IsacTP3.Controllers
{
    [Route("api/comissario")]
    [ApiController]
    public class ComissarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComissarioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Comissarios
        /// <summary>
        /// Retorna todos os comissários presentes no banco de dados
        /// </summary>
        /// <returns></returns>
        /// <remarks>Retorna todos os comissários cadastrados no banco, com suas informações</remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comissario>>> GetComissarios()
        {
            return await _context.Comissarios.Include(c => c.Tripulacoes).ToListAsync();
        }

        /// <summary>
        ///     Retorna um comissário específico de acordo com seu id
        /// </summary>
        /// <param name="id"> Informe o id</param>
        /// <returns></returns>
        /// <remarks>Retorna um comissário específico, com suas informações</remarks>
        // GET: api/Comissarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comissario>> GetComissario(int id)
        {
            var comissario = await _context.Comissarios
                .Include(c => c.Tripulacoes) // Inclui as tripulações relacionadas
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comissario == null)
            {
                return NotFound();
            }

            return comissario;
        }

        // POST: api/Comissarios
        /// <summary>
        ///     Criação de um novo comissário no banco de dados
        /// </summary>
        /// <param name="comissarioDto"></param>
        /// <returns></returns>
        /// <remarks>Método para criação de um novo comissário no banco de dados, a tripulação nesse método é automaticamente nula.</remarks>
        [HttpPost]
        public async Task<ActionResult<Comissario>> CreateComissario(ComissarioDto comissarioDto)
        {
            var comissario = new Comissario
            {
                Nome = comissarioDto.Nome,
                AnosExperiencia = comissarioDto.AnosExperiencia,
                Cpf = comissarioDto.Cpf,
                UltimoTreinamento = comissarioDto.UltimoTreinamento,
                DataNascimento = comissarioDto.DataNascimento,
                Tripulacoes = null
            };

            _context.Comissarios.Add(comissario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComissario), new { id = comissario.Id }, comissario);
        }

        // PUT: api/Comissarios/5
        /// <summary>
        ///     Atualiza as informações de um comissário já existente
        /// </summary>
        /// <remarks>Método para atualização de um comissário já existente</remarks>
        /// <param name="id">Informe o id do comissário para atualizar</param>
        /// <param name="comissarioDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComissario(int id, ComissarioDto comissarioDto)
        {

            var comissario = await _context.Comissarios.FindAsync(id);

            if (comissario == null)
            {
                return BadRequest("O ID fornecido não corresponde ao ID do comissário.");
            }
            
            comissario.Nome = comissarioDto.Nome;
            comissario.UltimoTreinamento = comissarioDto.UltimoTreinamento;
            comissario.DataNascimento = comissarioDto.DataNascimento; 
            comissario.Cpf = comissarioDto.Cpf;
            comissario.AnosExperiencia = comissarioDto.AnosExperiencia;

            if (comissarioDto.tripulacoes != null)
            {
                foreach (var tripulacaoId in comissarioDto.tripulacoes)
                {
                    if (_context.Tripulacoes.Any(r => r.Id == tripulacaoId))
                    {
                        comissario.Tripulacoes.Add(new Tripulacao { Id = tripulacaoId });
                    }
                    else
                    {
                        return BadRequest($"A rota com ID {tripulacaoId} não existe.");
                    }
                }
            }

            _context.Entry(comissario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComissarioExists(id))
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

        // DELETE: api/Comissarios/5
        /// <summary>
        ///     Exclui um comissário do banco de dados
        /// </summary>
        /// <remarks>Método para a exclusão do comissário</remarks>
        /// <param name="id">Informe o id do comissário para a exclusão</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComissario(int id)
        {
            var comissario = await _context.Comissarios.FindAsync(id);
            if (comissario == null)
            {
                return NotFound();
            }

            _context.Comissarios.Remove(comissario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComissarioExists(int id)
        {
            return _context.Comissarios.Any(c => c.Id == id);
        }
    }
}
