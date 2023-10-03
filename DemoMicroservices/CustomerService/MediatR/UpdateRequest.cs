using System;
using Common.Infrastructure;
using Common.Models;
using MediatR;

namespace ConsumerAPI.MediatR;

public class UpdateRequest: IRequest<ApiResult>
{
    public Guid Id { get; set; }
    public bool IsApproved { get; set; }
}