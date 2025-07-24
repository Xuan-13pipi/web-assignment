using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace web_assignment
{
    public static class Extensions
    {
        public static bool IsAjax(this HttpRequest request)
        { 
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        public static bool IsValid(this ModelStateDictionary ms, string key)
        {
            return ms.GetFieldValidationState(key) == ModelValidationState.Valid;
        }
    }
}
