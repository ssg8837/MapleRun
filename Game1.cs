using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MapleRun0509.Library;
namespace MapleRun0509
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Game1()
        {
            #region 건들지 말 것
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            #endregion
        }

        #region Initialize     
        protected override void Initialize()
        {
            SettingManager.SetWindowSize(graphics,800, 450);

            #region 건들지 말 것
            this.IsMouseVisible = true;
            base.Initialize();
            #endregion
        }
        #endregion

        #region LoadContent
        protected override void LoadContent()
        {
            #region 건들지 말 것
            spriteBatch = new SpriteBatch(GraphicsDevice);
            #endregion

            ScreenManager.Instace.graphics = graphics;
            ScreenManager.Instace.spriteBatch = spriteBatch;
            ScreenManager.Instace.Load(Content);
        }
        #endregion

        #region Unload
        protected override void UnloadContent()
        {
            ScreenManager.Instace.Unload();
        }
        #endregion

        #region Update
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            ScreenManager.Instace.Update(gameTime,this);

            #region 건들지 말 것
            base.Update(gameTime);
            #endregion
        }
        #endregion

        #region Draw
        protected override void Draw(GameTime gameTime)
        {

            #region 건들지 말 것
            GraphicsDevice.Clear(Color.CornflowerBlue);
            #endregion

         //   spriteBatch.Begin();
            //spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend,null,null,null,null,CameraManager.Instance.mx);
            ScreenManager.Instace.Draw(spriteBatch,gameTime);
            //spriteBatch.End();
            
            #region 건들지 말 것
            base.Draw(gameTime);
            #endregion
        }
        #endregion
    }
}
