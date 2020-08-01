using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MapleRun0509.Library
{
    // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    public class Animation : MyObject
    {
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 변수
        public List<Image> imgList = new List<Image>();        // 이미지들의 리스트로 애니메이션이 이루어 진다.
        int speed;                                             // 애니메이션의 속도
        int count;                                             // 이미지 개수
        public Point size;                                     // 이미지의 크기
        #endregion

        #region 변수들에 대한 프로퍼티
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int Width
        {
            get { return size.X; }
        }
        public int Height
        {
            get { return size.Y; }
        }
        public int Count
        {
            get { return count; }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 생성자
        public Animation()
        {
            speed = SettingManager.AnimationSpeed;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public void Load(string dirName)
        {
            string[] fileNames = PathManager.GetFilesNames(dirName);            // 디렉토리 안에 있는 모든 파일의 파일명을 가져옴
            this.count = fileNames.Length;
            for (int i = 0; i < count; i++)
            {
                Image img = new Image(PathManager.GetPureName(fileNames[i]));   // 해당 파일로부터 Image를 생성한다.
                img.Load();
                imgList.Add(img);                                               // 이미지 리스트에 이미지를 추가
            }
            size.X = imgList[0].Rect.Width;
            size.Y = imgList[0].Rect.Height;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Unload
        public void Unload()
        {
            foreach(Image img in imgList)
            {
                img.Unload();
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Draw
        public void Draw(SpriteBatch sp, GameTime gt, Vector2 pos, SpriteEffects effects = SpriteEffects.None)
        {
            // GameTime으로부터 Miliseconds를 받아오고, 이를 speed로 나눈뒤, count값으로 나눈 나머지 값을 index에 저장
            // speed로 나눈 이유는, 애니메이션의 속도를 위해서
            // count로 나눈 나머지 값을 저장하는 이유는, 이미지 리스트의 배열의 인덱스로 사용하기 위해
            int index = (int)(gt.TotalGameTime.TotalMilliseconds / speed) % count; 
            imgList[index].Draw(sp, pos, effects);            
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public void Update(GameTime gt)
        {
            foreach (Image img in imgList)
            {
                img.Alpha = Alpha;
                img.Update(gt);
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region FadeOut / FadeIn / FadeInOut
        /* 애니메이션을 FadeIn,FadeOut,FadeInOut하는 효과를 주는 함수이다.
         * static 클래스 AnimationManager의 static 메서드를 이용하여 함수를 수행한다.
         * 이미지 리스트에 있는 모든 이미지들에 효과를 준다.     
         * 
         * MyObject의 FadeIn,FadeOut,FadeInOut메서드를 새로 정의하기 때문에 
         * new 키워드를 이용.
         */
            
        public new void FadeOut(GameTime gt, float speed)
        {
            foreach(Image img in imgList)
            {
                AnimationManager.FadeOut(gt, ref img.alpha, ref isFadeInOut, speed);
            }            
        }
        public new void FadeIn(GameTime gt, float speed)
        {
            AnimationManager.FadeIn(gt, ref alpha, ref isFadeInOut, speed);
        }
        public new void FadeInOut(GameTime gt, float speed = 0.3f)
        {
            AnimationManager.FadeInOut(gt, ref alpha, ref isFadeInOut, speed);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
