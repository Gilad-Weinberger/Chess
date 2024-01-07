using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Piece
    {
        public string type;
        public string color;

        public Piece(string pieceCode)
        {
            string[] parts = pieceCode.Split('_');
            string color = parts[0];
            string type = parts[1];

            this.type = char.ToUpper(type[0]) + type.Substring(1);
            this.color = char.ToUpper(color[0]) + color.Substring(1);
        }
    }
}
