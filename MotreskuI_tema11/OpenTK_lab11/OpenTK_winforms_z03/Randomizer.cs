
using System;
using System.Drawing;

namespace OpenTK_winforms_z02
{
    public class Randomizer
    {
        private Random random;

        public Randomizer()
        {
            random = new Random();
        }

        public Color GenerateRandomColor()
        {
            int r = random.Next(0, 255);
            int g = random.Next(0, 255);
            int b = random.Next(0, 255);

            return Color.FromArgb(r, g, b);
        }
    }
}
