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
    class MenuObject {

        public Vector2 position;
        public Texture2D texture;

        public MenuObject(string specificTexture) { 
            texture = Game1.CM.Load<Texture2D>(specificTexture);
        }

        public void Draw(SpriteBatch sb, bool highlight) {
            if (highlight)
                sb.Draw(texture, position, Color.Green);
            else
                sb.Draw(texture, position, Color.White);
        }
    }
}
