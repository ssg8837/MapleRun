using MapleRun0509.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleRun0509.Monster
{
    public static class MonsterInfo
    {
        /*
         * static 클래스 MonsterInfo
         * PathManager로부터 Monster 클래스에 있는 모든 몬스터들의 이름 경로를
         * Dictionary에 저장한다.
         */
        public static Dictionary<int,string> DicInfo;
        public static void Init()
        {
            DicInfo = new Dictionary<int, string>();
            foreach(string str in PathManager.MonsterAnimation)
            {
                DicInfo.Add(DicInfo.Count(), str);
            }
        }
    }
}
