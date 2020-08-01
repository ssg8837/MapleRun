using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MapleRun0509.Library;
using Microsoft.Xna.Framework.Input;
using MapleRun0509.Library.Screen;
using Microsoft.Xna.Framework.Media;

namespace MapleRun0509.Library
{
    // 화면에 나타나는 모든 것을 관리해줌
    // GameScreen,SplashScreen.. 등등
    public class ScreenManager
    {
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        // Content, graphics,spriteBatch는 Game1.cs의 것들을 그대로 ScreenManager에서도 사용할 수 있도록 선언한 것으로,
        // Game1.cs에서 Load할 때 넘겨주면 된다.
        public ContentManager Content { get; set; }
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        
        #region 추가하고 싶은 Screen들을 추가해주면 됨
        PlayScreen playerScreen;
        MonsterScreen monsterScreen;
        MapScreen mapScreen;
        MenuScreen menuScreen;
        ItemScreen itemScreen;
        CollisionScreen collisionScreen;

        CountScreen_1 countScreen_1;
        CountScreen_2 countScreen_2;
        CountScreen_3 countScreen_3;
        CountScreen_Start countScreen_start;

        GameUiScreen gameUiScreen;
        GameUi_NumberScreen gameUiNumberScreen;

        ResultScreen resultScreen;
        #endregion

        #region 기타 변수들
        EnumManager.GameState gameState;
        Song songMenu;
        Song songGame;
        Song songEnding;

        bool isSongMenuStart;
        bool isSongGameStart;
        bool isSongEndingStart;
        #endregion

        #region 싱글톤 인스턴스
        // 싱글톤 패턴을 적용해서 ScreenManager의 인스턴스를 생성할 것.
        // 왜? ScreenManager의 인스턴스가 오직 하나만 생성되고, 
        //  다른 곳에서 새로 생성 못 하게 하고, 오직 하나로만 통일되게 접근할 수 있도록
        private static ScreenManager instance;
        public static ScreenManager Instace
        {
            get
            {
                // ScreenManager가 생성이 안 됬으면 새로 생성해주고
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                // 그 외의 경우는 현재 생성되어 있는 ScreenManager의 instance를 반환
                return instance;
            }
        }
        #endregion  

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region 생성자
        public ScreenManager()
        {
            playerScreen = new PlayScreen();
            monsterScreen = new MonsterScreen();
            mapScreen = new MapScreen();
            menuScreen = new MenuScreen();
            itemScreen = new ItemScreen();
            collisionScreen = new CollisionScreen();

            countScreen_1 = new CountScreen_1();
            countScreen_2 = new CountScreen_2();
            countScreen_3 = new CountScreen_3();
            countScreen_start = new CountScreen_Start();
            gameUiScreen = new GameUiScreen();
            gameUiNumberScreen = new GameUi_NumberScreen();

            resultScreen = new ResultScreen();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Load
        public void Load(ContentManager Content)
        {
            // Game1.cs의 Content를 받아서 ScreenManager만의 ContentManager를 생성한다.
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            playerScreen.Load();
            monsterScreen.Load();
            mapScreen.Load();
            menuScreen.Load();
            itemScreen.Load();
            collisionScreen.Load();

            countScreen_1.Load();
            countScreen_2.Load();
            countScreen_3.Load();
            countScreen_start.Load();

            gameUiScreen.Load();
            gameUiNumberScreen.Load();

            resultScreen.Load();
            songGame = Content.Load<Song>("BGM");
            songMenu = Content.Load<Song>("MenuSound");
            songEnding = Content.Load<Song>("Ending");

            MediaPlayer.IsRepeating = true;
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Unload
        public void Unload()
        {
            playerScreen.Unload();
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        
        #region Draw
        public void Draw(SpriteBatch sp, GameTime gt)
        {
            #region 카메라에 영향을 받는 것들 여기다가 Draw(플레이어,몬스터,아이템 등등)
            sp.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, CameraManager.Instance.mx);

            mapScreen.Draw(sp, gt);
            itemScreen.Draw(sp, gt);
            playerScreen.Draw(sp, gt);
            monsterScreen.Draw(sp, gt);

            sp.End();
            #endregion

            #region 카메라에 영향 안 받는 것들 (UI 같은 거)
            sp.Begin();

            countScreen_3.Draw(sp, gt);
            countScreen_2.Draw(sp, gt);
            countScreen_1.Draw(sp, gt);
            gameUiScreen.Draw(sp, gt);
            gameUiNumberScreen.Draw(sp, gt);
            countScreen_start.Draw(sp, gt);
            collisionScreen.Draw(sp, gt);

            switch (gameState)
            {
                case EnumManager.GameState.Menu:
                    menuScreen.Draw(sp, gt);
                    break;

                case EnumManager.GameState.Start:
                    break;

                case EnumManager.GameState.End:
                    resultScreen.Draw(sp, gt);
                    break;
            }

            sp.End();
            #endregion
        }
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

        #region Update
        public void Update(GameTime gt, Game game)
        {
            switch (gameState)
            {
                case EnumManager.GameState.Menu:
                    if(isSongGameStart == false)
                    {
                        MediaPlayer.Play(songMenu);
                        isSongGameStart = true;
                    }
                    menuScreen.Update(gt);
                    if (menuScreen.isExitClicked)
                    {
                        game.Exit();
                    }
                    if (menuScreen.isStartClicked)
                    {
                        if (menuScreen.FadeOut(gt, 1.0f))
                        {
                            if (isSongMenuStart == false)
                            {
                                MediaPlayer.Play(songGame);
                                isSongMenuStart = true;
                            }

                            if (countScreen_3.FadeIn(gt, 1.0f) || countScreen_3.isEnd == true)
                            {
                                countScreen_3.Init();
                                if (countScreen_2.FadeIn(gt, 1.0f) || countScreen_2.isEnd == true)
                                {
                                    countScreen_2.Init();
                                    if (countScreen_1.FadeIn(gt, 1.0f) || countScreen_1.isEnd == true)
                                    {
                                        countScreen_1.Init();
                                        if (countScreen_start.FadeIn(gt, 1.0f) || countScreen_start.isEnd == true)
                                        {
                                            countScreen_start.Init();       // 3,2,1,start 카운트
                                            if (playerScreen.FadeIn(gt, 2.0f) || monsterScreen.FadeIn(gt, 2.0f))
                                            {
                                                gameUiScreen.Update(gt);
                                                gameState = EnumManager.GameState.Start;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

                case EnumManager.GameState.Start:

                    mapScreen.Update(gt, (playerScreen.Position));

                    playerScreen.Update(gt);
                    playerScreen.CheckTile(mapScreen.tileList);


                    itemScreen.Update(gt);
                    itemScreen.itemRemove(playerScreen.Rect);
                    if (itemScreen.List.Count < 10)
                    {
                        itemScreen.Additem(gt, playerScreen.Rect, mapScreen.tileList);
                    }
                    if (!collisionScreen.invincibility)
                    {
                        collisionScreen.collisionWithMonster(gt, playerScreen.Hero, monsterScreen.monsterList);
                    }

                    else
                    {
                        if (gt.TotalGameTime.TotalMilliseconds >= (collisionScreen.Time.TotalMilliseconds + 500))
                        {

                            collisionScreen.invincibility = false;

                        }
                    }
                    collisionScreen.collisionWithItems(gt, playerScreen.Hero, itemScreen.List);

                    gameUiNumberScreen.connectLife(playerScreen.Hero);

                    gameUiNumberScreen.connectMeso(playerScreen.Hero);
                    mapScreen.CheckPlayer(playerScreen.Rect);

                    monsterScreen.Update(gt);
                    monsterScreen.CheckTile(mapScreen.tileList);
                    monsterScreen.CheckPlayer(playerScreen.Rect);

                    gameUiNumberScreen.Update(gt);

                    if (gameUiNumberScreen.heart <= 0)
                    {
                        resultScreen.settingValue(gameUiNumberScreen.run/10, gameUiNumberScreen.meso);
                        gameState = EnumManager.GameState.End;
                    }
                    break;

                case EnumManager.GameState.End:
                    if(isSongEndingStart == false)
                    {
                        MediaPlayer.Play(songEnding);
                        isSongEndingStart = true;
                    }
                    resultScreen.FadeIn(gt, 1.0f);
                    resultScreen.Update(gt);

                    if (resultScreen.isOkClicked && gameState == EnumManager.GameState.End)
                    {
                        game.Exit();
                    }

                    break;
            }
            CameraManager.Instance.Update();
        }
        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
        #endregion

        // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
    }
}
