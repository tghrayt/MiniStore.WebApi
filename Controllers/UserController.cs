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
    public class UserController : ControllerBase    
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;

        }


        /// <summary>
        /// Retourne L'utilisateur enrégistré
        /// </summary>
        /// <returns>L'utilisateur enrégistré</returns>
        /// <response code="201">L'utilisateur est enrégistré avec succès</response>
        /// <response code="400">Cet utilisateur existe déja ou veillez saisir tous les champs</response>
        /// <response code="500">Oops! le service est indisponible pour le moment</response>
        /// <exception>Déclanche une exception d'application si l'un des champs vide</exception>
        // GET: api/Product/products
        [ProducesResponseType(typeof(User), 201)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(void), 500)]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            try
            {
                _logger.LogInformation("users api Invoked (pour enregistrer un nouveau utilisateur) ...");
                //validation
                userForRegisterDto.userName = userForRegisterDto.userName.ToLower();
                if(await _userService.UserExist(userForRegisterDto.userName))
                {
                    _logger.LogWarning("Cet utilisateur existe déja, Veillez saisir un autre nom !");
                    return BadRequest("Cet utilisateur existe déja, Veillez saisir un autre nom !");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning(ModelState.ToString());
                    return BadRequest(ModelState);
                }
                var userToCreate = new User
                {
                    UserName = userForRegisterDto.userName
                };

                var createdUser = await _userService.Register(userToCreate, userForRegisterDto.password);
               

                return StatusCode(201, createdUser);
            }
            catch (Exception e)
            {
                _logger.LogError("une erreur est survenue lors de traitement, avec un message de : " + e.Message);
                return new NotFoundResult();
            }

        }
    }
}
