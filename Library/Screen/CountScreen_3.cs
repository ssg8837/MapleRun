﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleRun0509.Library
{
    public class CountScreen_3 : GameScreen
    {
        Image count_3;
        public override void Load()
        {
            base.Load();

            count_3 = new Image(PathManager.GetPureName(PathManager.Count[2]));
            count_3.Load();
            count_3.Position = new Vector2(SettingManager.WINDOW_WIDTH / 2 - count_3.Rect.Width / 2, SettingManager.WINDOW_HEIGHT / 3);

            list = new List<MyObject>();
            list.Add(count_3);
            isEnd = false;

            Init();
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }
        public override void Draw(SpriteBatch sp, GameTime gt)
        {
            count_3.Draw(sp);

            base.Draw(sp, gt);
        }
        public override void Unload()
        {
            base.Unload();
        }
    }
}