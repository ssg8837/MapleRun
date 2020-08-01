using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapleRun0509.Library;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MapleRun0509.Item
{
    public class Items : MyObject
    {
        public int type;//아이템의 타입 0,1,2,3=>메소,4=>포션
        Animation AnimationSpin;//애니메이션
        int speed;//애니메이션의 속도


        public Items(int type)
        {
            AnimationSpin = new Animation();//애니메이션 생성
            this.type = type;//타입 입력
            this.speed = 130;//애니메이션 회전속도 입력
        }
        public virtual void Load()
        {
            if (type == 4)//포션일때
            {
                AnimationSpin.Load(PathManager.Potion);//포션 폴더에 잇는 이미지를 애니메이션화
            }
            else
            {

                AnimationSpin.Load(PathManager.MesoAnimation[type]);//동전 폴더에 잇는 이미지를 애니메이션화
            }
            AnimationSpin.Speed = speed;//애니메이션 속도를 speed값 대입
        }
        public virtual void Draw(SpriteBatch sp, GameTime gt)
        {
            AnimationSpin.Draw(sp, gt, pos);//pos위치에 그림 그림.
        }
        public virtual void Update(GameTime gt)
        {
        }

        public Point LoadSize()//애니메이션 사이즈 반납
        {
            return AnimationSpin.size;
        }
    }
}
