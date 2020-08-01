using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapleRun0509.Library;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MapleRun0509.Map
{
    public class Background : MyObject
    {
        
        Image[] img;
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public void Load()
        {
            img = new Image[2];
            for (int i = 0; i < img.Count(); i++)
            {
                img[i] = new Image(PathManager.Map_Background);
                img[i].Load();
                img[i].Alpha = 0.9f;
            }
            img[1].X = img[0].Rect.Width;
            img[1].Rect = new Rectangle(img[0].Rect.Width, 0, img[0].Rect.Width, img[0].Rect.Height);          
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Draw
        public void Draw(SpriteBatch sp,GameTime gt)
        {
            img[0].Draw(sp, img[0].Rect);
            img[0].Draw(sp, img[1].Rect);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public void Update(GameTime gt, Vector2 pos)
        {
            for (int i=0;i<img.Count();i++)
            {
                // 플레이어의 위치와 계산해서,
                // 1,2번 이미지 중에서 뒤로 많이 넘어간 이미지의 위치를
                // 다음 이미지에 연달아서 올 수 있게 설정해준다.
                if (img[1-i].Rect.Right - pos.X <= SettingManager.WINDOW_WIDTH)
                {
                    img[i].X = img[1-i].Rect.Right;
                }
                img[i].Update(gt);
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
