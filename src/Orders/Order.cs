using System;

namespace dotnetk8
{

  public class Order
  {
    public Guid Id { get; private set; }
    public Guid RestaurantId { get; private set; }
    private Order(Guid restaurantId)
    {
      Id = Guid.NewGuid();
      RestaurantId = restaurantId;
    }


    public static Order Create(Guid restuarantId)
    {
      return new Order(restuarantId);
    }
  }
}