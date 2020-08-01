using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleRun0509.Library
{
    public class InputManager
    {
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 변수
        KeyboardState prevState;    // 이전에 눌린 키보드 상태를 저장하는 prevState
        KeyboardState nowState;    // 현재 눌린 키보드 상태를 저장하는 nextState

        // 싱글톤패턴 적용해서, InputManager는 오직 하나만 존재하도록 설정
        private static InputManager instance;
        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public void Update()
        {
            prevState = nowState;

            nowState = Keyboard.GetState();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region isKeyDown key를 누르고 있는 동안 계속 해야되는 일이 있을 때 사용 ( ex) 케릭터 움직이기 )
        public bool isKeyDown(Keys key)
        {
            if (nowState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region isKeyPress key를 눌렀을 때 딱 한번만 인정해줄 때 사용              ( ex) 정보창 끄고 키고 )
        public bool isKeyPress(Keys key)
        {
            if (prevState.IsKeyUp(key) && nowState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region isKeyRelease
        public bool isKeyRelease(Keys key)
        {
            if (prevState.IsKeyDown(key) && nowState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 


        #region isKeyUp key를땔때 사용 ( ex) 케릭터 움직이기 )
        public bool isKeyUp(Keys key)
        {
            if (nowState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }
        #endregion

    }
}
