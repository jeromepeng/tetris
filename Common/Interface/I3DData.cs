using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    /// <summary>
    /// Interface for 3D data.
    /// </summary>
    public interface I3DData
    {
        /// <summary>
        /// Get info string.
        /// </summary>
        /// <returns></returns>
        string InfoString();

        /// <summary>
        /// Get graphic of this data;
        /// </summary>
        /// <returns></returns>
        GraphicType DataType();

        /// <summary>
        /// Get data.
        /// </summary>
        object GetData();

        /// <summary>
        /// Move a data.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        void Move(double value, MoveType type);

        /// <summary>
        /// Rotate a data around the original.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="type"></param>
        /// <param name="?"></param>
        void Rotate(double angle, MoveType type, double[] original);

        /// <summary>
        /// Key of this object
        /// </summary>
        /// <returns></returns>
        string GetKey();

        /// <summary>
        /// Get all of the planes which can be seen in any screen.
        /// </summary>
        /// <param name="projectPoint"></param>
        /// <param name="screen"></param>
        /// <param name="screenHeight"></param>
        /// <returns></returns>
        List<I3DData> GetScreenPlaneWithHide(I3DData projectPoint, I3DData screen, double screenHeight, object parameter);

        /// <summary>
        /// Get all of the planes which can be seen in any screen.
        /// And all the planes which hide behind specific object won't be return.
        /// </summary>
        /// <param name="projectPoint"></param>
        /// <param name="screen"></param>
        /// <param name="screenHeight"></param>
        /// <param name="parameter"></param>
        /// <param name="specificObject"></param>
        /// <returns></returns>
        List<I3DData> GetScreenPlaneWihtHideOnSpecificObjects(I3DData projectPoint, I3DData screen, double screenHeight, object parameter, I3DData specificObject);

        /// <summary>
        /// Is the point hide behind this object.
        /// </summary>
        /// <param name="projectPoint"></param>
        /// <param name="screen"></param>
        /// <param name="screenHeight"></param>
        /// <param name="parameter"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        bool IsPointHideBehindThisObject(I3DData projectPoint, I3DData screen, double screenHeight, object parameter, I3DData point);
    }
}
