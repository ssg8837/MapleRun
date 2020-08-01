using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleRun0509.Library
{
    public class GameUiScreen : GameScreen
    {
        Image gameUi;

        public override void Load()
        {
            base.Load();

            gameUi = new Image(PathManager.GameUi);
            gameUi.Load();
            gameUi.Position = new Vector2(0, 0);
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }
        public override void Draw(SpriteBatch sp, GameTime gt)
        {
            gameUi.Draw(sp);

            base.Draw(sp, gt);
        }
        public override void Unload()
        {
            base.Unload();
        }
    }
}
