// Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using MyApplication;

namespace MyApplication{
    
    enum Defaults{
        id = 100,
        orderId = 0,
        itemId = 128,
        orderItemId = 256,
        paid = 0
    }

    class Customer{
        private int _customerId;
        private string _customerName;
        private List<Order>? _orders;

        public Customer(): this((int)Defaults.id, "Peter"){}
        public Customer(int id): this(id, "Peter"){}
        public Customer(string name): this((int)Defaults.id, name){}
        public Customer(int id, string name){
            this._customerId = id;
            this._customerName = name;
            this._orders = new List<Order>();
        }

        public int getId(){
            return this._customerId;
        }
        public string getName(){
            return this._customerName;
        }
        public List<Order> getOrders(){
            return this._orders;
        }
        public void placeOrder(int orderId, int orderItemId, int itemId, string itemName){
            this._orders.Add(new Order(orderId));
            this._orders[0]._orderItems[0].Add(new OrderItem(orderItemId));
            this._orders[0]._orderItems[0]._Items[0].Add(new Item(itemId, itemName));
        }

    }

    class Order{
        private int _orderId;
        private DateTime _dateCreated;
        private bool _paid;
        private List<OrderItem>? _orderItems;

        public Order(): this((int)Defaults.orderId){}
        public Order(int orderId){
            this._orderId = orderId;
            this._dateCreated = DateTime.Now;
            this._paid = Convert.ToBoolean(Defaults.paid);
            this._orderItems = new List<OrderItem>();
        }
        public int getOrderId(){
            return _orderId;
        }
        public List<OrderItem> getOrderItems(){
            return _orderItems;
        }
        public bool getIsPaid(){
            return this._paid;
        }
    }
    class OrderItem{
        private int _orderItemId;
        private List<Item>? _Items;
        public OrderItem(): this((int)Defaults.orderItemId){}
        public OrderItem(int orderItemId){
            this._orderItemId = orderItemId; 
            this._Items = new List<Item>();
        }
        public int getOrderItemId(){
            return _orderItemId;
        }
    }
    class Item{
        private int _itemId;
        private string _itemName;

        public Item(): this((int)Defaults.itemId, "ItemName"){}
        public Item(int itemId): this(itemId, "ItemName"){}
        public Item(string itemName): this((int)Defaults.itemId, itemName){}
        public Item(int itemId, string itemName){
            this._itemId = itemId;
            this._itemName = itemName;
        }

    }

    class Program{
        public static void Main(string[] args){
            Customer Herbert = new Customer(0, "Herbert");
            Herbert.placeOrder(1000, 40, 99, "Apple");
        }
    }
}