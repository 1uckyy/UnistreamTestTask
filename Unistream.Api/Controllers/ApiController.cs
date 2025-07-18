﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Unistream.Api.Controllers;

/// <summary>
/// Represents the base API controller.
/// </summary>
[ApiController]
public abstract class ApiController : ControllerBase
{
    private ISender _sender;

    /// <summary>
    /// Gets the sender.
    /// </summary>
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
}