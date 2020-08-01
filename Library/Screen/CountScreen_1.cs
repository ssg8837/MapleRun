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
    public class CountScreen_1 : GameScreen
    {
        Image count_1;

        public override void Load()
        {
            base.Load();

            count_1 = new Image(PathManager.GetPureName(PathManager.Count[0]));
            count_1.Load();
            count_1.Position = new Vector2(SettingManager.WINDOW_WIDTH / 2 - count_1.Rect.Width / 2, SettingManager.WINDOW_HEIGHT / 3);

            list = new List<MyObject>();
            list.Add(count_1);

            Init();
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }
        public override void Draw(SpriteBatch sp, GameTime gt)
        {
            count_1.Draw(sp);

            base.Draw(sp, gt);
        }
        public override void Unload()
        {
            base.Unload();
        }
    }
}
