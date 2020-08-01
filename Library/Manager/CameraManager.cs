using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;  

namespace MapleRun0509.Library
{
    public class CameraManager
    {
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 싱글톤 인스턴스
        private static CameraManager instance;
        public static CameraManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CameraManager();                    
                }
                return instance;
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        Vector2 pos;
        public Matrix mx;
        public void Update()
        {
            mx = Matrix.CreateTranslation(new Vector3(-pos, 0));
        }
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region SetFocus
            /*
             * Player의 위치값을 받아서 사용한다.
             * Player의 위치를 계산해서 일정 값을 넘어가면 카메라를 Player에 고정시킨다.
             */
        public void SetFocus(Vector2 pos)
        {            
            this.pos = new Vector2(pos.X - SettingManager.WINDOW_WIDTH / 5,pos.Y - SettingManager.WINDOW_HEIGHT/2);
            if(this.pos.X < 0)
            {
                this.pos.X = 0;
            }
            if(this.pos.Y < 0)
            {
                this.pos.Y = 0;
            }
        }
        #endregion  

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
