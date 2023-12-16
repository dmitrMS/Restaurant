using BLL.DTO;
using DAL;
using DomainModel;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private Model1 db;
        public OrderService()
        {
            db = new Model1();
        }
        Random rnd = new Random();
        public bool MakeOrder(orderDto orderDto)
        {

            List<dish> orderedDishes = new List<dish>();
            int sum = 0;
            string status = "cook";
            foreach (var pId in orderDto.OrderedDishesIds)
            {
                dish dish = db.dishes.Find(pId);
                // валидация
                if (dish == null)
                    throw new Exception("Блюдо не найдено");
                sum += (int)dish.price;
                orderedDishes.Add(dish);
            }
            //применяем скидку
            sum = (int)new discount(0.1).GetDiscountedPrice(sum);

            order order = new order
            {
                id = (int)db.dishes.Local.ToBindingList().Max(x => x.id) + 1+ (rnd.Next()%100),
                order_status = status,
                date = DateTime.Now,
                summ = sum,
                //dish = orderDto.dish,
                //dish_string = orderedphones

            };
            db.orders.Add(order);
            if (db.SaveChanges() > 0)
                return true;
            return false;

        }
        public bool Save()
        {
            if (db.SaveChanges() > 0) return true;
            return false;
        }
        public orderDto GetOrder(int Id)
        {
            return new orderDto(db.orders.Find(Id));
        }
        public void CreateOrder(orderDto p)
        {
            db.orders.Add(new order() { id_stol = p.id_stol, id = p.id, summ = p.summ, order_status = p.order_status});
            Save();
        }
        public void UpdateOrder(orderDto p)
        {
            order ph = db.orders.Find(p.id);
            ph.id_stol = p.id_stol;
            ph.summ = p.summ;
            ph.order_status = p.order_status;
            Save();
        }
        public void DeleteOrder(int id)
        {
            order p = db.orders.Find(id);
            if (p != null)
            {
                db.orders.Remove(p);
                Save();
            }
        }

        public List<orderDto> GetAllOrders()
        {
            var res = db.orders.ToList();
            return res.Select(i => new orderDto(i)).ToList();
        }
    }
}