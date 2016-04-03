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
    class Character {
        public Vector2 position;
        public Vector2 leftCollisionPoint;
        public Vector2 rightCollisionPoint;
        public Texture2D texture;
        Vector2 gravity = new Vector2(0, 0.15f);
        public string specificTexture;
        public Vector2 velocity;
        public bool falling = true;

        protected PlayerAnimation PA;

        public Character(Vector2 position, string specificTexture) {
            this.position = position;
            this.specificTexture = specificTexture;
            texture = Game1.CM.Load<Texture2D>(specificTexture);

            // Loading the spritesheet class
            PA = new PlayerAnimation(texture, 7, 3);
            PA.IsLooping = true;
            PA.framesPerSecond = 12;
        }

        virtual public void Update(GameTime gameTime) { }

        public void UpdatePos(GameTime gameTime) {
            //The player will basicly always fall down but gets back on top of the block at collision
            velocity += gravity;
            position += velocity;
                
            // Sets the "feets" of the player
            leftCollisionPoint = new Vector2((int)position.X, (int)position.Y + texture.Bounds.Height / PA.rows);
            rightCollisionPoint = new Vector2((int)position.X + texture.Bounds.Width / PA.frames, (int)position.Y + texture.Bounds.Height / PA.rows);

            Game1.output = "LeftY: " + leftCollisionPoint.Y + "; RightY: " + rightCollisionPoint.Y + " Falling: " + falling; // Debug text

            PA.Update(gameTime);
        }

        public void checkForCollision() { 
            foreach(Block block in Game1.blocks){
                if (!(leftCollisionPoint.X < block.position.X || leftCollisionPoint.X > (block.position.X + block.texture.Bounds.Width))) {
                    if (!(leftCollisionPoint.Y < block.position.Y || leftCollisionPoint.Y > (block.position.Y + block.texture.Bounds.Height))) {
                        atCollision(block);
                    }
                }
                else if (!(rightCollisionPoint.X < block.position.X || rightCollisionPoint.X > (block.position.X + block.texture.Bounds.Width))) {
                    if (!(rightCollisionPoint.Y < block.position.Y || rightCollisionPoint.Y > (block.position.Y + block.texture.Bounds.Height))) {
                        atCollision(block);
                    }
                }
                
            }
        }

        public void Draw(SpriteBatch sb) {
            PA.Draw(sb);
        }

        public void atCollision(Block block) { 
            position.Y = block.position.Y - texture.Height / PA.rows;
            Game1.output += " Collision: true"; // Debug text
            if (velocity.Y > 0) {
                falling = false;
                velocity.Y = 0;
            }
        }
    }
}
