using System;

namespace LuckyStar.Models
{
    public class LuckyItem
    {
        public string CubeA { get; set; }

        public string CubeB { get; set; }

        public string CubeC { get; set; }

        public LuckyItem(string cubeA, string cubeB, string cubeC)
        {
            CubeA = cubeA;
            CubeB = cubeB;
            CubeC = cubeC;
        }

        public bool IsContain(string cube)
        {
            return this.CubeA == cube || this.CubeB == cube || this.CubeC == cube;
        }
    }
}