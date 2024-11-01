using APIGestionInventario.DTOs.Response;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace APIGestionInventario.Interfaces
{
    public interface IGeneralServices
    {
        List<ErrorDetail>? ModeDetalleErrores(ModelStateDictionary keyValuePairs);
    }
}
