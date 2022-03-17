using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniStore.Dto;
using MiniStore.Models;
using MiniStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Controllers
{

    [EnableCors("AllowOrigin")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, IMapper mapper, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation("Categories controller Invoked ...");
        }


        /// <summary>
        /// Retourne toute la liste des categories
        /// </summary>
        /// <returns>La liste des categories</returns>
        /// <response code="200">Liste des catégories</response>
        /// <response code="404">la liste introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si la liste est vide</exception>
        // GET: api/Category/categories
        [ProducesResponseType(typeof(IEnumerable<Category>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpGet("categories")]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                _logger.LogInformation("Categories api Invoked (pour obtenir la liste des catégories) ...");
                var categories = _categoryService.GetAllCategories();
                return StatusCode(200, categories);
            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return new NotFoundResult();
            }

        }



        /// <summary>
        /// Retourne une catégorie selon l'id donné
        /// </summary>
        /// <returns>La catégorie demandée</returns>
        /// <response code="200">Catégorie sélectionné</response>
        /// <response code="404">la catégorie est introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si la catégorie n'existe pas</exception>
        // GET: api/Category/categories/{5}
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpGet("categories/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCatgoryByID(int id)
        {
            try
            {
                _logger.LogInformation("Categories/id api Invoked (pour obtenir la catégorie demandée) ...");
                var categorie = await _categoryService.GetCatgoryByID(id);
                var categorieDto = _mapper.Map<CategoryDto>(categorie);
                if(categorieDto == null)
                {
                    _logger.LogWarning("la categorie n'existe pas!!");
                    return new NotFoundResult();
                    
                }
                return StatusCode(200, categorieDto);
            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return  new NotFoundResult();
            }

        }





        /// <summary>
        /// Ajouter une catégorie
        /// </summary>
        /// <returns>La catégorie ajoutée</returns>
        /// <response code="201">Catégorie Ajoutée avec succès</response>
        /// <response code="400">la catégorie est nulle</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si la catégorie est nulle ou l'un de ces champs null</exception>
        // POST: api/Category/add
        [ProducesResponseType(typeof(CategoryDto), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpPost("add")]
        public async Task<ActionResult<CategoryDto>> AddCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                _logger.LogInformation("Categorie/Add api Invoked (pour ajouter une nouvelle catégorie) ...");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Enter a valid category name!!");
                    return BadRequest("Enter a valid category name");
                }
                var category = _mapper.Map<Category>(categoryDto);
                var categoryReturne = await _categoryService.AddCategory(category);
                return StatusCode(201, categoryReturne);
            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return BadRequest(e);
            }

        }



        /// <summary>
        /// Retourne le status de l'action de catégorie à supprimer
        /// </summary>
        /// <returns>boolean</returns>
        /// <response code="202">Catégorie suprimmée avec succès</response>
        /// <response code="400">la catégorie n'existe pas</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si la catégorie n'existe pas</exception>
        // GET: api/Category/categories/{5}
        [ProducesResponseType(typeof(CategoryDto), 202)]
        [ProducesResponseType(typeof(NotFoundResult), 400)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpDelete("categories/{id}")]
        public async Task<ActionResult<CategoryDto>> DeleteCategory(int id)
        {
            try
            {
                _logger.LogInformation("Categories/id api Invoked (pour supprimer la catégorie souhaitée) ...");
                var categorieStatus = await _categoryService.DeleteCategory(id);
                if(categorieStatus == false)
                {
                    _logger.LogWarning("Cette categorie n'existe plus !!");
                    return BadRequest("Cette categorie n'existe plus !!");
                }
                    return StatusCode(202, categorieStatus);

            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return BadRequest(e);
            }

        }



        /// <summary>
        /// Retourne la catégorie synchronisée
        /// </summary>
        /// <returns>Category</returns>
        /// <response code="204">Catégorie modifiée avec succès</response>
        /// <response code="404">la catégorie n'existe pas</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si la catégorie n'existe pas</exception>
        // GET: api/Category/categories/{5}
        [ProducesResponseType(typeof(CategoryDto), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpPut("categories/{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id , [FromBody] CategoryDto categoryDto)
        {
            try
            {
                _logger.LogInformation("Categories/id api Invoked (pour modifier  la catégorie souhaitée) ...");
                var category = _mapper.Map<Category>(categoryDto);
                var categoryUpdated = await _categoryService.UpdateCategory(id,category);
                return StatusCode(204, categoryUpdated);

            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return new NotFoundResult();
            }

        }

    }
}
