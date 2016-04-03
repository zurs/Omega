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
    class Map {

        public string path;
        public string name;

        public Map(string path) {
            this.path = path;
            string[] sortOfPath = path.Split(Convert.ToChar(@"\"));
            name = sortOfPath[sortOfPath.Length - 1].Substring(0, sortOfPath[sortOfPath.Length - 1].Length - 4); // Gives the name of the file without the .txt
        }

    }
}
