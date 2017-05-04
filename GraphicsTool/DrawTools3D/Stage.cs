using Common;
using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsTool.DrawTools3D
{
    /// <summary>
    /// Class for stage.
    /// </summary>
    public class Stage : IStage
    {
        #region Private Member
        /// <summary>
        /// All objects in this stage.
        /// </summary>
        private Dictionary<string, I3DData> objects = new Dictionary<string, I3DData>();

        /// <summary>
        /// The plane of screen.
        /// </summary>
        private IViewer viewer;
        #endregion

        #region Consturctor
        /// <summary>
        /// Constructor of stage.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="project"></param>
        public Stage(IViewer viewer)
        {
            this.viewer = viewer;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Get screen point of specific object.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<double[]> GetScreenPointOfOneObject(string key)
        {
            List<double[]> result = new List<double[]>();
            return result;
        }

        /// <summary>
        /// Get screen plane of specific object.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isHideByOtherObjecs"></param>
        /// <returns></returns>
        public List<I3DData> GetScreenPlaneOfOneObject(string key, double screenHeight, bool isHideByOtherObjecs)
        {
            List<I3DData> result = new List<I3DData>();
            if (isHideByOtherObjecs)
            {
                result = GetScreenPlaneOfAllObjects(key, screenHeight);
            }
            else
            {
                if (GetRealData(key).DataType() == Common.GraphicType.Cuboid)
                {
                    List<I3DData> resultPlane = GetRealData(key).GetScreenPlaneWithHide(viewer.ProjectPoint, viewer.Screen, screenHeight, 1);
                    for (int i = 0; i < resultPlane.Count; i++)
                    {
                        result.Add(resultPlane[i]);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Get all the planes of this stage, some plane will be invisiable if the object is hidded by other objects.
        /// </summary>
        /// <param name="screenHeight"></param>
        /// <returns></returns>
        public List<I3DData> GetScreenPlaneOfAllObjects(string key, double screenHeight)
        {
            List<I3DData> result = new List<I3DData>();
            if (GetRealData(key).DataType() == Common.GraphicType.Cuboid)
            {
                foreach (KeyValuePair<string, I3DData> kvp in objects)
                {
                    List<I3DData> resultPlane = new List<I3DData>();
                    if (kvp.Key != key)
                    {
                        resultPlane = GetRealData(key).GetScreenPlaneWihtHideOnSpecificObjects(viewer.ProjectPoint, viewer.Screen, screenHeight, 1, kvp.Value);
                    }
                    for (int i = 0; i < resultPlane.Count; i++)
                    {
                        result.Add(resultPlane[i]);
                    }
                }
            }
            return result;
        }
        #endregion

        #region Interface Method
        /// <summary>
        /// Get information of this stage.
        /// </summary>
        /// <returns></returns>
        public string StageInfo()
        {
            return string.Empty;
        }

        /// <summary>
        /// Add a data into stage.
        /// </summary>
        public void AddData(I3DData data)
        {
            objects.Add(data.GetKey(), data);
        }

        /// <summary>
        /// Get real data of this key.
        /// </summary>
        /// <returns></returns>
        public I3DData GetRealData(string key)
        {
            I3DData result = null;
            if (objects.ContainsKey(key))
            {
                result = objects[key];
            }
            return result;
        }

        /// <summary>
        /// Get project data of this stage.
        /// </summary>
        /// <returns></returns>
        public List<I3DData> GetProjectData()
        {
            return null;
        }

        /// <summary>
        /// Set the info of a stage.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="projectPoint"></param>
        public void SetStageInfo(IViewer viewerInformation)
        {
            //Do nothing.
        }

        /// <summary>
        /// Rotate the viewer of this stage.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public void RotateTheViewr(double value, MoveType type, double[] original)
        {
            //Do nothing.
        }

        /// <summary>
        /// Rotate the viewer of this stage.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public void RotateTheViewrAroundProjectPoint(double value, MoveType type)
        {
            viewer.Rotate(value, type, viewer.ProjectPoint.GetData() as double[]);
        }

        /// <summary>
        /// Move the viewer of this stage.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public void MoveViewer(double value, MoveType type)
        {
            viewer.Move(value, type);
        }

        /// <summary>
        /// Get information of specific data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string StageDataInfo(string key)
        {
            string result = string.Empty;
            if (objects.ContainsKey(key))
            {
                return objects[key].InfoString();
            }
            return result;
        }
        #endregion
    }
}
