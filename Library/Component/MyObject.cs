using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleRun0509.Library
{
    // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    public class MyObject
    {
        /*
         *  MyObject는 화면에 추가되는 요소들이 공통적으로 사용하는 변수들,메서드들을 모아둔 클래스이다.
         *  이미지나 버튼, 플레이어 등등 MyObject를 상속받아서 사용한다.
         *  통일된 타입을 통해서 다형성을 이용하기 위한 목적.
         * 
         */
        #region 변수
        protected Texture2D texture;
        protected Rectangle rect;
        public Vector2 pos;
        protected Vector2 scale;
        protected Vector2 origin;

        public float alpha;

        protected bool isActive;
        protected bool isVisible;
        protected bool isFadeInOut;
        #endregion

        #region 변수들에 대한 프로퍼티
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }
        public float X
        {
            get { return pos.X; }
            set { pos.X = value; }
        }
        public float Y
        {
            get { return pos.Y; }
            set { pos.Y = value; }
        }
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public bool IsFadeInOut
        {
            get { return isFadeInOut; }
            set { isFadeInOut = value; }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 생성자
        public MyObject()
        {
            pos = Vector2.Zero;
            scale = Vector2.One;
            origin = Vector2.One;
            alpha = 1.0f;
            isFadeInOut = false;
            isVisible = true;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region FadeIn, FadeOut, FadeInOut
            /*
             *  static 클래스인 AnimationManager의 static 메서드들을 이용해서
             *  MyObject에 FadeIn,FadeOut,FadeInOut 효과를 주는 메서드
             */
        public void FadeOut(GameTime gt, float speed = 1.0f)
        {
            AnimationManager.FadeOut(gt, ref alpha, ref isFadeInOut, speed);
        }
        public void FadeIn(GameTime gt, float speed = 1.0f)
        {
            AnimationManager.FadeIn(gt, ref alpha, ref isFadeInOut, speed);
        }
        public void FadeInOut(GameTime gt, float speed = 1.0f)
        {
            AnimationManager.FadeInOut(gt, ref alpha, ref isFadeInOut, speed);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
