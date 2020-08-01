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
    public class ResultScreen : GameScreen
    {
        Image resultUi;
        Button btnOk;

        int distance;
        int coin;
        int total;

        Number resultDistance;
        Number resultCoin;
        Number resultTotal;

        bool countStart = true;

        public bool isOkClicked
        {
            get; set;
        }


        public override void Load()
        {
            base.Load();

            resultUi = new Image(PathManager.ResultUi);
            resultUi.Load();
            resultUi.Position = new Vector2(250, 30);

            btnOk = new Button(PathManager.ResultOkBtn);
            btnOk.Load();
            btnOk.Position = new Vector2(360, 290);

            resultDistance = new Number(PathManager.Number_ResultUi);
            resultDistance.Load();
            resultCoin = new Number(PathManager.Number_ResultUi);
            resultCoin.Load();
            resultTotal = new Number(PathManager.Number_ResultUi);
            resultTotal.Load();

            list = new List<MyObject>();
            list.Add(resultUi);
            list.Add(btnOk);

            Init();
        }
        public override void Update(GameTime gt)
        {
            base.Update(gt);

            btnOk.Update(gt);

            if (btnOk.isClicked)
            {
                isOkClicked = true;
            }
            else
            {
                isOkClicked = false;
            }

            if (countStart == true)
            {
                resultDistance.Update(gt);
                resultCoin.Update(gt);
                resultTotal.Update(gt);

                if (distance > 0)
                {
                    if (distance > 1000)
                    {
                        distance -= 100;
                        total += 5000;
                    }
                    else if (distance > 100)
                    {
                        distance -= 10;
                        total += 500;
                    }
                    else
                    {
                        distance -= 1;
                        total += 50;   // 거리 1당 점수는 50점
                    }
                }
                else
                {
                    if (coin > 0)
                    {
                        if (coin > 1000)
                        {
                            coin -= 100;
                            total += 100;
                        }
                        else if (coin > 100)
                        {
                            coin -= 10;
                            total += 10;
                        }
                        else
                        {
                            coin -= 1;
                            total += 1;    // 코인 1당 점수는 1점
                        };
                    }
                    else
                    {
                        countStart = false;
                    }
                }
            }

        }
        public override void Draw(SpriteBatch sp, GameTime gt)
        {
            resultUi.Draw(sp);
            btnOk.Draw(sp);

            resultDistance.Draw(sp, distance, new Vector2(500, 135), EnumManager.Direction.RIGHT);
            resultCoin.Draw(sp, coin, new Vector2(530, 180), EnumManager.Direction.RIGHT);
            resultTotal.Draw(sp, total, new Vector2(530, 230), EnumManager.Direction.RIGHT);

            base.Draw(sp, gt);
        }
        public override void Unload()
        {
            base.Unload();
        }
        public void settingValue(int run, int meso)
        {
            distance = run;
            coin = meso;
        }
    }
}