using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleRun0509.Library
{
    public class Number : MyObject
    {
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 변수
        public string dirName;              // 숫자 이미지들의 폴더의 경로
        public int type;                    // 숫자의 종류
        public Image[] img;                 // 숫자 이미지들을 저장하는 img 배열
        public int[] number;                // Draw하려는 숫자값을 배열형태로 저장
        public int[] width;                 // 숫자 이미지의 넓이 값을 저장하는 배열.(why? Draw할 때 숫자끼리 서로 안 곂치도록 하기 위해서)
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 생성자
        public Number(string path)
        {
            this.dirName = path;
            img = new Image[10];
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public void Load()
        {
            for (int i = 0; i < 10; i++)
            {
                img[i] = new Image(PathManager.GetPureName(PathManager.GetFilesNames(this.dirName)[i]));
                img[i].Load();
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Unload
        public void Unload()
        {
            foreach (Image i in img)
            {
                i.Unload();
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Draw
        public void Draw(SpriteBatch sp, int number, Vector2 pos, EnumManager.Direction where = EnumManager.Direction.LEFT)
        {
            // Draw하려는 숫자 number를 int[] 로 변환하는 과정
            this.number = Array.ConvertAll(number.ToString().ToArray(), x => x - 48);

            if (number >= 0)
            {
                // 숫자 이미지마다 넒이가 다르기 때문에, 각 이미지의 넓이를 저장해놔서, Draw할 때 곂치지 않도록 해준다.
                width = new int[this.number.Length];
                for (int i = 1; i < width.Length; i++)
                {
                    width[i] = width[i - 1] + img[this.number[i]].Rect.Width;
                }

                switch (where)
                {
                    // 왼쪽 정렬
                    case EnumManager.Direction.LEFT:

                        for (int i = 0; i < this.number.Length; i++)
                        {
                            img[this.number[i]].Draw(sp, new Vector2(pos.X + width[i], pos.Y));
                        }
                        break;

                    // 오른쪽 정렬
                    case EnumManager.Direction.RIGHT:

                        for (int i = 0; i < this.number.Length; i++)
                        {
                            img[this.number[i]].Draw(sp, new Vector2(pos.X - width[width.Length - 1 - i] - img[this.number[i]].Rect.Width, pos.Y));
                        }
                        break;
                }
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        public void Update(GameTime gt)
        {

        }
    }
}
