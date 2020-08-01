using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapleRun0509.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapleRun0509.Library
{
    public class MapScreen : GameScreen
    {
        #region 변수
        int temp;
        int startHeight = 360;
        Random rand = new Random();
        Background background;
        Tile tile_LtoR;     // LtoR 타일
        Tile tile_RtoL;     // RtoL 타일
        Tile tile_Floor;    // 바닥 타일
        Tile tile_Wall;     // 벽 타일
        public List<TileInfo> tileList; // 타일 정보들에 대한 List.
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public override void Load()
        {
            background = new Background();
            background.Load();

            tileList = new List<TileInfo>();

            tile_Floor = new Tile(PathManager.Tile_Type[0], EnumManager.TileType.FLOOR);
            tile_LtoR = new Tile(PathManager.Tile_Type[0], EnumManager.TileType.LtoR);
            tile_RtoL = new Tile(PathManager.Tile_Type[0], EnumManager.TileType.RtoL);
            tile_Wall = new Tile(PathManager.Tile_Type[0], EnumManager.TileType.Wall);

            tile_Floor.Load();
            tile_LtoR.Load();
            tile_RtoL.Load();
            tile_Wall.Load();

            // 타일을 생성한다
            CreateTile(new int[,] {                                                                           // Map map의 Generate를 통해 List<CollisionTiles> 를 생성한다.
               { 0,0,0,0,0,0,0,0,0,1,1,0,}, });

            base.Load();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Draw
        public override void Draw(SpriteBatch sp, GameTime gt)
        {
            background.Draw(sp, gt);

            foreach (TileInfo info in tileList)
            {
                if (info.isVisible)
                {
                    // 타일의 밑으로 벽을 그려준다.
                    for (int i = 8; i > info.pos.Y / 60; i--)
                    {
                        sp.Draw(tile_Wall.img.Texture, new Vector2(info.pos.X, i * 60 - 42));
                    }
                    switch (info.type)
                    {
                        case EnumManager.TileType.FLOOR:
                            tile_Floor.Draw(sp, info);
                            break;

                        case EnumManager.TileType.LtoR:
                            tile_LtoR.Draw(sp, info);
                            break;

                        case EnumManager.TileType.RtoL:
                            tile_RtoL.Draw(sp, info);
                            break;
                    }
                }
            }
            base.Draw(sp, gt);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public void Update(GameTime gt, Vector2 pos)
        {
            background.Update(gt, pos);
            base.Update(gt);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region CreateTile
        public void CreateTile(int[,] map)
        {
            /*
             * 타일을 생성하는 함수
             * 2차원 배열을 선언한 이유는, 맵의 빈 공간에도 타일이 생성되게 하려고 했었기 때문..
             * 실제로 타일을 생성하는 것이 아니라, 타일에 대한 정보만 생성하고
             * TileInfo 리스트에 해당 내용을 저장한다.
             * 이렇게 하면, 중복되는 타일의 내용을 생성할 필요도 없기 때문에 리소스 관리 면에서 효율적이다.
             */
            EnumManager.TileType type;
            for (int y = map.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    type = (EnumManager.TileType)map[y, x];
                    if (x == 0 && y == map.GetLength(0) - 1)
                    {
                        TileInfo info = new TileInfo(type);
                        info.pos = new Point(0, startHeight);
                        info.size = new Point(tile_Floor.img.Rect.Width, tile_Floor.img.Rect.Height);
                        tileList.Add(info);
                    }
                    else
                    {
                        Add(type, tileList.Last());
                    }
                }
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Add
        public void Add(EnumManager.TileType type, TileInfo last)
        {
            /*
             * 타일들에 대한 정보를 저장해둔 TileList의
             * 마지막 타일로부터,
             * 새로 생성할 타일의 위치를 지정해줄 수 있다.
             * 또한, 타일을 새로 생성할 때, 일정 조건에 따라 생성 제한을 둔다.
             * 그 이유는, 계속 올라가는 타일만 생성되거나, 계속 내려가는 타일을 생성할 경우
             * 화면을 넘어갈 수 있기 때문이다.
             */
            Point LastSize = last.size;
            int x = last.pos.X + LastSize.X;
            EnumManager.TileType LastType = last.type;

            TileInfo info = new TileInfo(type);
            info.pos = new Point(x, last.pos.Y);
            info.size = Point.Zero;

            switch (type)
            {
                case EnumManager.TileType.FLOOR:
                    info.size.Y = tile_Floor.img.Rect.Height;
                    if (LastType == EnumManager.TileType.LtoR)
                    {
                        info.pos.Y -= 60;
                    }
                    break;

                case EnumManager.TileType.LtoR:
                    info.size.Y = tile_LtoR.img.Rect.Height;
                    if (LastType == EnumManager.TileType.LtoR)
                    {
                        info.pos.Y -= 60;
                    }
                    break;

                case EnumManager.TileType.RtoL:
                    info.size.Y = tile_RtoL.img.Rect.Height;
                    if (LastType == EnumManager.TileType.FLOOR || LastType == EnumManager.TileType.RtoL)
                    {
                        info.pos.Y += 60;
                    }
                    break;
            }
            info.size = new Point(LastSize.X, info.size.Y);
            tileList.Add(info);
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region CheckPlayer
        public void CheckPlayer(Rectangle champ)
        {
            /*
             * 플레이어의 위치와 타일의 위치를 계산해서,
             * 타일이 화면을 벗어났다고 판정이 되면,
             * 해당 파일을 삭제한 뒤, 새로운 타일을 생성하는 함수이다.
             */
            for (int i = 0; i < tileList.Count(); i++)
            {
                if (champ.X - tileList.ElementAt(i).pos.X >= tileList.ElementAt(i).size.X * 3)
                {
                    tileList.ElementAt(i).isVisible = false;

                    if (tileList.Last().pos.Y >= startHeight - 20)
                    {
                        temp = rand.Next(0, 2);
                    }
                    else if (tileList.Last().pos.Y <= startHeight)
                    {
                        temp = rand.Next(-1, 3);
                        if (temp == -1)
                            temp = 0;
                        if (temp == 1)
                            temp = 2;
                    }
                    else
                    {
                        temp = rand.Next(-3, 3);
                        if (temp < 0)
                            temp = 0;
                    }
                    Add((EnumManager.TileType)temp, tileList.Last());
                    tileList.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
