using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Omega {
    class PlayerAnimation : SpriteManager {

        private float timeElapsed;
        public bool IsLooping = true;
        private float timeToUpdate = 0.05f;

        // Used to change the number of frames per second
        public int framesPerSecond { set { timeToUpdate = (1f / value); } }

        public PlayerAnimation(Texture2D texture, int frames, int rows) : base(texture, frames, rows) { 
            
        }

        public void Update(GameTime gameTime) {
            
            UpdateDirection();
            position = Game1.GetPlayerPosition();
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate) {
                timeElapsed -= timeToUpdate;
                if (!IsLooping)
                    frameIndex = 0;
                else if (frameIndex < rectangles.GetLength(0) - 1 && IsLooping)
                    frameIndex++;
                else if (IsLooping)
                    frameIndex = 0;
            }
        }
        

    }
}
