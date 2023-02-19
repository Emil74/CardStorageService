using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CardStorageService.Services;
using CardStorageService.Models.Requests;
using CardStorageService.Data;

namespace CardStorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        #region Services

        private readonly ILogger<CardController> _logger;
        private readonly IClientRepositoryService _clientRepositoryService;

        #endregion

        #region Constructors

        public ClientController(ILogger<CardController> logger, IClientRepositoryService clientRepositoryService)
        {
            _logger = logger;
            _clientRepositoryService = clientRepositoryService;
        }

        #endregion


        #region Public Metods

        [HttpGet("create")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]

        public IActionResult Create([FromQuery] CreateClientRequest request)
        {
            try
            {
                var clientId = _clientRepositoryService.Create(new Client
                {
                    FirstName = request.FirstName,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic
                });
                return Ok(new CreateClientResponse
                {
                    ClientId = clientId
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create client error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 912,
                    ErrorMessage = "Create client error."
                });
            }
        }

        #endregion
    }
}
