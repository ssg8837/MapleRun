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
    public class GameUi_NumberScreen : GameScreen
    {
        public int run = 0;
        public int meso = 0;
        public int heart = 10;

        Number uiRun;
        Number uiMeso;
        Number uiHeart;

        bool isStart = false;

        public override void Load()
        {
            base.Load();

            uiRun = new Number(PathManager.Number_GameRun);
            uiRun.Load();
            uiMeso = new Number(PathManager.Number_GameUi);
            uiMeso.Load();
            uiHeart = new Number(PathManager.Number_GameUi);
            uiHeart.Load();
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);

            if (isStart == false)
            {
                run ++;
            }
        }
        public override void Draw(SpriteBatch sp, GameTime gt)
        {
            uiRun.Draw(sp, run/10, new Vector2(455, 30), EnumManager.Direction.RIGHT);
            uiMeso.Draw(sp, meso, new Vector2(625, 35), EnumManager.Direction.LEFT);
            uiHeart.Draw(sp, heart, new Vector2(625, 78), EnumManager.Direction.LEFT);



            base.Draw(sp, gt);
        }
        public override void Unload()
        {
            base.Unload();
        }
        public void connectLife(Player player)
        {
            heart = player.Life;
        }
        public void connectMeso(Player player)
        {
            meso = player.Meso;
        }
    }
}
