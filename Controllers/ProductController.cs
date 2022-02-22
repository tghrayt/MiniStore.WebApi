using AutoMapper;
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
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, IMapper mapper, ILogger<ProductController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation("Products controller Invoked ...");
        }





        /// <summary>
        /// Retourne toute la liste des Produits
        /// </summary>
        /// <returns>La liste des produits</returns>
        /// <response code="200">Liste des produits</response>
        /// <response code="404">la liste introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si la liste est vide</exception>
        // GET: api/Product/products
        [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpGet("products")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("products api Invoked (pour obtenir la liste des produits) ...");
                var products = _productService.GetAllProducts();
                return StatusCode(200, products);
            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return new NotFoundResult();
            }

        }






        /// <summary>
        /// Retourne un produit selon l'id donné
        /// </summary>
        /// <returns>Le produit demandé</returns>
        /// <response code="200">Produit sélectionné</response>
        /// <response code="404">le produit est introuvable</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si le produit n'existe pas</exception>
        // GET: api/Product/products/{5}
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpGet("products/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductByID(int id)
        {
            try
            {
                _logger.LogInformation("products/id api Invoked (pour obtenir le produit demandée) ...");
                var produit = await _productService.GetProductByID(id);
                var produitDto = _mapper.Map<ProductDto>(produit);
                return StatusCode(200, produitDto);
            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return new NotFoundResult();
            }

        }


        /// <summary>
        /// Ajouter un produit
        /// </summary>
        /// <returns>Le produit ajouté</returns>
        /// <response code="201">Produit Ajouté avec succès</response>
        /// <response code="400">le produit est null</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si le produit est null ou l'un de ces champs null</exception>
        // POST: api/product/add
        [ProducesResponseType(typeof(ProductDto), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpPost("add")]
        public async Task<ActionResult<CategoryDto>> AddProduct([FromBody] ProductDto productDto)
        {
            try
            {
                _logger.LogInformation("Products/Add api Invoked (pour ajouter un nouveau produit) ...");
                if (!ModelState.IsValid)
                {
                    return BadRequest("un des champs est null");
                }
                var product = _mapper.Map<Product>(productDto);
                var productReturne = await _productService.AddProduct(product);
                return StatusCode(201, productReturne);
            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return BadRequest(e);
            }

        }




        /// <summary>
        /// Retourne le status de l'action de produit à supprimer
        /// </summary>
        /// <returns>boolean</returns>
        /// <response code="202">Produit suprimmé avec succès</response>
        /// <response code="400">le produit n'existe pas</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si le produit n'existe pas</exception>
        // GET: api/Category/categories/{5}
        [ProducesResponseType(typeof(ProductDto), 202)]
        [ProducesResponseType(typeof(NotFoundResult), 400)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDto>> DeleteProduct(int id)
        {
            try
            {
                _logger.LogInformation("prodcuts/id api Invoked (pour supprimer le produit souhaité) ...");
                var productStatus = await _productService.DeleteProduct(id);
                if (productStatus == false)
                {
                    return BadRequest("Ce produit n'existe plus !!");
                }
                return StatusCode(202, productStatus);

            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return BadRequest(e);
            }

        }






        /// <summary>
        /// Retourne le produit synchronisée
        /// </summary>
        /// <returns>Category</returns>
        /// <response code="204">Produit modifiée avec succès</response>
        /// <response code="404">le produit n'existe pas</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si le produit n'existe pas</exception>
        // GET: api/Category/categories/{5}
        [ProducesResponseType(typeof(ProductDto), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> UpdateCategory(int id, [FromBody] ProductDto productDto)
        {
            try
            {
                _logger.LogInformation("products/id api Invoked (pour modifier  la catégorie souhaitée) ...");
                var produit = _mapper.Map<Product>(productDto);
                var productUpdated = await _productService.UpdateProduct(id, produit);
                return StatusCode(204, productUpdated);

            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return new NotFoundResult();
            }

        }

    }
}
