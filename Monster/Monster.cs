using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapleRun0509.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapleRun0509.Map;

namespace MapleRun0509.Monster
{
    public class MONSTER : MyObject
    {
        #region 변수
        public Image img;
        public Animation AnimationMove;
        public Vector2 velocity;
        
        public Point size;
        public int speed;        
        public bool hasJumped;        
        SpriteEffects effect;
        public List<MyObject> list;
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 생성자
        public MONSTER()
        {
            velocity = Vector2.Zero;                        
            effect = SpriteEffects.None;
            hasJumped = true;
            list = new List<MyObject>();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public virtual void Load()
        {
            Random rand = new Random();

            AnimationMove = new Animation();
            AnimationMove.Load(PathManager.MonsterAnimation[rand.Next(0,MonsterInfo.DicInfo.Count())]);
            size = new Point(AnimationMove.Width, AnimationMove.Height);

            velocity.X = rand.Next(1, 30) / 10 + (float)rand.Next(0, MonsterInfo.DicInfo.Count()) / 10 + 0.5f;
            AnimationMove.Speed = (int)velocity.X * (-10) + 100;
        }
        public virtual void Load(int type)
        {
            Random rand = new Random();
           
            AnimationMove = new Animation();
            AnimationMove.Load(PathManager.MonsterAnimation[type]);
            size = new Point(AnimationMove.Width, AnimationMove.Height);

            velocity.X = rand.Next(1, 30) / 10 + (float)type/10 + 0.5f;
            AnimationMove.Speed = (int)velocity.X * (-10) + 100;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Unload
        public virtual void Unload()
        {
            AnimationMove.Unload();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Draw
        public virtual void Draw(SpriteBatch sp, GameTime gt)
        {
            AnimationMove.Draw(sp, gt, pos, effect);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public virtual void Update(GameTime gt)
        {
            if (velocity.X > 0)
            {
                effect = SpriteEffects.None;
            }
            else
            {
                effect = SpriteEffects.FlipHorizontally;
            }            
            if (hasJumped)
            {
                float i = 1;
                velocity.Y += 0.2f * i;
            }
            else
            {
                velocity.Y = 0;
            }
            if (pos.Y + AnimationMove.Height == SettingManager.WINDOW_HEIGHT)
            {
                hasJumped = false;
                velocity.Y = 0;
            }
            pos.X -= velocity.X;
            pos.Y += velocity.Y;

            if (pos.X < 0)
            {
                pos.X = 0;
                velocity.X *= -1;
            }
            if (pos.Y < 0)
            {
                pos.Y = 0;
            }
            if (pos.Y > SettingManager.WINDOW_HEIGHT - AnimationMove.Height)
            {
                pos.Y = SettingManager.WINDOW_HEIGHT - AnimationMove.Height;
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region CheckTile
            /*
             * Monster와 Tile을 Check하는 함수
             * Tile의 GetGround값과 Monster의 위치를 수식으로 계산해서
             * Monster가 Tile위에 서있도록 한다.
             */
        public void CheckTile(List<TileInfo> list)
        {
            int y = (int)pos.Y;
            int x = (int)pos.X + size.X / 2;
            
            foreach (TileInfo tile in list)
            {
                if ((tile.pos.X <= x && x < tile.pos.X + tile.size.X))
                {
                    if ((y + size.Y- tile.GetGround(x)) >= 0)
                    {
                        pos.Y = tile.GetGround(x) - size.Y;
                        hasJumped = false;
                    }
                    else
                    {
                        hasJumped = true;
                    }
                }
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
