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
    public class Block {
        public bool solid = true;
        public Vector2 position;
        public Texture2D texture;
        public Rectangle rectangle;
        public string specificTexture;

        public Block(Vector2 position, string specificTexture) {
            this.position = position;
            this.specificTexture = specificTexture;
        }

        public void Load() { 
            texture = Game1.CM.Load<Texture2D>(specificTexture);
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Bounds.Y, texture.Bounds.X); // Not necessary with a rectangle anymore tho
        }

        public void Draw(SpriteBatch sb){
            sb.Draw(texture, position, Color.White);
        }

    }
}
