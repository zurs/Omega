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
    class Player:Character {

        int horizontalSpeed = 5;
        Vector2 jumpSpeed = new Vector2(0, -5); // How fast the player will go upwards

        public Player(Vector2 position, string specificTexture) : base(position, specificTexture) { }

        public override void Update(GameTime gameTime){


            KeyboardState ks = Keyboard.GetState();
            
            if (ks.IsKeyDown(Keys.D)) {
                position.X += horizontalSpeed;
                PA.movementState = MovementState.WALKING;
                PA.direction = Direction.RIGHT;
            }
            else if (ks.IsKeyDown(Keys.A)) {
                position.X -= horizontalSpeed;
                PA.movementState = MovementState.WALKING;
                PA.direction = Direction.LEFT;
            }
            else
                PA.movementState = MovementState.STILL;
            if (falling == true)
                PA.movementState = MovementState.FALLING;


            if (ks.IsKeyDown(Keys.Space) && falling != true) {
                velocity += jumpSpeed;
                falling = true;
            }

            UpdatePos(gameTime);
            checkForCollision();
            if (falling == false)
                velocity.Y = 0;

        }

    }
}
