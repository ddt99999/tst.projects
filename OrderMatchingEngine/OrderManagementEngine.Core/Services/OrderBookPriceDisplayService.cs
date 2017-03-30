using System;
using System.Collections.Generic;
using System.Linq;
using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Services
{
    public class OrderBookPriceDisplayService : IOrderBookPriceDisplayService
    {
        private readonly IOrderBookRepository orderBookRepository;
        private readonly object locker = new object();

        public OrderBookPriceDisplayService(IOrderBookRepository orderBookRepository)
        {
            this.orderBookRepository = orderBookRepository;
        }

        public void DisplayPrices(Order order, int level)
        {
            KeyValuePair<decimal, OrderList>[] topBids;
            KeyValuePair<decimal, OrderList>[] topAsks;
            lock (locker)
            {
                var orderBook = orderBookRepository.GetOrderBook(order);

                topBids = orderBook.Bids.GetPriceTree().Reverse().Take(level).ToArray();
                topAsks = orderBook.Asks.GetPriceTree().Take(level).ToArray();
            }
            
            // bids
            for (var i = 0; i < level; i++)
            {
                Console.Write("|     |");
            }

            // asks
            for (var i = 0; i < level; i++)
            {
                Console.Write("|     |");
            }

            Console.WriteLine();

            for (var i = level; i > 0; i--)
            {
                Console.Write("|Bid{0} |", i);
            }

            for (var i = 0; i < level; i++)
            {
                Console.Write("|Ask{0} |", i+1);
            }

            Console.WriteLine();

            for (var i = level - 1; i >= 0; i--)
            {
                try
                {
                    Console.Write("|{0}|", topBids[i].Key);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.Write("|     |");
                }
                
            }

            for (var i = 0; i < level; i++)
            {
                try
                {
                    Console.Write("|{0}|", topAsks[i].Key);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.Write("|     |");
                }
                
            }

            Console.WriteLine();

            for (var i = level - 1; i >= 0; i--)
            {
                try
                {
                    Console.Write("|{0}  |", topBids[i].Value.Volume);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.Write("|     |");
                }
            }

            for (var i = 0; i < level; i++)
            {
                try
                {
                    Console.Write("|{0}  |", topAsks[i].Value.Volume);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.Write("|     |");
                }               
            }
        }
    }
}