using Microsoft.AspNetCore.Mvc.Controllers;

namespace BookChangelog.API.Infrastructure;

public static class SwaggerExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(opts =>
        {
            opts.SupportNonNullableReferenceTypes();
            opts.TagActionsBy(description =>
            {
                if (description.GroupName != null)
                {
                    return new[] { description.GroupName };
                }

                if (description.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
                {
                    var namespaceLastPiece = actionDescriptor.MethodInfo.DeclaringType!.Namespace!.Split('.').Last();
                    return new[] { namespaceLastPiece };
                }

                return Array.Empty<string>();
            });
        });
    }
}