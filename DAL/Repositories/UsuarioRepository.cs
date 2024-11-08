﻿
using APIGestionInventario.Interfaces;
using APIGestionInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestionInventario.DAL.Repositories
{
    public class UsuarioRepository : RepositoyGestionInventarioDB<Usuario>, IUsuarioRepository
    {
        private readonly GestionInventarioContext _GestionInventarioContext;
        public UsuarioRepository(
            GestionInventarioContext gestionInventarioContext
        ) : base(gestionInventarioContext)
        {
            _GestionInventarioContext = gestionInventarioContext;
        }

        public async Task<Usuario?> Login(string usuarioId, string password)
        {
            return await _GestionInventarioContext.Usuarios
                .AsNoTracking()
                .Include(x => x.Roles)
                .Where(
                    x => x.UsuarioId == usuarioId
                    && x.Password == password
                    && x.Estado == true
                 )
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario?> RefreshToken(string usuarioId)
        {
            return await _GestionInventarioContext.Usuarios
                .AsNoTracking()
                .Include(x => x.Roles)
                .Where(
                    x => x.UsuarioId == usuarioId 
                    && x.Estado == true
                )
                .FirstOrDefaultAsync();
        }
    }
}
