namespace SovTech.Models
{
    public class ResponseModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public dynamic Data { get; set; }
    }
}