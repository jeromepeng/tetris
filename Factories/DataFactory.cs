using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interface;
using Common;
using Data.DataContainer.TetrisDataGenerator;

namespace Factories
{
    public class DataFactory
    {
        /// <summary>
        /// The generator of game data.
        /// </summary>
        private static IDataGenerator generator;

        /// <summary>
        /// Gets the generator of game data.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDataGenerator GetDataGenerator(GameType type)
        {
            switch (type)
            {
                case GameType.Tetris:
                    {
                        generator = new TetrisDataGenerator();
                        break;
                    }
                default:
                    {
                        generator = null;
                        break;
                    }
            }
            return generator;
        }
    }
}
