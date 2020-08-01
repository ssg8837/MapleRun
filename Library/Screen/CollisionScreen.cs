
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

namespace MapleRun0509.Library.Screen
{
    class CollisionScreen : GameScreen
    {
        bool poweroverwhelming = false;//무적인지 아닌지 판정.
        List<MONSTER> listMonster;//몬스터 리스트-0525

        Image aya;//맞앗을땨 빨간색으로 표시

        TimeSpan collisionTime;

        public override void Load()
        {
            aya = new Image(PathManager.Aya);
            aya.alpha = 0f;
            aya.Load();
            aya.X = 0;
            aya.Y = 0;
            base.Load();
        }
        public override void Draw(SpriteBatch sp, GameTime gt)
        {
            aya.Draw(sp);
            base.Draw(sp, gt);
        }

        public TimeSpan Time
        {
            get { return collisionTime; }
            set { collisionTime = value; }
        }

        private bool collision(GameTime gt, Point size, float x, float y, Player player, bool item) //부딪혓는지 판단.
        {
            float Step = 0;
            Vector2 playerVectorStart = new Vector2(player.img.X, player.img.Y);//포지션 받기
            Rectangle playerRect = player.img.Rect;//렉텡클(크기 재려고) 받아옴
            float[] playerLR = new float[2];
            //0일경우 왼쪽 좌표, 1일 경우 오른쪽 좌표
            if (item)
            {
                //아이템일 경우 플레이어 판정 후하게
                playerLR[0] = playerVectorStart.X - playerRect.Width * 0.5f;
                playerLR[1] = playerVectorStart.X + playerRect.Width * 1.5f;
            }
            else
            {
                //아닐경우 정직하게
                playerLR[0] = playerVectorStart.X + playerRect.Width;
                playerLR[1] = playerVectorStart.X + playerRect.Width * 1f;
            }

            float[] playerUD = new float[2];
            //0일경우 위 좌표, 1일 경우 아래 좌표
            if (item)
            {
                playerUD[0] = playerVectorStart.Y;
                playerUD[1] = playerVectorStart.Y + playerRect.Height;
            }
            else
            {
                playerUD[0] = playerVectorStart.Y;
                playerUD[1] = playerVectorStart.Y + playerRect.Height * 0.2f;//충돌 판정을 위한 아랫값. 20%보다 더 가면 밟기로 판정
                Step = playerVectorStart.Y + playerRect.Height;//실제 이미지의 아랫값
            }

            float[] objectLR = new float[2];
            if (item)
            {
                objectLR[0] = x - size.X;
                objectLR[1] = x + size.X * 2.2f;

            }
            else
            {
                objectLR[0] = x;
                objectLR[1] = x + size.X * 1.2f;
            }

            float[] objectUD = new float[2];
            if (item)
            {

                objectUD[0] = y - size.Y * 0.3f;
                objectUD[1] = y + size.Y * 1.5f;
            }
            else
            {
                objectUD[0] = y;
                objectUD[1] = y + size.Y;
            }

            if ((playerLR[0] <= objectLR[0] && objectLR[0] <= playerLR[1]) || (objectLR[0] <= playerLR[1] && playerLR[1] <= objectLR[1]))
            {
                if (!item && playerUD[1] <= objectUD[0] && objectUD[0] <= Step)
                {
                    player.velocity.Y = -5f;
                    player.hasJumped = false;
                    return false;
                }
                else if ((playerUD[0] <= objectUD[0] && objectUD[0] <= playerUD[1]) || (objectUD[0] <= playerUD[1] && playerUD[1] <= objectUD[1]))
                {
                    return true;
                }

            }
            return false;
        }

        public void collisionWithMonster(GameTime gt, Player player, List<MONSTER> Monster)//몬스터와 충돌햇는지 판단-0525
        {
            int countCol = 0;
            aya.Alpha = 0f;

            listMonster = Monster;
            foreach (MONSTER mon in listMonster)
            {
                Point monSize = mon.AnimationMove.size;
                if (collision(gt, monSize, mon.pos.X, mon.pos.Y, player, false))
                    countCol++;
                if (countCol != 0)
                {
                    aya.Alpha = 0.4f;//알파값만 바꿈
                    player.modifyLife(-1);//생명력 감소
                    poweroverwhelming = true;//무적
                    collisionTime = gt.TotalGameTime;//충돌한 시점의 시간 저장
                    break;
                }

            }

        }

        public void collisionWithItems(GameTime gt, Player player, List<Items> itemList)
        {
            for (int i = 0; i < itemList.Count(); i++)
            {
                Items item = itemList.ElementAt(i);

                Point itemSize = item.LoadSize();
                if (collision(gt, itemSize, item.pos.X, item.pos.Y, player, true))
                {
                    if (item.type == 4)
                    {
                        player.modifyLife(1);
                    }
                    else
                    {
                        player.modifyMeso(50 * (i + 1));
                    }
                    itemList.RemoveAt(i);
                    i--;
                }
            }



        }


        public bool invincibility
        {
            get { return poweroverwhelming; }
            set { poweroverwhelming = value; }
        }

    }
}
