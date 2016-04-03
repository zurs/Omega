using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Omega {
    class Menu {

        List<MenuObject> buttons;
        int selectedButton = 0;
        List<Map> maps;
        SpriteFont mapFont;
        KeyboardState oldState;

        public Menu() {
            mapFont = Game1.CM.Load<SpriteFont>("MapFont");
            buttons = new List<MenuObject>();
            //Start adding the buttons
            buttons.Add(new MenuObject("start"));
            buttons.Add(new MenuObject("quit"));

            oldState = Keyboard.GetState();

            //Sets the first menubutton
            buttons[0].position.X = Game1.screenSize.Width / 2 - (buttons[0].texture.Bounds.Width / 2); // Middle position
            buttons[0].position.Y = Game1.screenSize.Height * 0.10f; // 10% down from the top

            for (int i = 1; i < buttons.Count; i++) {
                buttons[i].position.X = Game1.screenSize.Width / 2 - (buttons[0].texture.Bounds.Width / 2); // Middle position
                buttons[i].position.Y = buttons[i - 1].texture.Bounds.Height + buttons[i - 1].position.Y + Game1.screenSize.Height * 0.08f; // Sets it 8% down from last button
            }

            // Load the maps
            string path = Directory.GetCurrentDirectory() + @"\Maps\";
            string[] foundFiles = Directory.GetFiles(path, "*.txt");
            maps = new List<Map>();

            foreach (string map in foundFiles) {
                maps.Add(new Map(map)); // The substring is unnecessary but it looks better without the .txt
            }

        }

        public void Update() {
            //Gives a menu that's easy to implement more buttons by scrolling through the list
            if (Game1.gameState == Game1.State.SELECTLEVEL) {
                selectLevel();
                return;
            }

            KeyboardState ks = Keyboard.GetState();
            if (ks != oldState) {
                if (ks.IsKeyDown(Keys.Down)) {
                    selectedButton++;
                    if (selectedButton >= buttons.Count)
                        selectedButton = 0;
                }
                if (ks.IsKeyDown(Keys.Up)) {
                    selectedButton--;
                    if (selectedButton < 0)
                        selectedButton = buttons.Count - 1;
                }
                if (ks.IsKeyDown(Keys.Enter)) {
                    switch (selectedButton) {
                        case 0:
                            Game1.gameState = Game1.State.SELECTLEVEL;
                            selectedButton = 0;
                            break;
                        case 1:
                            Game1.shouldExit = true;
                            break;
                    }
                }
            }
            oldState = ks;


        }

        public void selectLevel() {

            KeyboardState ks = Keyboard.GetState();
            if (ks != oldState) {
                if (ks.IsKeyDown(Keys.Down)) {
                    selectedButton++;
                    if (selectedButton >= maps.Count)
                        selectedButton = 0;
                }
                if (ks.IsKeyDown(Keys.Up)) {
                    selectedButton--;
                    if (selectedButton < 0)
                        selectedButton = maps.Count - 1;
                }
                if (ks.IsKeyDown(Keys.Enter)) {
                    Game1.loadLevel(maps[selectedButton].path);
                    Game1.gameState = Game1.State.PLAY;
                }
            }
            oldState = ks;
        }

        public void drawSelectLevel(SpriteBatch sb) { 
            int i = 0;
            foreach(Map map in maps){
                if(selectedButton == i)
                    sb.DrawString(mapFont, map.name, new Vector2(20, i * 30), Color.Yellow);
                else
                    sb.DrawString(mapFont, map.name, new Vector2(20, i * 30), Color.White);
                i++;
            }
        }

        public void Draw(SpriteBatch sb) {
            if (Game1.gameState == Game1.State.MENU) {
                int i = 0;
                foreach (MenuObject button in buttons) {
                    if (selectedButton == i)
                        button.Draw(sb, true);
                    else
                        button.Draw(sb, false);
                    i++;
                }
            }
            else if (Game1.gameState == Game1.State.SELECTLEVEL) {
                drawSelectLevel(sb);
            }
        }
    }
}
