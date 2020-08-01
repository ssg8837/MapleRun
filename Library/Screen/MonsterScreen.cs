using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapleRun0509.Monster;
using MapleRun0509.Map;

namespace MapleRun0509.Library
{
    class MonsterScreen : GameScreen
    {
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 변수
        public List<MONSTER> monsterList;
        Random rand;
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public override void Load()
        {
            base.Load();
            MonsterInfo.Init();
            rand = new Random();
            monsterList = new List<MONSTER>();
            for (int i = 0; i < 5; i++)
            {
                MONSTER m = new MONSTER();
                m.Load(i % MonsterInfo.DicInfo.Count());
                monsterList.Add(m);
            }
            foreach (MONSTER mon in monsterList)
            {
                mon.pos.X = SettingManager.WINDOW_WIDTH + mon.velocity.X *300;
            }

            list = new List<MyObject>();

            foreach (MyObject mon in monsterList)
            {
                foreach (MyObject obj in ((MONSTER)mon).AnimationMove.imgList)
                {
                    list.Add(obj);
                }
            }
            Init();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Unload
        public override void Unload()
        {
            base.Unload();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Draw
        public override void Draw(SpriteBatch sp, GameTime gt)
        {
            foreach (MONSTER mon in monsterList)
            {
                mon.Draw(sp, gt);
            }
            base.Draw(sp, gt);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public override void Update(GameTime gt)
        {
            foreach (MONSTER mon in monsterList)
            {
                mon.Update(gt);
            }
            base.Update(gt);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region CheckTile
        public void CheckTile(List<TileInfo> list)
        {
            /*
             * Monster와 Tile을 Check하는 함수
             * Monster가 Tile 위에 서있게 한다고 보면 된다.
            */
            foreach (MONSTER mon in monsterList)
            {
                mon.CheckTile(list);
            }
        }

        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region CheckPlayer
        public void CheckPlayer(Rectangle champ)
        {
            /*
             * Monster가 Player로부터 일정 거리 뒤로 멀어져서
             * 화면 밖으로 나가게 되면,
             * MonsterList로부터 해당 Monster를 삭제하고
             * 새로운 Monster를 Add한다.
             */
            for (int i = 0; i < monsterList.Count(); i++)
            {
                if (champ.X - monsterList.ElementAt(i).pos.X >= 300)
                {
                    Add(new Vector2(champ.X,champ.Y));
                    monsterList.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Add
        public void Add(Vector2 player)
        {
            /*
             * 새로운 Monster를 생성해서
             * Monster List에 추가한다.
             */
            MONSTER mon = new MONSTER();
            mon.Load();
            mon.X = player.X + SettingManager.WINDOW_WIDTH;
            mon.Y = SettingManager.WINDOW_HEIGHT / 2;
            monsterList.Add(mon);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}