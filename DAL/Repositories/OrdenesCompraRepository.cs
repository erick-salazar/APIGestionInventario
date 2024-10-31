using APIGestionInventario.Interfaces;
using APIGestionInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace APIGestionInventario.DAL.Repositories
{
    public class OrdenesCompraRepository : RepositoyGestionInventarioDB<OrdenesCompra>, IOrdenesCompraRepository
    {
        private readonly GestionInventarioContext _GestionInventarioContext;

        public OrdenesCompraRepository(
            GestionInventarioContext gestionInventarioContext
        ) : base(gestionInventarioContext)
        {
            _GestionInventarioContext = gestionInventarioContext;
        }

    }
}
