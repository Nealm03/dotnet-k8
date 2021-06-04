using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnetk8
{
  [ApiController, Route("[controller]")]
  public class OrderController : ControllerBase
  {
    public OrderController()
    {

    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) => Ok(Order.Create(Guid.Parse(id)));
    [HttpPost]
    public async Task<IActionResult> Post() => Ok();
  }
}