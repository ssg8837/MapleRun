using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MapleRun0509.Library
{
    public static class SettingManager
    {
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 변수

        #region 화면 관련 
        // 화면 넓이,높이
        public static int WINDOW_WIDTH;
        public static int WINDOW_HEIGHT;

        // 최대,최소 넓이
        public static int MAX_WIDTH;
        public static int MIN_WIDTH;

        // 최대,최소 높이
        public static int MAX_HEIGHT;
        public static int MIN_HEIGHT;
        
        // 화면의 중앙값을 return
        public static int CENTER_WIDTH
        {
            get { return WINDOW_WIDTH / 2; }
        }
        public static int CENTER_HEIGHT
        {
            get { return WINDOW_HEIGHT / 2; }
        }
        #endregion

        #region 애니메이션 관련
        public static readonly int AnimationSpeed = 200;
        #endregion
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 생성자
        // 프로그램 생성할 때 최대,최소 넓이,높이가 정해짐
        static SettingManager()
        {
            MAX_WIDTH = 1280;
            MAX_HEIGHT = 720;

            MIN_WIDTH = 800;
            MIN_HEIGHT = 400;
        }
        #endregion  

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        
        #region Set Window Size  Window의 크기를 설정한다.
        public static void SetWindowSize(GraphicsDeviceManager graphics, int width, int height)
        {
            // width와 height이 유효한 값인지 check
            if (Check(width, height))
            {                
                graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
                graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
                graphics.ApplyChanges();
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 최대 크기로 설정
        public static void SetWindowMAX(GraphicsDeviceManager graphics)
        {
            SetWindowSize(graphics,MAX_WIDTH, MAX_HEIGHT);
        }
        #endregion

        #region 최소 크기로 설정
        public static void SetWindowMIN(GraphicsDeviceManager graphics)
        {
            SetWindowSize(graphics,MIN_WIDTH, MIN_HEIGHT);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region width와 height 유효범위를 check
        static bool Check(int width, int height)
        {
            if ((MIN_WIDTH <= width && width <= MAX_WIDTH) && (MIN_HEIGHT <= height && height <= MAX_WIDTH))
            {
                WINDOW_WIDTH = width;
                WINDOW_HEIGHT = height;
                return true;
            }
            WINDOW_WIDTH = MIN_WIDTH;
            WINDOW_HEIGHT = MIN_HEIGHT;
            return false;
        }
        #endregion  
    }
}
