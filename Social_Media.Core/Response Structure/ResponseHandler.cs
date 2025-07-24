namespace Social_Media.Core.Response_Structure
{
    public class ResponseHandler
    {
        /// <summary>
        /// For Get EndPoint
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public Response<T> OK<T>(T entity, string Message = default!)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = Message,
                Data = entity,
                Succeeded = true,
                Errors = default
            };
        }
        /// <summary>
        /// For Post EndPoint (Create New Entity)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Message"></param>
        /// <returns></returns>
        public Response<T> Created<T>(string Message = default!)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Message = Message,
                Data = default,
                Succeeded = true,
                Errors = default
            };
        }
        /// <summary>
        /// For Delete EndPoint (Delete Any Entity)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Message"></param>
        /// <returns></returns>
        public Response<T> NoContent<T>(string Message = default!)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = Message,
                Data = default,
                Succeeded = true,
                Errors = default
            };
        }
        /// <summary>
        /// For UnAuthorized User 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Message"></param>
        /// <returns></returns>
        public Response<T> UnAuthorized<T>(string Message = default!)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Message = Message,
                Data = default,
                Succeeded = false,
                Errors = default
            };
        }
        /// <summary>
        /// For BadRequest EndPoint 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Message"></param>
        /// <param name="Errors"></param>
        /// <returns></returns>
        public Response<T> BadRequest<T>(string Message = default!, List<string> Errors = default!)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = Message,
                Data = default,
                Succeeded = false,
                Errors = Errors
            };
        }
        /// <summary>
        /// For Forbidden
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Message"></param>
        /// <param name="Errors"></param>
        /// <returns></returns>
        public Response<T> ForBidden<T>(string Message = default!, List<string> Errors = default!)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Forbidden,
                Message = Message,
                Data = default,
                Succeeded = false,
                Errors = Errors
            };
        }
        /// <summary>
        /// For NotFound Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Message"></param>
        /// <returns></returns>
        public Response<T> NotFound<T>(string Message = default!)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Data = default,
                Succeeded = false,
                Errors = default,
                Message = Message
            };
        }
    }
}
