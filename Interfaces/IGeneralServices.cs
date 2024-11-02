using APIGestionInventario.DTOs.Response;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;

namespace APIGestionInventario.Interfaces
{
    public interface IGeneralServices
    {
        List<ErrorDetail>? ModelDetalleErrores(ModelStateDictionary keyValuePairs);

        MemoryCacheEntryOptions ObtenerMemoryCacheOptions();
    }
}
