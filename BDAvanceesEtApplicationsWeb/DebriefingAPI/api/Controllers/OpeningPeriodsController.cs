using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDDemo.Dal;
using DDDDemo.DTO;
using DDDDemo.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DDDDemo.api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class OpeningPeriodsController : Controller
    {
        private readonly ShopDirectoryContext _context;

        public OpeningPeriodsController(ShopDirectoryContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET: api/OpeningPeriod
        [HttpGet]
        public IActionResult Get()
        {
            //todo: Débat: Est-ce que cette méthode a un sens
            IEnumerable<DTO.OpeningPeriod> dtos = _context.Shops.SelectMany(sm => sm.OpeningPeriods).Select(CreateDTOFromEntity);
            return Ok(dtos);
        }

        // GET api/OpeningPeriod/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Model.OpeningPeriod entity = await FindOpeningPeriodById(id);
            return (entity == null) ? NotFound() : (IActionResult)Ok(CreateDTOFromEntity(entity));

        }

        private Task<Model.OpeningPeriod> FindOpeningPeriodById(int id)
        {
            //fixme: Comment faire?
            throw new NotImplementedException();
        }

        // GET api/OpeningPeriod/Shop/5
        [HttpGet("Shop/{shopId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DTO.OpeningPeriod>))]
        public async Task<IActionResult> GetByShop(int shopId)
        {
            Model.Shop shopFound = await FindShopById(shopId);
            if (shopFound == null)
                return NotFound();

            IEnumerable<DTO.OpeningPeriod> dtos = shopFound.OpeningPeriods.Select(CreateDTOFromEntity);
            return Ok(dtos);
        }

        private async Task<Model.Shop> FindShopById(int shopId)
        {
            return await _context.Shops.Include(s => s.OpeningPeriods).FirstOrDefaultAsync(s => s.Id == shopId);
        }

        private static DTO.OpeningPeriod CreateDTOFromEntity(Model.OpeningPeriod op)
        {
            //fixme: Possibilité d'amélioration ?
            return new DTO.OpeningPeriod()
            {
                Id = op.Id,
                Opening = op.Opening,
                Closing = op.Closing,
                Day = op.Day,
                // on ne le stocke pas en DB car calculable, mais on facilite la vie
                // des applications clientes en le proposant dans le DTO!
                DurationOfOpening = op.Closing.Subtract(op.Opening)
            };
        }

        // POST api/OpeningPeriod
        [HttpPost("Shop/{shopId}")]
        public async Task<IActionResult> Post(int shopId, [FromBody]DTO.OpeningPeriod dto)
        {
            Model.Shop shop = await FindShopById(shopId);
            if (shop == null)
                return NotFound();
            int userId = int.Parse(User.Claims.First(c => c.Type == PrivateClaims.UserId).Value);

            #region ...
            if (!User.IsInRole(Constants.Roles.Admin) && shop.OwnerId!=userId)
                return Forbid();
            #endregion

            Model.OpeningPeriod entity = CreateEntityFromDTO(dto);
            shop.AddOpeningPeriod(entity);
            await _context.SaveChangesAsync();
            return Created($"api/{entity.Id}", CreateDTOFromEntity(entity));
        }

        private Model.OpeningPeriod CreateEntityFromDTO(DTO.OpeningPeriod dto)
        {
            return new Model.OpeningPeriod(dto.Opening, dto.Closing, dto.Day);
        }

        // PUT api/OpeningPeriod/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]DTO.OpeningPeriod dto)
        {
            Model.OpeningPeriod entity = await FindOpeningPeriodById(id);
            if (entity == null)
                return NotFound();
            //fixme: comment améliorer cette implémentation?
            entity.Closing = dto.Closing;
            entity.Day = dto.Day;
            entity.Opening = dto.Opening;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/OpeningPeriod/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Model.OpeningPeriod entity = await FindOpeningPeriodById(id);
            if (entity == null)
                // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
                // s'agit-il vraiment d'un cas d'erreur? 
                return NotFound();
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
