using Microsoft.AspNetCore.Mvc;
using APIGestionInventario.Models;
using APIGestionInventario.DTOs.Request;
using System.Net;
using APIGestionInventario.DTOs.Response;
using APIGestionInventario.Middleware;
using Microsoft.AspNetCore.Authorization;
using APIGestionInventario.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using APIGestionInventario.DAL.Repositories;
using APIGestionInventario.DTOs.Custom;

namespace APIGestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _IUsuarioRepository;
        private readonly IJWTServices _IJWTServices;
        private readonly IGeneralServices _IGeneralServices;
        private readonly IMemoryCache _IMemoryCache;
        private readonly MemoryCacheEntryOptions _MemoryCacheEntryOptions;

        public AuthController(
            IUsuarioRepository usuarioRepository,
            IJWTServices jWTServices,
            IGeneralServices generalServices,
            IMemoryCache memoryCache
        )
        {
            _IUsuarioRepository = usuarioRepository;
            _IJWTServices = jWTServices;
            _IGeneralServices = generalServices;
            _IMemoryCache = memoryCache;
            _MemoryCacheEntryOptions = _IGeneralServices.ObtenerMemoryCacheOptions();
        }

        /// <summary>
        /// Login API
        /// </summary>
        /// <response code="200">Login correcto (success)</response>
        /// <response code="400">Error en parametros de solicitud</response>
        /// <response code="401">Usuario o contraseña incorrecta</response>
        /// <response code="500">Error de servidor</response>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (ModelState.IsValid)
            {
                Usuario? usuario = await _IUsuarioRepository.Login(request.UsuarioId, request.Password);

                if (usuario != null)
                {
                    var token = _IJWTServices.CrearTokenJWT(usuario);
                    return Ok(new LoginResponseDto
                    {
                        UsuarioId = usuario.UsuarioId,
                        UsuarioNombre = usuario.UsuarioNombre,
                        UsuarioApellido = usuario.UsuarioApellido,
                        RolId = usuario.RolId,
                        Token = token
                    });
                }

                throw new CustomError((int)HttpStatusCode.Unauthorized, "0005", "Usuario o contraseña incorrecta", null);
            }

            var errors = _IGeneralServices.ModelDetalleErrores(ModelState);

            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <response code="200">Refresh token correcto (success)</response>
        /// <response code="401">Token expirado</response>
        /// <response code="500">Error de servidor</response>
        [HttpGet]
        [Route("RefreshToken")]
        [Authorize(Roles = "Administrador,Empleado")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseGenericAPI<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseGenericAPI<GetAllResult<object>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken()
        {
            string? nameid = _IJWTServices.ObtenerClaimJWT(Request, "nameid");

            if (!string.IsNullOrEmpty(nameid))
            {
                var cacheKey = $"RefreshToken/{nameid}";

                if (!_IMemoryCache.TryGetValue(cacheKey, out Usuario? usuario))
                {
                    usuario = await _IUsuarioRepository.RefreshToken(nameid);
                    _IMemoryCache.Set(cacheKey, usuario, _MemoryCacheEntryOptions);
                }

                usuario = usuario ?? throw new CustomError((int)HttpStatusCode.Unauthorized, "0005", "Token expirado", null);

                var token = _IJWTServices.CrearTokenJWT(usuario);

                return Ok(new LoginResponseDto
                {
                    UsuarioId = usuario.UsuarioId,
                    UsuarioNombre = usuario.UsuarioNombre,
                    UsuarioApellido = usuario.UsuarioApellido,
                    RolId = usuario.RolId,
                    Token = token
                });

            }

            throw new CustomError((int)HttpStatusCode.Unauthorized, "0005", "Token expirado", null);
        }
    }
}
