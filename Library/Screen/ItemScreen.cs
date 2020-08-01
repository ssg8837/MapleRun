using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MapleRun0509.Item;
using MapleRun0509.Map;
using MapleRun0509.Monster;

namespace MapleRun0509.Library.Screen
{
    class ItemScreen : GameScreen
    {

        List<Items> itemList;
        public override void Load()
        {
            base.Load();
            itemList = new List<Items>();


            for (int i = 0; i < 5; i++)
            {
                Items item = new Items(0);
                item.pos = new Vector2(SettingManager.WINDOW_WIDTH / 3 + i * 125, 300);
                item.Load();

                itemList.Add(item);
            }

        }


        public override void Draw(SpriteBatch sp, GameTime gt)
        {


            foreach (Items item in itemList)
            {
                item.Draw(sp, gt);
            }

            base.Draw(sp, gt);
        }

        public override void Update(GameTime gt)
        {

            foreach (Items item in itemList)
            {
                item.Update(gt);
            }




            base.Update(gt);
        }




        public void Additem(GameTime gt, Rectangle playerRect, List<TileInfo> tileList)
        {
            Random r = new Random((int)gt.TotalGameTime.Milliseconds);
            int x, y;
            int type = r.Next(0, 5);


            x = playerRect.X + SettingManager.WINDOW_WIDTH + r.Next(0, 250);

            foreach (TileInfo tile in tileList)
            {
                if ((tile.pos.X <= x && x < tile.pos.X + tile.size.X))
                {

                    Items item = new Items(type);
                    y = tile.GetGround(x) - r.Next(40, 220);
                    item.pos = new Vector2(x, y);
                    item.Load();

                    itemList.Add(item);
                }
            }




        }



        public void itemRemove(Rectangle champ)
        {
            for (int i = 0; i < itemList.Count(); i++)
            {
                if (champ.X - itemList.ElementAt(i).pos.X >= 300)
                {

                    Vector2 tempVec = new Vector2(champ.X, champ.Y);
                    itemList.RemoveAt(i);
                    i--;
                }
            }
        }
        public List<Items> List
        {
            get { return itemList; }
        }


    }
}
