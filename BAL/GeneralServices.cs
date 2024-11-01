using APIGestionInventario.DTOs.Response;
using APIGestionInventario.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace APIGestionInventario.BAL
{
    public class GeneralServices : IGeneralServices
    {
        public List<ErrorDetail>? ModeDetalleErrores(ModelStateDictionary keyValuePairs)
        {
            return keyValuePairs.Keys
            .Where(key => keyValuePairs?[key].Errors.Count > 0)
            .Select(key => new ErrorDetail
            {
                Field = key,
                Message = string.Join(", ", keyValuePairs[key].Errors.Select(e => e.ErrorMessage))
            }).ToList();
        }
    }
}
