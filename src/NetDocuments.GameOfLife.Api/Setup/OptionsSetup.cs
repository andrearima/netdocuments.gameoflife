using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetDocuments.GameOfLife.Api.Handler;

namespace NetDocuments.GameOfLife.Api.Setup;

public class OptionsSetup : IConfigureOptions<MvcOptions>
{
    public void Configure(MvcOptions options)
    {
        options.Filters.Add(typeof(ExceptionFilter));
    }
}
