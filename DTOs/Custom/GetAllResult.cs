namespace APIGestionInventario.DTOs.Custom
{
    public class GetAllResult<T>
    {
        public List<T> ListaDetalle { get; set; } = [];

        public int TotalRegistro { get; set; }
    }
}
