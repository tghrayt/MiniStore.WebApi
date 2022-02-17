﻿using AutoMapper;
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
    [ApiController]
    [Route("api/[controller]")]
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
        /// <exception>Déclanche une exception d'application si la liste est vide</exception>
        // GET: api/Category/categories
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
                return BadRequest(e);
            }

        }



        /// <summary>
        /// Retourne une catégorie selon l'id donné
        /// </summary>
        /// <returns>La catégorie demandée</returns>
        /// <exception>Déclanche une exception d'application si la liste est vide</exception>
        // GET: api/Category/categories/{5}
        [HttpGet("categories/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCatgoryByID(int id)
        {
            try
            {
                _logger.LogInformation("Categories/id api Invoked (pour obtenir la catégorie demandée) ...");
                var categorie = await _categoryService.GetCatgoryByID(id);
                var categorieDto = _mapper.Map<CategoryDto>(categorie);
                return StatusCode(200, categorieDto);
            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return BadRequest(e);
            }

        }





        /// <summary>
        /// Ajouter une catégorie
        /// </summary>
        /// <returns>La catégorie ajoutée</returns>
        /// <exception>Déclanche une exception d'application si la liste est vide</exception>
        // POST: api/Category/add
        [HttpPost("add")]
        public async Task<ActionResult<CategoryDto>> AddCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                _logger.LogInformation("Categorie/Add api Invoked (pour ajouter une nouvelle catégorie) ...");
                if (!ModelState.IsValid)
                {
                    return BadRequest("Enter a valid category name");
                }
                var category = _mapper.Map<Category>(categoryDto);
                var categoryReturne = await _categoryService.AddCategory(category);
                return StatusCode(201, categoryReturne);
            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return BadRequest(e.Message);
            }

        }



    }
}
