﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using APIGestionInventario.Models;
using APIGestionInventario.DTOs.Request;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using APIGestionInventario.DTOs.Response;
using APIGestionInventario.Middleware;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;
using APIGestionInventario.Interfaces;

namespace APIGestionInventario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _IUsuarioRepository;
        private readonly IJWTServices _IJWTServices;

        public AuthController(
            IUsuarioRepository usuarioRepository,
            IJWTServices jWTServices
        )
        {
            _IUsuarioRepository = usuarioRepository;
            _IJWTServices = jWTServices;
        }


        [HttpPost("login")]
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

            var errors = ModelState.Keys
            .Where(key => ModelState[key].Errors.Count > 0)
            .Select(key => new ErrorDetail
            {
                Field = key,
                Message = string.Join(", ", ModelState[key].Errors.Select(e => e.ErrorMessage))
            }).ToList();


            throw new CustomError((int)HttpStatusCode.BadRequest, null, "", errors);          
        }

        [HttpGet]
        [Route("RefreshToken")]
        [Authorize(Roles = "Administrador,Empleado")]
        public async Task<IActionResult> RefreshToken()
        {
            string? nameid = _IJWTServices.ObtenerClaimJWT(Request, "nameid");

            if (!string.IsNullOrEmpty(nameid))
            {
                Usuario? usuario = await _IUsuarioRepository.RefreshToken(nameid);

                if (usuario != null)
                {   // Generate token
                    var token = _IJWTServices.CrearTokenJWT(usuario);

                    // Return the token
                    return Ok(new LoginResponseDto
                    {
                        UsuarioId = usuario.UsuarioId,
                        UsuarioNombre = usuario.UsuarioNombre,
                        UsuarioApellido = usuario.UsuarioApellido,
                        RolId = usuario.RolId,
                        Token = token
                    });
                }
            }

            throw new CustomError((int)HttpStatusCode.Unauthorized, "0005", "Token expirado", null);
        }

        

    }
}