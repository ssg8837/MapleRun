using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleRun0509.Library
{
    public abstract class GameScreen
    {
        // ScreenManager에서 이미 언급했듯이 콘텐츠를 관리하기 위해서 GameScreen만의 ContentManager를 선언.
        // ScreenManager의 Content를 공유하는 ContentManager를 생성
        #region 변수
        protected ContentManager Content;
        public List<MyObject> list; // 현재 Screen에 있는 모든 MyObject 타입의 객체들을 저장하는 list        
        public bool isEnd;          // 현재 Screen이 FadeOut이 다 되었는지
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public virtual void Load()
        {
            //  ScreenManager의 Content를 공유하는 ContentManager를 생성
            this.Content = new ContentManager(ScreenManager.Instace.Content.ServiceProvider, "Content");
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Unload
        public virtual void Unload()
        {
            // 콘텐츠가 더 이상 필요없으면 Unload시킨다.
            Content.Unload();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public virtual void Update(GameTime gt)
        {
            InputManager.Instance.Update();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Draw
        public virtual void Draw(SpriteBatch sp, GameTime gt)
        {
            /*
             *  GameScreen을 상속받는 각각의 Screen에서 Draw를 override해서 사용한다.
             */
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Init
        /*
         * GameScreen 안에 있는 모든 MyObject들의 Alpha값을 0.0f로 설정한다.
         */
        public virtual void Init()
        {
            foreach(MyObject obj in list)
            {
                obj.Alpha = 0.0f;
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region FadeIn,FadeOut,FadeInOut
            /*
             * GameScreen 내에 있는 MyObject 타입의 객체들에 대해서
             * FadeIn, FadeOut, FadeInOut효과를 주는 메서드를 수행하는 부분
             */
        public virtual bool FadeOut(GameTime gt, float speed = 1.0f)
        {
            
            foreach (MyObject obj in list)
            {
                obj.FadeOut(gt, speed);
            }
            if (list.First().Alpha <= 0.0f)
            {
                return true;
            }
            return false;
        }
        public virtual bool FadeIn(GameTime gt, float speed = 1.0f)
        {
            foreach (MyObject obj in list)
            {
                obj.FadeIn(gt, speed);
            }
            if(list.Last().Alpha >= 1.0f)
            {
                isEnd = true;
                return true;
            }
            return false;
        }
        public virtual bool FadeInOut(GameTime gt, float speed = 1.0f)
        {
            foreach (MyObject obj in list)
            {
                obj.FadeInOut(gt, speed);
            }
            return false;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
