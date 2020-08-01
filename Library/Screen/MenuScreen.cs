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
    public class MenuScreen : GameScreen
    {
        Image menu;
        public Button btnStart;
        Button btnExit;

        public bool isStartClicked
        {
            get; set;
        }
        public bool isExitClicked
        {
            get; set;
        }


        public override void Load()
        {
            base.Load();
            menu = new Image(PathManager.GetPureName(PathManager.GetFileName(PathManager.Menu)));
            menu.Load();

            btnStart = new Button(PathManager.BtnStart);
            btnExit = new Button(PathManager.BtnExit);

            btnStart.Load();
            btnExit.Load();

            btnStart.Position = new Vector2(SettingManager.WINDOW_WIDTH / 2 - btnStart.Rect.Width / 2, SettingManager.WINDOW_HEIGHT / 2);
            btnExit.Position = new Vector2(SettingManager.WINDOW_WIDTH / 2 - btnExit.Rect.Width / 2, SettingManager.WINDOW_HEIGHT / 2 + 60);

            list = new List<MyObject>();
            list.Add(menu);
            list.Add(btnStart);
            list.Add(btnExit);
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);

            btnStart.Update(gt);
            btnExit.Update(gt);

            if (btnStart.isClicked)
            {
                isStartClicked = true;
            }
            else
            {
                isStartClicked = false;
            }

            if (btnExit.isClicked)
            {
                isExitClicked = true;
            }
            else
            {
                isExitClicked = false;
            }

        }
        public override void Draw(SpriteBatch sp, GameTime gt)
        {
            menu.Draw(sp);
            btnStart.Draw(sp);
            btnExit.Draw(sp);
            base.Draw(sp, gt);
        }
        public override void Unload()
        {
            base.Unload();
        }
    }
}