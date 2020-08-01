using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MapleRun0509.Item;
using MapleRun0509.Map;
using MapleRun0509.Monster;

namespace MapleRun0509.Library
{
    public class PlayScreen : GameScreen
    {
        Player player;
        public Vector2 Position
        {
            get { return player.img.Position; }//플레이어 그림의 포지션 반환
        }
        public Rectangle Rect
        {
            get { return player.img.Rect; }//플레이어 그림을 담는 틀 반환(넓이,높이 구할때 사용)
        }
        public Player Hero//플레이어스크린이 사용하는 플레이어에 접근하게 해줌
        {
            get { return player; }
            set { player = value; }
        }

        //int count = 0;
        public override void Load()
        {
            base.Load();
            player = new Player();//플레이어 초기화
            player.Load(PathManager.Player);


            list = new List<MyObject>();
            foreach (MyObject obj in player.AnimationMove.imgList)
            {
                list.Add(obj);
            }
            foreach (MyObject obj in player.AnimationStand.imgList)
            {
                list.Add(obj);
            }
            Init();
            //화면 전환을 위한 함수. 리스트내에 애니메이션에 들어가는 그림을 다 넣엇음, 그림의 알파값을 전부 0으로 두어서 숨겨둠.
        }
        public override void Unload()
        {
            base.Unload();
        }
        public override void Update(GameTime gt)
        {
            //count++;
            player.Update(gt);
            base.Update(gt);
        }
        public override void Draw(SpriteBatch sp, GameTime gt)
        {


            player.Draw(sp, gt);
            base.Draw(sp, gt);
        }
        public void CheckTile(List<TileInfo> tileList)
        {
            player.CheckTile(tileList);
            //플레이어가 타일 아래로 내려가지 않게함.
        }


    }
}
