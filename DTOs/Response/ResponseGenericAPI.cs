namespace APIGestionInventario.DTOs.Response
{
    public class ResponseGenericAPI<T>
    {
        public int Status { get; set; }
        public string Code { get; set; } = null!;
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
        public List<ErrorDetail>? Error { get; set; }
        public string TraceId { get; set; } = null!;
    }
    public class ErrorDetail
    {
        public string Field { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
