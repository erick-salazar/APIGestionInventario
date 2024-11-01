using System.ComponentModel.DataAnnotations;

namespace APIGestionInventario.DTOs.Custom
{
    public class GetAllParameter
    {
        public int? Limite { get; set; }
        public int? Salto { get; set; }
    }
}
