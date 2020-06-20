using System;
using System.Collections.Generic;

namespace Design.Pattern.CSharp
{
    public class StrategyPattern
    {
        public static void Main(string[] args)
        {
            var NoSaleDay = new NoSaleDay();
            var BlackFridaySale = new BlackFridaySale();

            var NoSalesDayCustomer = new Customer(NoSaleDay);
            NoSalesDayCustomer.Add(1.0, 1);
            NoSalesDayCustomer.Add(1.0, 2);
            NoSalesDayCustomer.PrintCartValue();


            Customer BlackFridaySalesDayCustomer = new Customer(BlackFridaySale);
            BlackFridaySalesDayCustomer.Add(2, 1);
            BlackFridaySalesDayCustomer.Add(2, 2);
            BlackFridaySalesDayCustomer.Add(1, 2);
            BlackFridaySalesDayCustomer.PrintCartValue();
            
            Console.ReadLine();
        }
    }


    class Customer
    {
        private IList<double> CartItem;
        public IPricingStrategy Strategy { get; set; }

        public Customer(IPricingStrategy strategy)
        {
            this.CartItem = new List<double>();
            this.Strategy = strategy;
        }

        public void Add(double price, int quantity)
        {
            this.CartItem.Add(this.Strategy.GetItemPrice(price * quantity));
        }

        // Payment of Cart Value
        public void PrintCartValue()
        {
            double sum = 0;
            foreach (var price in this.CartItem)
            {
                sum += price;
            }
            Console.WriteLine($"Total Checkout Amount: {sum}");
        }
    }

    interface IPricingStrategy
    {
        double GetItemPrice(double rawPrice);
    }

    // Normal Sales strategy (No Discount)
    class NoSaleDay : IPricingStrategy
    {
        public double GetItemPrice(double rawPrice) => rawPrice;
    }

    // Strategy for Black Friday Sales (40% discount)
    class BlackFridaySale : IPricingStrategy
    {
        public double GetItemPrice(double rawPrice) => rawPrice * 0.4;
    }
}
