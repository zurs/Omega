using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Omega {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        // General variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static List<Block> blocks;
        static Player player;
        public static State gameState;
        public static ContentManager CM;
        public static Rectangle screenSize;
        Menu menu;
        private SpriteFont pauseFont;
        Vector2 pausePosition;
        string currentLevelUrl;
        public static bool shouldExit;

        SpriteFont font1;
        Vector2 fontPos;
        public static string output = "----";

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            screenSize = GraphicsDevice.PresentationParameters.Bounds;
            
            //string url = @"C:\Users\Kabeltv\Documents\Visual Studio 2013\Projects\Omega\Omega\Omega\bin\x86\Debug\Map1.txt";
            pausePosition = new Vector2(screenSize.X / 2 - 30, screenSize.Y / 2 - 15);
            shouldExit = false;
            gameState = State.MENU;
            
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            CM = Content;
            menu = new Menu();
            blocks = new List<Block>();
            runMenu();
            pauseFont = Content.Load<SpriteFont>("pauseFont");
            font1 = Content.Load<SpriteFont>("Collision");
            fontPos = new Vector2(10, 10);
        }

        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        public void ManualExit() {
            if(shouldExit)
                this.Exit();
        }

        public static Vector2 GetPlayerPosition() {
            return player.position;
        }

        protected override void Update(GameTime gameTime) {
            ManualExit();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            output = "";

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.P) && gameState != State.MENU && gameState != State.SELECTLEVEL)
                gameState = State.PAUSE;
            else if (ks.IsKeyDown(Keys.P) && gameState == State.PAUSE)
                gameState = State.PLAY;
            if (ks.IsKeyDown(Keys.M))
                gameState = State.MENU;
            if (ks.IsKeyDown(Keys.R) && gameState == State.PLAY) // Should reload the level
                loadLevel(currentLevelUrl);


            if(gameState == State.PLAY)
                player.Update(gameTime);
            else if (gameState == State.MENU || gameState == State.SELECTLEVEL) {
                menu.Update();
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (gameState == State.MENU || gameState == State.SELECTLEVEL)
                menu.Draw(spriteBatch);
            else if (gameState == State.PLAY) {
                foreach (Block block in blocks) {
                    block.Draw(spriteBatch);
                }
                player.Draw(spriteBatch);
                spriteBatch.DrawString(font1, output, fontPos, Color.White);
                if (gameState == State.PAUSE)
                    spriteBatch.DrawString(pauseFont, "PAUSED", pausePosition, Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        public static void loadLevel(string url) { // Should load the selected level
            blocks.Clear();
            string[] lines = System.IO.File.ReadAllLines(url);

            List<string> lineList = new List<string>(lines);

            // Add the Player
            string[] playerLine = lineList[0].Split(' ');
            Vector2 playerPos = new Vector2(Convert.ToInt32(playerLine[0]), Convert.ToInt32(playerLine[1]));
            player = new Player(playerPos, playerLine[2]);
            lineList.RemoveAt(0);

            foreach (string line in lineList) { // Adds all the blocks in the level
                string[] data = line.Split(' ');
                int X = Convert.ToInt32(data[0]);
                int Y = Convert.ToInt32(data[1]);
                Vector2 position = new Vector2(X, Y);
                string specificTexture = data[2];
                blocks.Add(new Block(position, specificTexture));
            }
            foreach (Block block in blocks) {
                block.Load();
            }
        }

        public void runMenu() { 
            
        }

        public enum State{
            MENU,
            SELECTLEVEL,
            PAUSE,
            PLAY
        }
    }
}
