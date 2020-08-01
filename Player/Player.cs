using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MapleRun0509.Library;
using Microsoft.Xna.Framework.Input;
using MapleRun0509.Map;
namespace MapleRun0509
{
    public class Player : MyObject //플레이어는 myojbect를 상속 받아서 만듬
    {
        public Image img;

        public Animation AnimationMove; //이동용 애니메이션
        public Animation AnimationStand; //정지용 애니메이션 , 디버그용 변수, 디버그시 키보드로 움직이기 때문.

        int life = 10;
        int meso = 0;
        public Vector2 velocity;//연산을 마치고 실제 플레이어가 움직이는 속도
        public float speed;//앞으로 움직이는 속도
        public float jumpSpeed;//점프하는 속도
        bool isStand;//정지판단, 디버그용
        public bool hasJumped;//점프했는가? 점프 가능한지 여부 판단
        int count = 0;//더블 점프용 점프 횟수 카운트
        SpriteEffects effect;
        //public List<MyObject> list;

        public int Life
        {
            get { return life; }
            set { life = value; }
        }
        public int Meso
        {
            get { return meso; }
            set { meso = value; }
        }
        public void Load(string path)
        {
            velocity = Vector2.Zero;
            img = new Image(path);
            img.Load();

            AnimationMove = new Animation();
            AnimationMove.Load(PathManager.Player_Move);

            AnimationStand = new Animation();
            AnimationStand.Load(PathManager.Player_Stand);

            hasJumped = true;
            isStand = true;
            speed = 1;
            jumpSpeed = -7f;
            effect = SpriteEffects.FlipHorizontally;//이미지 좌우반전, 이미지가 왼쪽을 바라보는게 기본이라 반전 후 시작.
        }
        public void Unload()
        {
            img.Unload();
        }

        public void Update(GameTime gt)
        {
            img.Update(gt);
            isStand = true;
            if (velocity.X > 0)
            {
                effect = SpriteEffects.FlipHorizontally;
                isStand = false;
            }
            else if (velocity.X < 0)
            {
                effect = SpriteEffects.None;
                isStand = false;
            }
            if (InputManager.Instance.isKeyDown(Keys.Right))
            {
                // velocity.X = (float)gt.ElapsedGameTime.TotalSeconds*60 * speed;
            }
            else if (InputManager.Instance.isKeyDown(Keys.Left))
            {
                // velocity.X = -(float)gt.ElapsedGameTime.TotalSeconds*60 * speed;
            }
            else
            {
                //velocity.X = 0;
            }
            speed = (int)(img.X / (500 * speed)) + 3;//오른쪽으로 움직이는 속도
            if (speed >= 10)
                speed = 10;//속도 제한
            jumpSpeed = -(speed / 3 + 5f);//점프 스피드를 오른쪽으로 움직이는 속도에 비례하게 함.
            velocity.X = (float)gt.ElapsedGameTime.TotalSeconds * 60 * speed;//speed와 실행한 시간에 비례해서 x값 이동
            if (InputManager.Instance.isKeyPress(Keys.Space))
            {

                /*
                #region 헤엄
                
                if (true)
                {
                    count++;
                    hasJumped = true;
                    velocity.Y = -5f;
                    img.Y -= 2 * (float)gt.ElapsedGameTime.TotalSeconds * 60;                    
                }
                #endregion
                */

                #region 2단 점프
                if (hasJumped == false)
                {
                    count = 1;
                    hasJumped = true;
                    velocity.Y = jumpSpeed;
                    img.Y -= jumpSpeed * (float)gt.ElapsedGameTime.TotalSeconds * 60;

                }
                if (count == 2)
                {
                    velocity.Y = jumpSpeed;
                    img.Y -= jumpSpeed * (float)gt.ElapsedGameTime.TotalSeconds * 60;
                    count = 0;
                }
                #endregion

            }
            if (InputManager.Instance.isKeyUp(Keys.Space))
            {
                if (count == 1)
                {
                    count = 2;
                }
            }

            if (hasJumped)
            {
                float i = 1;
                velocity.Y += 0.2f * i;
            }
            if (img.Y + img.Rect.Height == SettingManager.WINDOW_HEIGHT)
            {
                hasJumped = false;
                velocity.Y = 0;
                count = 0;
            }
            img.Position += velocity;

            if (img.Y > SettingManager.WINDOW_HEIGHT - img.Rect.Height)
            {
                img.Y = SettingManager.WINDOW_HEIGHT - img.Rect.Height;
            }
            CameraManager.Instance.SetFocus(new Vector2(img.X, SettingManager.WINDOW_HEIGHT / 2));

        }

        public void Draw(SpriteBatch sp, GameTime gt)
        {
            if (isStand)
            {
                AnimationStand.Draw(sp, gt, img.Position, effect);
            }
            else
            {
                AnimationMove.Draw(sp, gt, img.Position, effect);
            }
        }
        public void CheckTile(List<TileInfo> list)
        {
            int y = (int)img.Y;
            int x = (int)img.X + img.Rect.Width / 2;
            foreach (TileInfo tile in list)
            {
                if ((tile.pos.X <= x && x < tile.pos.X + tile.size.X))//플레이어가 무슨 타일위에 올라가 잇는지 찾음.
                {
                    if ((y + img.Rect.Height - tile.GetGround(x)) >= 0)//타일위에 플레이어가 올라감. 
                    {
                        img.Y = tile.GetGround(x) - img.Rect.Height;
                        hasJumped = false;
                    }
                    else
                    {
                        hasJumped = true;
                    }
                }
            }
        }

        public void modifyLife(int lifeValue)//라이프 변경
        {
            life += lifeValue;
        }
        public void modifyMeso(int mesoValue)//메소 변경
        {
            meso += mesoValue;
        }
    }
}
