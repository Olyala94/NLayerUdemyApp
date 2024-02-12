using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    public class NotfoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotfoundFilter(IService<T> service)
        {
            _service = service;
        }

        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    var idValue = context.ActionArguments.Values.FirstOrDefault();

        //    if (idValue != null)
        //    {
        //        await next.Invoke();
        //        return;
        //    }
        //    var id = (int)idValue;
        //    var anyEntity = await _service.AnyAsync(x=>x.Id== id);

        //    if(anyEntity) 
        //    {
        //        await next.Invoke();
        //        return;
        //    }
        //    context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found"));
        //}

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();

            // Check if idValue is null or not an integer
            if (idValue == null || !int.TryParse(idValue.ToString(), out int id))
            {
                context.Result = new BadRequestObjectResult("Invalid ID");
                return;
            }

            var anyEntity = await _service.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found"));
        }

    }

}
