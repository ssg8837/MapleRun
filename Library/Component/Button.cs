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
    // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *  
    public class Button : MyObject
    {
        #region 변수

        Image imgCurrent;       // 현재 버튼을 저장
        Image imgActive;        // 활성화 상태일 때의 이미지
        Image imgMouseOn;       // 마우스가 올라갔을 때의 이미지
        
        public bool isMouseOn;  // 버튼 위에 마우스가 올라왔는지
        public bool isClicked;  // 버튼을 클릭했는지
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 생성자
        public Button(string path, bool isActive = true, bool isVisible = true)
        {
            imgActive = new Image(PathManager.GetPureName(PathManager.GetFilesNames(path)[0]));
            imgMouseOn = new Image(PathManager.GetPureName(PathManager.GetFilesNames(path)[1]));            
            imgCurrent = imgActive; // 활성화 버튼을 현재 버튼으로 설정
            this.isActive = isActive;
            this.isVisible = isVisible;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public void Load()
        {
            imgActive.Load();
            imgMouseOn.Load();
            pos = Vector2.Zero;
            rect = new Rectangle((int)pos.X, (int)pos.Y, imgActive.Rect.Width, imgActive.Rect.Height);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public void Update(GameTime gt)
        {
            imgCurrent.Alpha = Alpha;
            rect.X = (int)pos.X;
            rect.Y = (int)pos.Y;

            // 버튼의 Rectangle 범위 안으로 현재 마우스 커서가 들어온다면
            if (rect.Intersects(new Rectangle(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, 5, 5)))
            {               
                isMouseOn = true;
                imgCurrent = imgMouseOn;    // 현재 버튼을 마우스가 올라왔을 때의 이미지로 변경

                // 마우스가 올라왔는데 클릭까지 했다면
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                    imgCurrent = imgActive; // 현재 버튼을 활성화 상태 때의 이미지로 변경
                }
            }
            else // 마우스 커서가 버튼을 벗어나면
            {
                isMouseOn = false;  
                imgCurrent = imgActive; // 현재 버튼을 활성화 상태 때로 변경
            }

        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Draw
        public void Draw(SpriteBatch sp)
        {
                imgCurrent.Draw(sp, pos);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
