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
    public class SpriteManager {

        protected Texture2D texture;
        public Vector2 position = Vector2.Zero;
        public Color color = Color.White;
        public Vector2 origin;
        public float rotation = 0f;
        public float scale = 1f;
        public SpriteEffects spriteEffect;
        protected Rectangle[,] rectangles;
        protected int frameIndex = 0;
        protected int rowIndex;
        public int frames;
        public int rows;
        public Direction direction;
        public MovementState movementState;

        public SpriteManager(Texture2D texture, int frames, int rows) {
            this.texture = texture;
            this.frames = frames;
            this.rows = rows;
            int width = texture.Width / frames;
            int height = texture.Height / rows;
            rectangles = new Rectangle[frames, rows];
            for (int i = 0; i < frames; i++) {
                for (int a = 0; a < rows; a++) {
                    rectangles[i, a] = new Rectangle(i * width, a * height, width, height);
                }
            }
        }

        protected void UpdateDirection() {
            if (direction == Direction.RIGHT)
                spriteEffect = SpriteEffects.None;
            else if (direction == Direction.LEFT)
                spriteEffect = SpriteEffects.FlipHorizontally;

            switch(movementState){
                case MovementState.WALKING:
                    rowIndex = 0;
                    break;
                case MovementState.FALLING:
                    rowIndex = 1;
                    break;
                case MovementState.STILL:
                    rowIndex = 2;
                    break;
            }
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(texture, position, rectangles[frameIndex, rowIndex], color, rotation, origin, scale, spriteEffect, 0f);
        }
    }
    public enum Direction { 
        RIGHT,
        LEFT
    }
    public enum MovementState { 
        WALKING,
        STILL,
        FALLING
    }
}
