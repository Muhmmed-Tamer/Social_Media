using System.Net;

namespace Social_Media.Core.Response_Structure
{
    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public bool Succeeded { get; set; }
    }
}
