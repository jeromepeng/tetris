using Common;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataFor3D
{
    /// <summary>
    /// Class for a normal viewer.
    /// </summary>
    public class NormalViewer : IViewer
    {
        #region Private member
        /// <summary>
        /// The screen for this viewer.
        /// </summary>
        private Plane viewScreen;

        /// <summary>
        /// Project of this viewer.
        /// </summary>
        private Point3D viewPoint;
        #endregion

        #region Constructor
        /// <summary>
        /// Construcotr
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="point"></param>
        public NormalViewer(Plane screen, Point3D point)
        {
            viewPoint = point;
            viewScreen = screen;
        }
        #endregion

        #region Interface Method
        /// <summary>
        /// Rotate the viewer around the original.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="original"></param>
        public void Rotate(double value, MoveType type, double[] original)
        {
            viewScreen.Rotate(value, type, original);
            viewPoint.Rotate(value, type, original);
        }

        /// <summary>
        /// Move the viewer.
        /// </summary>
        /// <param name="type"></param>
        public void Move(double value, MoveType type)
        {
            viewPoint.Data[(int)type] += value;
        }

        /// <summary>
        /// Get the screen of this viewer.
        /// </summary>
        public I3DData Screen 
        {
            get
            {
                return viewScreen;
            }
        }

        /// <summary>
        /// Get the project point of this viewer.
        /// </summary>
        public I3DData ProjectPoint 
        {
            get
            {
                return viewPoint;
            }
        }
        #endregion
    }
}
