using MapleRun0509.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapleRun0509.Map
{
    public class Tile : MyObject
    {
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 변수
        string path;
        public Image img;
        public EnumManager.TileType type;
        #endregion
        
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 생성자
        public Tile(string path,EnumManager.TileType type)
        {
            string[] dirs = PathManager.GetDirsNames(path); // 타일 이미지들이 저장된 폴더 안에 있는 폴더명들을 받아옴
            switch (type)
            {
                case EnumManager.TileType.FLOOR:
                    this.path = PathManager.GetPureName(PathManager.GetFileName(dirs[0])); 
                    break;

                case EnumManager.TileType.LtoR:
                    this.path = PathManager.GetPureName(PathManager.GetFileName(dirs[1]));
                    break;

                case EnumManager.TileType.RtoL:
                    this.path = PathManager.GetPureName(PathManager.GetFileName(dirs[2]));
                    break;

                case EnumManager.TileType.Wall:
                    this.path = PathManager.GetPureName(PathManager.GetFileName(dirs[3]));
                    break;
            }
            this.type = type;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public void Load()
        {
            img = new Image(path);
            img.Load();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Draw
        public void Draw(SpriteBatch sp, TileInfo info)
        {
            img.Draw(sp, new Rectangle(info.pos.X, info.pos.Y + info.y_sub, info.size.X, info.size.Y));
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public void Update(GameTime gt)
        {
            img.Update(gt);
        }
        #endregion
    }
}
