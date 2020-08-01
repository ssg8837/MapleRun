using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace MapleRun0509.Library
{
    public class Image : MyObject
    {
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        #region 변수
        string path;                                        // 이미지의 경로              
        RenderTarget2D renderTarget;                        // ScreenManager의 GraphicsDevice로부터 RenderTarget을 설정하기 위해서.
        ContentManager Content;                             // ScreenManager의 Content로부터 받아오는 Content        
        #endregion

        #region 변수들에 대한 프로퍼티
        public RenderTarget2D RenderTarget
        {
            get { return renderTarget; }
            set { renderTarget = value; }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 생성자
        public Image(string Path) : base()
        {
            this.path = Path;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public void Load()
        {
            this.Content = new ContentManager(ScreenManager.Instace.Content.ServiceProvider, "Content");
            Texture = Content.Load<Texture2D>(path);
            Position = Vector2.Zero;
            Rect = new Rectangle((int)pos.X, (int)pos.Y, Texture.Width, Texture.Height);
            Origin = new Vector2(Rect.Width / 2, Rect.Height / 2);

            #region 건들지 말 것
            RenderTarget = new RenderTarget2D(ScreenManager.Instace.graphics.GraphicsDevice, Rect.Width, Rect.Height);
            ScreenManager.Instace.graphics.GraphicsDevice.SetRenderTarget(RenderTarget);
            ScreenManager.Instace.graphics.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instace.spriteBatch.Begin();
            ScreenManager.Instace.spriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instace.spriteBatch.End();
            Texture = RenderTarget;
            ScreenManager.Instace.graphics.GraphicsDevice.SetRenderTarget(null);
            #endregion
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Unload
        public void Unload()
        {
            Content.Unload();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public void Update(GameTime gt)
        {
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;

            if(pos.X < 0) // 이미지가 화면의 왼쪽을 벗어나지 않도록 하는 부분
            {
                pos.X = 0;
            }
            if(pos.Y < 0) // 이미지가 화면의 위쪽을 벗어나지 않도록 하는 부분
            {
                pos.Y = 0;
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Draw

        public void Draw(SpriteBatch sp, Vector2 pos, SpriteEffects effect = SpriteEffects.None)
        {
            if (isVisible)
            {
                sp.Draw(Texture, pos + Origin, Rect, Color.White * Alpha, 0.0f, Origin,Scale, effect, 0.0f);
            }
        }
        public void Draw(SpriteBatch sp, SpriteEffects effect = SpriteEffects.None)
        {
            if (isVisible)
            {
                this.Draw(sp,Position,effect);
            }
        }
        public void Draw(SpriteBatch sp,Rectangle temp)
        {
            if(isVisible)
            {
                sp.Draw(Texture, temp, Color.White);
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *         
    }
}
