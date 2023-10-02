namespace Nnow.Api
{
    public class ResponseApi
    {

        public object Data { get; set; }
        public int StatusCode { get; set; }
        public List<string> Validations { get; set; } = new List<string>();

    }
}
