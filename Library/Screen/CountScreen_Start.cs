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
    public class CountScreen_Start : GameScreen
    {
        Image count_start;

        public override void Load()
        {
            base.Load();

            count_start = new Image(PathManager.GetPureName(PathManager.Count[3]));
            count_start.Load();
            count_start.Position = new Vector2(SettingManager.WINDOW_WIDTH / 2 - count_start.Rect.Width / 2, SettingManager.WINDOW_HEIGHT / 3);

            list = new List<MyObject>();
            list.Add(count_start);

            Init();
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }
        public override void Draw(SpriteBatch sp, GameTime gt)
        {
            count_start.Draw(sp);

            base.Draw(sp, gt);
        }
        public override void Unload()
        {
            base.Unload();
        }
    }
}
