
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleRun0509.Library
{
    public static class PathManager
    {

        static readonly string has = "\\";
        public static readonly string Root;

        public static readonly string Content;
        public static readonly string Map;
        public static readonly string Map_Background;
        public static readonly string Tile;
        public static readonly string[] Tile_Type;

        public static readonly string Mob;
        public static readonly string Effect;
        public static readonly string Number;

        public static readonly string Menu;

        public static readonly string Player = "0";
        public static readonly string PlayerDir;
        public static readonly string Player_Move;
        public static readonly string Player_Stand;
        public static readonly string Meso;
        public static readonly string[] MonsterAnimation;
        public static readonly string[] MesoAnimation;
        
        public static readonly string Btn;
        public static readonly string BtnStart;
        public static readonly string BtnCredit;
        public static readonly string BtnExit;
        public static readonly string[] Count;

        public static readonly string GameUi;
        public static readonly string Number_GameUi;
        public static readonly string Number_GameRun;

        public static readonly string ResultUi;
        public static readonly string ResultBtn;
        public static readonly string ResultOkBtn;
        public static readonly string Number_ResultUi;

        public static readonly string Aya;
        public static readonly string Potion;


        static PathManager()
        {
            Root = Directory.GetCurrentDirectory();
            Content = Root + has + "Content";
            Map = Content + has + "Map";
            Map_Background = GetPureName(GetFileName(Map));
            Mob = Content + has + "Monster";
            Effect = Content + has + "Effect";
            Number = Content + has + "Number";
            PlayerDir = Content + has + "Player\\작은 노란 강아지";
            Player_Move = PlayerDir + has + "move";
            Player_Stand = PlayerDir + has + "stand";
            Meso = Content + has + "Meso";
            Aya = Content + has + "Aya";
            Potion = Content + has + "potion";

            Tile = Map + has + "Tile";
            Tile_Type = GetDirsNames(Tile);

            MonsterAnimation = GetDirsNames(Mob);
            MesoAnimation = GetDirsNames(Meso);

            Menu = Content +  has +"Menu";
            Btn = Menu + has + "Button";
            BtnCredit = GetDirsNames(Btn)[0];
            BtnExit= GetDirsNames(Btn)[1];
            BtnStart= GetDirsNames(Btn)[2];

            Count = GetFilesNames(Content + has + "Count");

            GameUi = "gameUI";
            Number_GameUi = Content + has + "Number_GameUi";
            Number_GameRun = Content + has + "Number_GameRun";

            ResultUi = "resultUI";
            ResultBtn = Menu + has + "Button_Result";
            ResultOkBtn = GetDirsNames(ResultBtn)[0];
            Number_ResultUi = Content + has + "Number_Result";

        }

        // path의 순수한 이름을 return 
        // Content.Load<>할 때 완벽한 절대경로가 아닌, 상대경로만 필요로 한다.
        // 예를 들면 GetFileName이나 GetDirName같은 함수들은 다음과 같은 완전한 경로를 반환한다
        // C:\Users\senso\Documents\Visual Studio 2015\Projects\Game2\Game2\bin\Windows\x86\Debug\Content\Number
        // 여기서 우리는
        // Number 폴더만 있으면 
        // Content.Load<>("Number")  이런 식으로 작업을 할 수 있다.    
        public static string GetPureName(string path)
        {
            return path.Substring(Content.Length + 1, path.LastIndexOf(".") - Content.Length - 1);
        }

        #region dir 안에 있는 첫 번째 파일의 이름을 return
        public static string GetFileName(string path)
        {
            return new DirectoryInfo(path).GetFiles()[0].FullName;
        }
        #endregion

        #region dir 안에 있는 모든 파일들의 이름들을 return
        public static string[] GetFilesNames(string path)
        {
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            string[] str = new string[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                str[i] = files[i].FullName;
            }
            return str;
        }
        #endregion


        #region dir 안에 있는 첫 번째 dir의 이름을 return
        public static string GetDirName(string path)
        {
            return new DirectoryInfo(path).GetDirectories()[0].FullName;
        }
        #endregion

        #region dir 안에 있는 모든 dir들의 이름을 return
        public static string[] GetDirsNames(string path)
        {
            DirectoryInfo[] dirs = new DirectoryInfo(path).GetDirectories();
            string[] str = new string[dirs.Length];
            for (int i = 0; i < dirs.Length; i++)
            {
                str[i] = dirs[i].FullName;
            }
            return str;
        }
        #endregion
    }
}
