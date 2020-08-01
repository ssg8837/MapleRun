using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleRun0509.Library
{
    public static class AnimationManager
    {
        /*
         * static 클래스
         * FadeIn,FadeOut,FadeInOut 메서드에 ref 변수를 받아서, 해당 변수의 값을 조작
         * MyObject 타입의 객체가 해당 메서드를 호출하면, 페이드 인이나 페이드 아웃 되는 효과를 줄 수 있다.
         */
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region FadeIn
        public static void FadeIn(GameTime gt, ref float alpha, ref bool isFadeInOut, float speed = 0.3f)
        {
            if (isFadeInOut == false)
            {
                alpha += speed * (float)gt.ElapsedGameTime.TotalSeconds;
            }
            if (alpha > 1.0f)
            {
                isFadeInOut = false;
                alpha = 1.0f;
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region FadeOut
        public static void FadeOut(GameTime gt, ref float alpha, ref bool isFadeInOut, float speed = 0.3f)
        {
            if (isFadeInOut == false)
            {
                alpha -= speed * (float)gt.ElapsedGameTime.TotalSeconds;
            }
            if (alpha < 0.0f)
            {
                isFadeInOut = true;
                alpha = 0.0f;
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region FadeInOut
        public static void FadeInOut(GameTime gt, ref float alpha, ref bool isFadeInOut, float speed = 0.3f)
        {
            if (isFadeInOut == false)
            {
                alpha -= speed * (float)gt.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                alpha += speed * (float)gt.ElapsedGameTime.TotalSeconds;
            }
            if (alpha < 0.0f)
            {
                isFadeInOut = true;
                alpha = 0.0f;
            }
            else if (alpha > 1.0f)
            {
                isFadeInOut = false;
                alpha = 1.0f;
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
