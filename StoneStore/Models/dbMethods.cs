using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoneStore.Models
{
    public class dbMethods
    {
        public static void SaveProduct(Stone stone)
        {
            db_StoneEntities db = new db_StoneEntities();
            if (stone.Id == 0)
            {
                db.Stones.Add(stone);
            }
            else
            {
                Stone editedStone = db.Stones.Find(stone.Id);
                if (editedStone != null)
                {
                    editedStone.Category = stone.Category;
                    editedStone.Color = stone.Color;
                    editedStone.Name = stone.Name;
                    editedStone.Size = stone.Size;
                    editedStone.Price = stone.Price;
                }
            }
            db.SaveChanges();
        }

        public static Stone Deletestone(int stoneId)
        {
            db_StoneEntities db = new db_StoneEntities();
            Stone dbEntry = db.Stones.Find(stoneId);
            if (dbEntry != null)
            {
                db.Stones.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }
    }
}