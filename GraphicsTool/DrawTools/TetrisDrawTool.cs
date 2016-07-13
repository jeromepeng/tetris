using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Common.Interface;
using Common;
using Common.EventArgument;

namespace GraphicsTool.DrawTools
{
    public class TetrisDrawTool : IDrawTool
    {
        #region Private property
        /// <summary>
        /// The data of current fraction.
        /// </summary>
        private DataDefine currentData;

        /// <summary>
        /// Private graphics.
        /// </summary>
        private Graphics thisGraphics;

        /// <summary>
        /// The size of pix.
        /// </summary>
        private int pixSize;

        /// <summary>
        /// The region of the game.
        /// </summary>
        private int[] gameRegion;

        /// <summary>
        /// Line number of the game region.
        /// </summary>
        private int lineNum;

        /// <summary>
        /// Column number of the game region.
        /// </summary>
        private int colNum;

        /// <summary>
        /// Start x of region.
        /// </summary>
        private int regionStartX;

        /// <summary>
        /// Start y of region.
        /// </summary>
        private int regionStartY;

        /// <summary>
        /// All datas.
        /// </summary>
        private DataDefine[] allDatas;

        /// <summary>
        /// Radom.
        /// </summary>
        private Random radomSeed;
        #endregion

        #region Interface function
        /// <summary>
        /// Draw a data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="graphics"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        public void InitDrawData(DataDefine[] data, object graphics, int x, int y, int size)
        {
            radomSeed = new Random(DateTime.Now.Millisecond);
            thisGraphics = graphics as Graphics;
            allDatas = data;
            currentData = data[radomSeed.Next(data.Length)];
            currentData.CurrentX = x;
            currentData.CurrentY = y;
            pixSize = size;
            lineNum = (int)thisGraphics.VisibleClipBounds.Height / size;
            colNum = (int)thisGraphics.VisibleClipBounds.Width / size;
            gameRegion = InitialGameRegion(ref lineNum, ref colNum);
            DrawData(currentData);
        }
        /// <summary>
        /// Action as timer go.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public void TimeAction(ActionEventArgs arg)
        {
            switch ((ActionType)arg.Parameter2)
            {
                case ActionType.Down:
                    {
                        DownAction(arg);
                        break;
                    }
                case ActionType.Right:
                    {
                        RightAction(arg);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            DrawGameRegion();
        }

        /// <summary>
        /// Action for key press.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public void KeyAction(ActionEventArgs arg)
        {
            switch ((ActionType)arg.Parameter2)
            {
                case ActionType.Down:
                    {
                        DownAction(arg);
                        break;
                    }
                case ActionType.Right:
                    {
                        RightAction(arg);
                        break;
                    }
                case ActionType.Left:
                    {
                        LeftAction(arg);
                        break;
                    }
                case ActionType.Up:
                    {
                        UpAction(arg);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            DrawGameRegion();
        }

        /// <summary>
        /// Action to move down.
        /// </summary>
        public void DownAction(ActionEventArgs arg)
        {
            if (CanDraw(currentData, ActionType.Down))
            {
                currentData.CurrentY -= arg.Parameter3;
                DrawData(currentData);
            }
            else
            {
                AddDataToGameRegion(currentData);
                currentData = allDatas[radomSeed.Next(allDatas.Length)];
                currentData.CurrentX = 4;
                currentData.CurrentY = 12;
            }
        }

        /// <summary>
        /// Action to move up.
        /// </summary>
        public void UpAction(ActionEventArgs arg)
        {
            if (CanDraw(currentData, ActionType.Up))
            {
                for (int i = 0; i < currentData.Datas.Length; i++)
                {
                    currentData.Datas[i] = Common.Math.Rotate.RotatePoint(currentData.Datas[i], 90);
                }
                DrawData(currentData);
            }
        }

        /// <summary>
        /// Action to move left.
        /// </summary>
        public void LeftAction(ActionEventArgs arg)
        {
            if (CanDraw(currentData, ActionType.Left))
            {
                currentData.CurrentX -= arg.Parameter3;
                DrawData(currentData);
            }
        }

        /// <summary>
        /// Action to move right;
        /// </summary>
        public void RightAction(ActionEventArgs arg)
        {
            if (CanDraw(currentData, ActionType.Right))
            {
                currentData.CurrentX += arg.Parameter3;
                DrawData(currentData);
            }
        }

        /// <summary>
        /// Update the game region, make it changed in some condition,
        /// For example:Delete one line if the line is fully filled.
        /// </summary>
        public void UpdateGameRegion()
        {
            for (int i = 1; i < lineNum; i++)
            {
                if (IsThisLineFullyFilled(i))
                {
                    RemoveOneLine(i);
                    i--;
                }
            }
            DrawGameRegion();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// If this line is fully filled.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="colNum"></param>
        /// <returns></returns>
        private bool IsThisLineFullyFilled(int line)
        {
            bool result = true;
            for (int i = 0; i < colNum; i++)
            {
                if (gameRegion[line * colNum + i] == 0)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Remove one line from game region.
        /// </summary>
        /// <param name="line"></param>
        private void RemoveOneLine(int line)
        {
            for (int i = 0; i < lineNum - 1; i++)
            {
                if (i >= line)
                {
                    for (int j = 0 ; j < colNum ; j++)
                    {
                        gameRegion[i * colNum + j] = gameRegion[(i + 1) * colNum + j];
                    }
                }
            }
        }

        /// <summary>
        /// Gets draws point from data point and x,y.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        /// <param name="halfSize"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Point GetDrawPointWithDataPoint(DataPoint pt, int x, int y, int size, int height)
        {
            Point result = new Point();
            result.X = (x + pt.X) * size;
            result.Y = height - ((y + pt.Y) * size);
            return result;
        }

        /// <summary>
        /// Gets a draw point from x and y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Point GetDrawPoint(int x, int y, int size, int height)
        {
            Point result = new Point();
            result.X = x * size;
            result.Y = height - (y * size);
            return result;
        }

        /// <summary>
        /// Judge that if the data can be drawn.
        /// </summary>
        /// <param name="currentData"></param>
        /// <returns></returns>
        private bool CanDraw(DataDefine currentData, ActionType type)
        {
            bool result = true;
            int actionX = 0;
            int actionY = 0;
            switch (type)
            {
                case ActionType.Down:
                    {
                        actionX = 0;
                        actionY = -1;
                        break;
                    }
                case ActionType.Up:
                    {
                        DataPoint[] tempDatas = new DataPoint[currentData.Datas.Length];
                        for (int i = 0; i < currentData.Datas.Length; i++)
                        {
                            tempDatas[i] = Common.Math.Rotate.RotatePoint(currentData.Datas[i], 90);
                        }
                        for (int i = 0; i < tempDatas.Length; i++)
                        {
                            if (tempDatas[i].Value > 0 && gameRegion[(tempDatas[i].Y + currentData.CurrentY + regionStartY + actionY) * colNum + tempDatas[i].X + currentData.CurrentX + regionStartX + actionX] > 0)
                            {
                                result = false;
                                break;
                            }
                        }
                        break;
                    }
                case ActionType.Left:
                    {
                        actionX = -1;
                        actionY = 0;
                        break;
                    }
                case ActionType.Right:
                    {
                        actionX = 1;
                        actionY = 0;
                        break;
                    }
            }
            for (int i = 0; i < currentData.Datas.Length; i++)
            {
                if (currentData.Datas[i].Value > 0 && gameRegion[(currentData.Datas[i].Y + currentData.CurrentY + regionStartY + actionY) * colNum + currentData.Datas[i].X + currentData.CurrentX + regionStartX + actionX] > 0)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Add one data to the game region.
        /// </summary>
        /// <param name="data"></param>
        private void DrawData(DataDefine data)
        {
            if (thisGraphics != null)
            {
                thisGraphics.Clear(Color.White);
                BColor color = data.GetAdditionalData("BColor") as BColor;
                Color drawColor = Color.YellowGreen;
                if (color != null)
                {
                    drawColor = Color.FromArgb(color.A, color.R, color.G, color.B);
                }
                for (int i = 0; i < currentData.Datas.Length; i++)
                {
                    if (currentData.Datas[i].Value > 0)
                    {
                        Point drawPoint = GetDrawPointWithDataPoint(currentData.Datas[i], data.CurrentX, data.CurrentY + regionStartY, pixSize, (int)thisGraphics.VisibleClipBounds.Height);
                        thisGraphics.FillRectangle(new SolidBrush(drawColor), drawPoint.X, drawPoint.Y, pixSize, pixSize);
                    }
                }
            }
        }

        /// <summary>
        /// Set the initial data of the game region
        /// </summary>
        /// <param name="lineNum"></param>
        /// <param name="colNum"></param>
        private int[] InitialGameRegion(ref int lineNum, ref int colNum)
        {
            regionStartX = 1;
            regionStartY = 1;
            lineNum = lineNum + regionStartY;
            colNum = colNum + regionStartX * 2;
            int[] result = new int[lineNum * colNum];
            for (int i = 0; i < lineNum; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < colNum; j++)
                    {
                        result[j] = 1;
                    }
                }
                else
                {
                    result[i * colNum] = 1;
                    result[i * colNum + colNum - 1] = 1;
                }
            }
            return result;
        }

        /// <summary>
        /// Add one data to the game region.
        /// </summary>
        /// <param name="data"></param>
        private void AddDataToGameRegion(DataDefine data)
        {
            if (thisGraphics != null)
            {
                thisGraphics.Clear(Color.White);
                for (int i = 0; i < data.Datas.Length; i++)
                {
                    gameRegion[(data.CurrentY + data.Datas[i].Y + regionStartY) * colNum + data.CurrentX + data.Datas[i].X + regionStartX] += data.Datas[i].Value;
                }
                for (int i = 0; i < lineNum; i++)
                {
 
                }
            }
            UpdateGameRegion();
        }

        /// <summary>
        /// Draw game region.
        /// </summary>
        private void DrawGameRegion()
        {
            for (int i = 0; i < lineNum - regionStartY; i++)
            {
                for (int j = regionStartX; j < colNum - regionStartX; j++)
                {
                    if (gameRegion[i * colNum + j] > 0)
                    {
                        Point drawPoint = GetDrawPoint(j - regionStartX, i, pixSize, (int)thisGraphics.VisibleClipBounds.Height);
                        thisGraphics.FillRectangle(new SolidBrush(Color.YellowGreen), drawPoint.X, drawPoint.Y, pixSize, pixSize);
                    }
                }
            }
        }
        #endregion
    }
}
