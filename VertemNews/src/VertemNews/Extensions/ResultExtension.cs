using Microsoft.AspNetCore.Mvc;

namespace VertemNews.Extensions
{
    public static class ResultExtension
    {
        public static NoContentResult ToSuccessRequest()
        {
            return new NoContentResult();
        }

        public static OkObjectResult ToSuccessRequest<T>(this T result) where T : class
        {
            return new OkObjectResult(result);
        }

        //public static OkObjectResult ToSuccessRequest(this result)
        //{
        //    return new OkObjectResult(new { result });
        //}

        //public static ActionResult ToBadRequest(this Result result)
        //{
        //    return new BadRequestObjectResult(new { result.Error });
        //}

        public static ActionResult ToBadRequest<T>(this T result) where T : class
        {
            return new BadRequestObjectResult(new { result });
        }
    }
}