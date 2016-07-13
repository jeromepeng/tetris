using Common;
using Common.Interface;
using GraphicsTool.DrawTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factories
{
    public class GraphicsToolFactory
    {
        /// <summary>
        /// The generator of game data.
        /// </summary>
        private static IDrawTool drawTool;

        /// <summary>
        /// Gets the generator of game data.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDrawTool GetDrawTool(GameType type)
        {
            switch (type)
            {
                case GameType.Tetris:
                    {
                        drawTool = new TetrisDrawTool();
                        break;
                    }
                default:
                    {
                        drawTool = null;
                        break;
                    }
            }
            return drawTool;
        }
    }
}
