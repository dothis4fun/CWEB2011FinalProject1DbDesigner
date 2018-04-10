using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoneStore.Models;

namespace StoneStore.Entities
{
    public class CartLine
    {
        public Stone Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

        public void Additem(Stone myproduct, int myquantity)
        {
            CartLine line = lineCollection.Where(p => p.Product.Id == myproduct.Id).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = myproduct,
                    Quantity = myquantity
                });
            }
            else
            {
                line.Quantity += myquantity;
            }
        }

        public void RemoveLine(Stone myproduct)
        {
            lineCollection.RemoveAll(p => p.Product.Id == myproduct.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(p => p.Product.Price * p.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
}