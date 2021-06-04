using System;
using System.Threading.Tasks;
using Grpc.Core;

namespace dotnetk8
{
  public class OrderService : OrderGrpcService.OrderGrpcServiceBase
  {
    public override Task<OrderResult> GetOrder(OrderQuery request, ServerCallContext context)
    {
      return Task.FromResult(new OrderResult
      {
        Id = request.Id
      });
    }
  }
}