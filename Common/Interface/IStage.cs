using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    /// <summary>
    /// Interface for stage.
    /// </summary>
    public interface IStage
    {
        /// <summary>
        /// Get information of this stage.
        /// </summary>
        /// <returns></returns>
        string StageInfo();

        /// <summary>
        /// Add a data into stage.
        /// </summary>
        void AddData(I3DData data);

        /// <summary>
        /// Get real data of this key.
        /// </summary>
        /// <returns></returns>
        I3DData GetRealData(string key);

        /// <summary>
        /// Get project data of this stage.
        /// </summary>
        /// <returns></returns>
        List<I3DData> GetProjectData();

        /// <summary>
        /// Set the information of the viewer in this stage.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="projectPoint"></param>
        void SetStageInfo(IViewer viewerInformation);

        /// <summary>
        /// Rotate the viewer of this stage.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        void RotateTheViewr(double value, MoveType type, double[] original);

        /// <summary>
        /// Rotate the viewer arount the project point.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        void RotateTheViewrAroundProjectPoint(double value, MoveType type);

        /// <summary>
        /// Move the viewer of this stage.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        void MoveViewer(double value, MoveType type);

        /// <summary>
        /// Get information of specific data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string StageDataInfo(string key);
    }
}
