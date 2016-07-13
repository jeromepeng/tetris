using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    /// <summary>
    /// Interface for viewer.
    /// </summary>
    public interface IViewer
    {
        /// <summary>
        /// Rotate the viewer around the original.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="original"></param>
        void Rotate(double value, MoveType type, double[] original);

        /// <summary>
        /// Move the viewer.
        /// </summary>
        /// <param name="type"></param>
        void Move(double value, MoveType type);

        /// <summary>
        /// Get the screen of this viewer.
        /// </summary>
        I3DData Screen { get; }

        /// <summary>
        /// Get the project point of this viewer.
        /// </summary>
        I3DData ProjectPoint { get; }
    }
}
