using Common;
using Common.Math;
using Data.DataFor3D;
using GraphicsTool.DrawTools3D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TestFor3DGraphic
{
    public partial class Form1 : Form
    {
        private Graphics graphicFor3D;

        private bool isHideByOtherObjects = false;

        List<Point3D> points3D = new List<Point3D>();
        List<int[]> planeIndex = new List<int[]>();

        Point3D projectPoint = new Point3D(new double[3] { 0, -200, 0 }, string.Empty);

        private double projectPointY = -200;

        private Cuboid toDraw;

        private Stage stage;
        public Form1()
        {
            InitializeComponent();
            List<Point3D> screenPoints = new List<Point3D>();
            screenPoints.Add(new Point3D(new double[3] { 0, 100, 100 }, string.Empty));
            screenPoints.Add(new Point3D(new double[3] { 0, 100, 0 }, string.Empty));
            screenPoints.Add(new Point3D(new double[3] { 100, 0, 0 }, string.Empty));
            screenPoints.Add(new Point3D(new double[3] { 100, 0, 100 }, string.Empty));
            Plane screen = new Plane(screenPoints, string.Empty);
            Point3D screenProjectPoint = new Point3D(new double[3] { 0, 0, 50 }, string.Empty);
            NormalViewer viewer = new NormalViewer(screen, screenProjectPoint);
            stage = new Stage(viewer);
            graphicFor3D = this.PanelForDrawing.CreateGraphics();
            PanelForDrawing.MouseWheel += PanelForDrawingMouseWheel;
            points3D.Add(new Point3D(new double[3] { 100, 112, 100 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 200, 112, 100 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 200, 212, 100 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 100, 212, 100 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 100, 112, 200 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 200, 112, 200 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 200, 212, 200 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 100, 212, 200 }, string.Empty));
            planeIndex.Add(new int[4] { 0, 1, 5, 4 });
            planeIndex.Add(new int[4] { 0, 4, 7, 3 });
            planeIndex.Add(new int[4] { 0, 1, 2, 3 });
            planeIndex.Add(new int[4] { 6, 7, 4, 5 });
            planeIndex.Add(new int[4] { 6, 5, 1, 2 });
            planeIndex.Add(new int[4] { 6, 7, 3, 2 });
            toDraw = new Cuboid(points3D, planeIndex, "object1");
            stage.AddData(toDraw);
            points3D.Clear();
            planeIndex.Clear();
            points3D.Add(new Point3D(new double[3] { 300, 112, 100 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 400, 112, 100 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 400, 212, 100 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 300, 212, 100 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 300, 112, 200 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 400, 112, 200 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 400, 212, 200 }, string.Empty));
            points3D.Add(new Point3D(new double[3] { 300, 212, 200 }, string.Empty));
            planeIndex.Add(new int[4] { 0, 1, 5, 4 });
            planeIndex.Add(new int[4] { 0, 4, 7, 3 });
            planeIndex.Add(new int[4] { 0, 1, 2, 3 });
            planeIndex.Add(new int[4] { 6, 7, 4, 5 });
            planeIndex.Add(new int[4] { 6, 5, 1, 2 });
            planeIndex.Add(new int[4] { 6, 7, 3, 2 });
            toDraw = new Cuboid(points3D, planeIndex, "object2");
            stage.AddData(toDraw);
            //graphicFor3D.DrawEllipse(new Pen(Color.Black), 100, 100, 100, 100);
            /*for (int i = 0 ; i < 8 ; i++)
            {
                double[] point2D = PointTransfer.TransferPointFrom3DTo2D(points3D[i], projectPoint);
                graphicFor3D.DrawEllipse(new Pen(Color.Black), (int)point2D[0], (int)point2D[1], 2, 2);
            }*/
        }

        private void BtnRefreshClick(object sender, EventArgs e)
        {
            /*ChangeProject(5, 0);*/
            PanelForDrawing.Focus();
            List<List<double[]>> result = stage.GetScreenPlaneOfOneObject("object1", this.PanelForDrawing.Height, isHideByOtherObjects);
            result.AddRange(stage.GetScreenPlaneOfOneObject("object2", this.PanelForDrawing.Height, isHideByOtherObjects));
            DrawPlaneInList(result);
        }

        private void PanelForDrawingMouseWheel(object sender, MouseEventArgs e)
        {
            //ChangeProject(projectPointY += e.Delta / 10, 1);
            /*ChangePosition(e.Delta / 10, MoveType.Y);
            RTBInfo.Text = string.Empty;
            RTBInfo.Text = toDraw.InfoString();*/
            stage.RotateTheViewrAroundProjectPoint(e.Delta / 10, MoveType.Z);
            List<List<double[]>> result = stage.GetScreenPlaneOfOneObject("object1", this.PanelForDrawing.Height, isHideByOtherObjects);
            result.AddRange(stage.GetScreenPlaneOfOneObject("object2", this.PanelForDrawing.Height, isHideByOtherObjects));
            DrawPlaneInList(result);
            RTBInfo.Text = string.Empty;
            RTBInfo.Text = stage.StageDataInfo("object1");
        }

        private void SCBVerticalScroll(object sender, ScrollEventArgs e)
        {
            //ChangeProject(e.NewValue * 3, 2);
            /*ChangePosition((e.NewValue - e.OldValue) * 3, MoveType.Z);
            RTBInfo.Text = string.Empty;
            RTBInfo.Text = toDraw.InfoString();*/
            //stage.MoveViewer((e.NewValue - e.OldValue) * 3, MoveType.Z);
            stage.GetRealData("object1").Rotate((e.NewValue - e.OldValue) * 5, MoveType.Z, new double[3] { 300, 212, 200 });
            stage.GetRealData("object2").Rotate((e.NewValue - e.OldValue) * 5, MoveType.Z, new double[3] { 300, 212, 200 });
            List<List<double[]>> result = stage.GetScreenPlaneOfOneObject("object1", this.PanelForDrawing.Height, isHideByOtherObjects);
            result.AddRange(stage.GetScreenPlaneOfOneObject("object2", this.PanelForDrawing.Height, isHideByOtherObjects));
            RTBInfo.Text = string.Empty;
            RTBInfo.Text = stage.StageDataInfo("object1") + "\n" + stage.StageDataInfo("object2");
            //result.AddRange(stage.GetScreenPlaneOfOneObject("object2", this.PanelForDrawing.Height));
            DrawPlaneInList(result);
        }

        private void SCBHorizonScroll(object sender, ScrollEventArgs e)
        {
            //ChangeProject(e.NewValue * 3, 0);
            /*ChangePosition((e.NewValue - e.OldValue) * 3, MoveType.X);
            RTBInfo.Text = string.Empty;
            RTBInfo.Text = toDraw.InfoString();*/
            stage.MoveViewer((e.NewValue - e.OldValue) * 3, MoveType.Z);
            List<List<double[]>> result = stage.GetScreenPlaneOfOneObject("object1", this.PanelForDrawing.Height, isHideByOtherObjects);
            result.AddRange(stage.GetScreenPlaneOfOneObject("object2", this.PanelForDrawing.Height, isHideByOtherObjects));
            DrawPlaneInList(result);
        }

        private void Form1KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    {
                        ChangeProject(50, 1);
                        break;
                    }
                case Keys.S:
                    {
                        ChangeProject(-50, 1);
                        break;
                    }
            }
            RTBInfo.Text = string.Empty;
            RTBInfo.Text = toDraw.InfoString();
        }

        private void ChangeProject(double value, int type)
        {
            graphicFor3D.Clear(Color.Gray);
            projectPoint.Data[type] = value;
            List<Point3D> result = toDraw.GetScreenPointWithHide(projectPoint, this.PanelForDrawing.Height);
            List<Plane> planes = toDraw.GetScreenPlaneWithHide(projectPoint, this.PanelForDrawing.Height);
            /*for (int i = 0; i < result.Count; i++)
            {
                graphicFor3D.DrawEllipse(new Pen(Color.Black), (float)result[i].Data[0], (float)result[i].Data[1], 2, 2);
            }*/
            for (int i = 0; i < planes.Count; i++)
            {
                PointF[] drawPoint = new PointF[4];
                for (int j = 0; j < 4; j++)
                {
                    drawPoint[j].X = (float)planes[i].DataPoint[j].Data[0];
                    drawPoint[j].Y = (float)planes[i].DataPoint[j].Data[1];
                }
                graphicFor3D.DrawPolygon(new Pen(Color.Red), drawPoint);
            }
        }

        private void ChangePosition(double value, MoveType type)
        {
            graphicFor3D.Clear(Color.Gray);
            toDraw.Move(value, type);
            //List<Point3D> result = toDraw.GetScreenPointWithHide(projectPoint, this.PanelForDrawing.Height);
            List<Plane> planes = toDraw.GetScreenPlaneWithHide(projectPoint, this.PanelForDrawing.Height);
            /*for (int i = 0; i < result.Count; i++)
            {
                graphicFor3D.DrawEllipse(new Pen(Color.Black), (float)result[i].Data[0], (float)result[i].Data[1], 2, 2);
            }*/
            for (int i = 0; i < planes.Count; i++)
            {
                PointF[] drawPoint = new PointF[4];
                for (int j = 0; j < 4; j++)
                {
                    drawPoint[j].X = (float)planes[i].DataPoint[j].Data[0];
                    drawPoint[j].Y = (float)planes[i].DataPoint[j].Data[1];
                }
                graphicFor3D.DrawPolygon(new Pen(Color.Red), drawPoint);
            }
        }

        private void DrawPlaneInList(List<List<double[]>> plane)
        {
            graphicFor3D.Clear(Color.Gray);
            for (int i = 0; i < plane.Count; i++)
            {
                PointF[] drawPoint = new PointF[4];
                for (int j = 0; j < 4; j++)
                {
                    drawPoint[j].X = (float)plane[i][j][0];
                    drawPoint[j].Y = (float)plane[i][j][1];
                }
                graphicFor3D.FillPolygon(new SolidBrush(Color.Red), drawPoint, System.Drawing.Drawing2D.FillMode.Winding);
            }
        }
    }
}
