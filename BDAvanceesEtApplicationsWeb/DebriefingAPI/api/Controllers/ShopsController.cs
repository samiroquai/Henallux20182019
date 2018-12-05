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
    public class ShopsController : Controller
    {
        private ShopDirectoryContext _context;

        public ShopsController(ShopDirectoryContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET: api/Shops/
        [HttpGet]
        public async Task<IActionResult> Get(int? pageIndex=0, int? pageSize=3,string nom=null)
        {
            IEnumerable<Model.Shop> entities = await _context.Shops
            .Where(shop=>nom==null || shop.Name.Contains(nom))
            .OrderBy(shop => shop.Id)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value*pageSize.Value)
            .ToArrayAsync();
            return Ok(entities.Select(CreateDTOFromEntity));
        }

        private static DTO.Shop CreateDTOFromEntity(Model.Shop entity)
        {
            //fixme: comment améliorer cette implémentation?
            return new DTO.Shop()
            {
                Id = entity.Id,
                Name = entity.Name,
                RowVersion = entity.RowVersion,
                OwnerId=entity.OwnerId
            };
        }

        // GET api/Shops/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Model.Shop entity = await FindShopById(id);
            if (entity == null)
                return NotFound();
            return Ok(CreateDTOFromEntity(entity));
        }

        private Task<Model.Shop> FindShopById(int id)
        {
            return _context.Shops.FindAsync(id);
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles = Constants.Roles.Admin)]
        public async Task<IActionResult> Post([FromBody]DTO.Shop dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Model.Shop entity = CreateEntityFromDTO(dto);
            _context.Shops.Add(entity);
            await _context.SaveChangesAsync();
            return Created($"api/Shops/{entity.Id}", CreateDTOFromEntity(entity));
        }

        private Model.Shop CreateEntityFromDTO(DTO.Shop dto)
        {
            var shop = new Model.Shop(dto.Name, dto.OwnerId);
            if (dto.RowVersion != null)
                shop.RowVersion = dto.RowVersion;
            return shop;
        }

        // PUT api/Shops/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]DTO.Shop dto)
        {
            //fixme: comment valider que le client envoie toujours quelque chose de valide?
            Model.Shop entity = await FindShopById(id);
            if (entity == null)
                return NotFound();
            //fixme: améliorer cette implémentation
            entity.Name = dto.Name;
            entity.OwnerId = dto.OwnerId;
            //fixme: le premier RowVersion n'a pas d'impact. 
            // Attardez-vous à comprendre pour quelle raison.
            // entity.RowVersion = dto.RowVersion;
            _context.Entry(entity).OriginalValues["RowVersion"] = dto.RowVersion;
            //pas de gestion des opening periods (voir autre controller).
            await _context.SaveChangesAsync();
            return Ok(CreateDTOFromEntity(entity));
        }

        // DELETE api/Shops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Model.Shop shop = await FindShopById(id);
            if (shop == null)
                // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
                // s'agit-il vraiment d'un cas d'erreur? 
                return NotFound();
            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
