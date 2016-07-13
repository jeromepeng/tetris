using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.EventArgument;

namespace Common.Interface
{
    public interface IDrawTool
    {
        /// <summary>
        /// Draw a data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="graphics"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        void InitDrawData(DataDefine[] data, object graphics, int x, int y, int size);

        /// <summary>
        /// Action as timer go.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        void TimeAction(ActionEventArgs arg);

        /// <summary>
        /// Action for key press.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        void KeyAction(ActionEventArgs arg);

        /// <summary>
        /// Action to move down.
        /// </summary>
        void DownAction(ActionEventArgs arg);

        /// <summary>
        /// Action to move up.
        /// </summary>
        void UpAction(ActionEventArgs arg);

        /// <summary>
        /// Action to move left.
        /// </summary>
        void LeftAction(ActionEventArgs arg);

        /// <summary>
        /// Action to move right;
        /// </summary>
        void RightAction(ActionEventArgs arg);

        /// <summary>
        /// Update the game region, make it changed in some condition,
        /// For example:Delete one line if the line is fully filled.
        /// </summary>
        void UpdateGameRegion();
    }
}
