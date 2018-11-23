using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly DataAccess _dal;
        public StudentsController(IMapper mapper, DataAccess dal)
        {
            _mapper = mapper;
            _dal = dal;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTO.Student>>> Get()
        {
            IEnumerable<Model.Student> etudiants = await _dal.ListerTousLesEtudiantsEtLeursInscriptionsAsync();
            IEnumerable<DTO.Student> dtos = etudiants.Select(_mapper.Map<DTO.Student>);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DTO.Student>>> Get(int id)
        {
            Model.Student etudiant = await _dal.EtudiantParIdAsync(id);
            if (etudiant == null)
                return NotFound();
            return Ok(_mapper.Map<DTO.Student>(etudiant));
        }

        // POST api/values
        [HttpPost]
        public ActionResult<DTO.Student> Post([FromBody] DTO.Student etudiant)
        {
            Model.Student entity = _mapper.Map<Model.Student>(etudiant);
            entity = _dal.AjouterEtudiant(entity);
            return Created("api/Students/" + entity.Id, _mapper.Map<DTO.Student>(entity));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<DTO.Student> Put(int id, [FromBody] DTO.Student etudiant)
        {
            //TODO: rowversion!!!
            Model.Student entity = _mapper.Map<Model.Student>(etudiant);
            if(entity==null)
                return NotFound();
            entity = _dal.ModifierEtudiant(entity);
            return Ok(_mapper.Map<DTO.Student>(entity));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Que faire? Est-ce que supprimer un étudiant inexistant est à considérer comme une erreur?
            // Est-ce que le fait que l'étudiant n'existe pas satisfait l'action delete?
            // Quelqu'un l'a peut-être supprimé juste avant l'utilisateur à l'origine de la deuxième tentative
            // de suppression... 
            await _dal.SupprimerEtudiantAsync(id);
            return Ok();
        }
    }
}
