using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace LevelEditorOmega {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D blockTexture;
        Vector2 mousePosition;
        List<Vector2> solidBlocks;
        ButtonState previousMouseState = ButtonState.Released;
        KeyboardState previousKeyState;
        bool shouldExit = false;

        SpriteFont sf;
        Vector2 spritePosition;
        string spriteText;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            solidBlocks = new List<Vector2>();
            mousePosition = new Vector2(0, 0);
            spritePosition = new Vector2(5, 10);
            this.IsMouseVisible = true;
            spriteText = "Press S to save; Press R to reverse; Press Q to quit;";

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            blockTexture = Content.Load<Texture2D>("stdBlock");
            sf = Content.Load<SpriteFont>("Tutorial");
        }

        protected override void UnloadContent() {

        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || shouldExit)
                this.Exit();

            MouseState mouse = Mouse.GetState();
            mousePosition.X = mouse.X;
            mousePosition.Y = mouse.Y;

            if(mouse.LeftButton == ButtonState.Pressed && mouse.LeftButton != previousMouseState){
                solidBlocks.Add(new Vector2(mouse.X, mouse.Y));
            }
            previousMouseState = mouse.LeftButton;

            HandleKeyboard();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(blockTexture, mousePosition, Color.White);
            foreach(Vector2 position in solidBlocks){
                spriteBatch.Draw(blockTexture, position, Color.White);
            }
            spriteBatch.DrawString(sf, spriteText, spritePosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void HandleKeyboard() {

            KeyboardState ks = Keyboard.GetState();
            if(ks != previousKeyState){
                if (ks.IsKeyDown(Keys.S))
                    SaveMap();
                else if (ks.IsKeyDown(Keys.R))
                    Reverse();
                else if (ks.IsKeyDown(Keys.Q))
                    Quit();
            }
            previousKeyState = ks;
        }

        private void SaveMap() {
            bool works = false;
            int loops = 1;
            string path = "";
            while (works != true) {
                if (!File.Exists(path = @".\Maps\newMap" + loops + ".txt"))
                    break;
                else
                    loops++;
            }
            string[] lines = new string[solidBlocks.Count + 1]; // Add 1 because of the players first line
            lines[0] = "10 10 playerWalkSheet";
            for (int i = 1; i <= solidBlocks.Count; i++ ) {
                lines[i] = solidBlocks[i - 1].X.ToString() + " " + solidBlocks[i - 1].Y.ToString() + " stdBlock";
            }
            File.WriteAllLines(path, lines);
            
        }

        private void Reverse() {
            if (solidBlocks.Count > 0) {
                solidBlocks.RemoveAt(solidBlocks.Count - 1);
            }
        }

        private void Quit() {
            shouldExit = true;
        }
    }
}
