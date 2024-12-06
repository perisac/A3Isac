using A2IsacTP3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A2IsacTP3.DTOs;

namespace A2IsacTP3.Controllers
{
    [Route("api/aviao")]
    [ApiController]
    public class AviaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AviaoController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  Retorna todos os aviões cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        /// <remarks>Retorna as especificações de todos os aviões, como tripulação, caso tenham, e as especificações dos aviões</remarks>
        // GET: api/Avioes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aviao>>> GetAvioes()
        {
            return await _context.Avioes
                .ToListAsync();
        }

        /// <summary>
        /// Retorna um avião específico do banco de dados a partir do seu id
        /// </summary>
        /// <param name="id">Informe o id do avião para busca</param>
        /// <remarks>Retorna as especificações de um avião, tal como as especificações da sua tripulação(caso tenha), e do avião em si</remarks>
        // GET: api/Avioes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aviao>> GetAviao(int id)
        {
            var aviao = await _context.Avioes
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aviao == null)
            {
                return NotFound();
            }

            return aviao;
        }

        /// <summary>
        ///     Criação um novo avião no banco de dados
        /// </summary>
        /// <param name="aviaoDTO"></param>
        /// <remarks>Criação de um novo avião no banco de dados. Caso não tenha nenhuma tripulação ou nenhuma rota para adicionar, pode retirar os campos, pois podem ser nulos</remarks>
        /// <returns></returns>
        // POST: api/Avioes
        [HttpPost]
        public async Task<ActionResult<Aviao>> CreateAviao(AviaoDto aviaoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Validação do modelo
            }

            // Verifica se a tripulação é válida
            if (aviaoDTO.TripulacaoId.HasValue && !_context.Tripulacoes.Any(t => t.Id == aviaoDTO.TripulacaoId.Value))
            {
                return BadRequest("O ID da tripulação fornecido não é válido.");
            }

            // Criação do avião
            var aviao = new Aviao
            {
                Modelo = aviaoDTO.Modelo,
                Autonomia = aviaoDTO.Autonomia,
                AnoFabricacao = aviaoDTO.AnoFabricacao,
                Capacidade = aviaoDTO.Capacidade,
                VelocidadeMedia = aviaoDTO.VelocidadeMedia,
                HorasVoo = aviaoDTO.HorasVoo,
                UltimaManutencao = aviaoDTO.UltimaManutencao,
                TripulacaoId = aviaoDTO.TripulacaoId ?? 0, // Defina um valor padrão
            };

            var tripulacao = await _context.Tripulacoes.FindAsync(aviaoDTO.TripulacaoId);

            if (tripulacao == null)
            {
                aviao.Tripulacao = null;
            }
            else
                aviao.Tripulacao = tripulacao;

            // Atribuindo rotas ao avião
            if (aviaoDTO.RotasId != null)
            {
                foreach (var rotaId in aviaoDTO.RotasId)
                {
                    if (_context.Rotas.Any(r => r.Id == rotaId))
                    {
                        aviao.Rotas.Add(new Rota { Id = rotaId });
                    }
                    else
                    {
                        return BadRequest($"A rota com ID {rotaId} não existe.");
                    }
                }
            }

            _context.Avioes.Add(aviao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAviao), new { id = aviao.Id }, aviao);
        }

        /// <summary>
        ///     Atualiza as informações de um avião específico
        /// </summary>
        /// <param name="id">Informe o id do avião para atualização</param>
        /// <param name="aviaoDto"></param>
        /// <remarks> Atualiza as informações de um avião de acordo com seu Id, nesse método, atualizar os dados de tripulação e rotas são obrigatórios</remarks>
        /// <returns></returns>
        // PUT: api/Avioes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAviao(int id, AviaoDto aviaoDto)
        {
            // Verifica se o avião existe
            var aviao = await _context.Avioes.FindAsync(id);
            if (aviao == null)
            {
                return NotFound("Avião não encontrado.");
            }

            // Verifica se a tripulação é válida
            if (aviaoDto.TripulacaoId <= 0)
            {
                return BadRequest("A tripulação é obrigatória.");
            }

            // Atualiza os dados do avião
            aviao.Modelo = aviaoDto.Modelo;
            aviao.Autonomia = aviaoDto.Autonomia;
            aviao.AnoFabricacao = aviaoDto.AnoFabricacao;
            aviao.Capacidade = aviaoDto.Capacidade;
            aviao.VelocidadeMedia = aviaoDto.VelocidadeMedia;
            aviao.HorasVoo = aviaoDto.HorasVoo;
            aviao.UltimaManutencao = aviaoDto.UltimaManutencao;
            aviao.TripulacaoId = aviaoDto.TripulacaoId ?? 0;

            // Atualiza as rotas
            aviao.Rotas = aviaoDto.RotasId
                .Select(idRota => _context.Rotas.Find(idRota))
                .Where(r => r != null)
                .ToList();

            // Marca a entidade como modificada para atualização
            _context.Entry(aviao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AviaoExists(id))
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

        /// <summary>
        /// Deleta um avião do banco de dados
        /// </summary>
        /// <param name="id">Informe o id do avião para exclusão do banco de dados</param>
        /// <returns></returns>
        // DELETE: api/Avioes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAviao(int id)
        {
            var aviao = await _context.Avioes.FindAsync(id);
            if (aviao == null)
            {
                return NotFound();
            }

            _context.Avioes.Remove(aviao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AviaoExists(int id)
        {
            return _context.Avioes.Any(a => a.Id == id);
        }
    }
}
