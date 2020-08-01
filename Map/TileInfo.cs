using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapleRun0509.Library;
using Microsoft.Xna.Framework;

namespace MapleRun0509.Map
{
    public class TileInfo
    {
        /*
         * 타일에 대한 정보를 저장하는 클래스
         * 타일의 종류와 타일의 위치,크기를 저장.
         * Y_sub는, 타일이 Floor일 때와 LtoR일 때와 RtoL일 때
         * 각 이미지 간의 높이 차로 인해서 자연스럽게 연결이 되지 않기 때문에
         * 조정을 해주는 변수이다.
         */

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 변수
        public bool isVisible;
        public int y_sub;
        public Point pos;
        public Point size; // x = width, y = height
        public EnumManager.TileType type;
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 생성자
        public TileInfo(EnumManager.TileType type)
        {
            switch (type)
            {
                /*
                 * Y_sub는, 타일이 Floor일 때와 LtoR일 때와 RtoL일 때
                 * 각 이미지 간의 높이 차로 인해서 자연스럽게 연결이 되지 않기 때문에
                 * 조정을 해주는 변수이다.
                 * 
                 */
                case EnumManager.TileType.FLOOR:
                    y_sub = 0;
                    break;

                case EnumManager.TileType.LtoR:
                    y_sub = -57;
                    break;

                case EnumManager.TileType.RtoL:
                    y_sub = -60;
                    break;

                case EnumManager.TileType.Wall:
                    y_sub = 0;
                    break;
            }
            this.type = type;
            isVisible = true;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region GetGround
        public int GetGround(int x)
        {
            /*
             * 타일 위에서의 땅으로 인정되는 Y값을 return한다.
             * FLOOR일 때는 타일의 y값 + y_SUB를 더해주면 되지만,
             * LtoR이나 RtoL일 때는, 기울기가 있기 때문에
             * 수식을 통해서 땅으로 인정되는 값을 얻어내야 한다.
             */
            switch (type)
            {
                case EnumManager.TileType.FLOOR:
                    return pos.Y + y_sub;

                case EnumManager.TileType.LtoR:
                    return pos.Y - (int)((double)60 / 90 * (x % 90)) + 3;

                case EnumManager.TileType.RtoL:
                    return pos.Y + y_sub + (int)((double)60 / 90 * (x % 90));

                default:
                    return 0;
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
