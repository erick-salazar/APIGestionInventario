using APIGestionInventario.DTOs.Response;
using APIGestionInventario.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;

namespace APIGestionInventario.BAL
{
    public class GeneralServices : IGeneralServices
    {

        private readonly IConfiguration _IConfiguration;
        public GeneralServices(IConfiguration iConfiguration)
        {
            _IConfiguration = iConfiguration;
        }

        public List<ErrorDetail>? ModelDetalleErrores(ModelStateDictionary keyValuePairs)
        {
            return keyValuePairs.Keys
            .Where(key => keyValuePairs?[key]!.Errors.Count > 0)
            .Select(key => new ErrorDetail
            {
                Field = key,
                Message = string.Join(", ", keyValuePairs[key]!.Errors.Select(e => e.ErrorMessage))
            }).ToList();
        }

        public MemoryCacheEntryOptions ObtenerMemoryCacheOptions()
        {
            return new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(int.Parse(_IConfiguration["MemoryCacheEntryOptions:AbsoluteExpirationRelativeToNow"]!.ToString())), 
                SlidingExpiration = TimeSpan.FromMinutes(int.Parse(_IConfiguration["MemoryCacheEntryOptions:SlidingExpiration"]!.ToString())) 
            };
        }

    }
}
