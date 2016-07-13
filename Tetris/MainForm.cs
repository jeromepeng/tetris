using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ActionControl;
using Factories;
using Common;
using Common.Interface;
using GraphicsTool;
using Common.EventArgument;

namespace Tetris
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private ActionTimer actionTimer;

        private Graphics screenGraphics;

        private IDataGenerator dataGenerator;

        private IDrawTool drawTool;

        private void MainForm_Load(object sender, EventArgs e)
        {
            actionTimer = new ActionTimer(1000, new TimerAction(TestAction));
            screenGraphics = this.MainScreen.CreateGraphics();
            KeyDown += MainForm_KeyDown;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        drawTool.KeyAction(new ActionEventArgs() { Parameter1 = "", Parameter2 = ActionType.Up, Parameter3 = 1 });
                        break;
                    }
                case Keys.Down:
                    {
                        drawTool.KeyAction(new ActionEventArgs() { Parameter1 = "", Parameter2 = ActionType.Down, Parameter3 = 1 });
                        break;
                    }
                case Keys.Right:
                    {
                        drawTool.KeyAction(new ActionEventArgs() { Parameter1 = "", Parameter2 = ActionType.Right, Parameter3 = 1 });
                        break;
                    }
                case Keys.Left:
                    {
                        drawTool.KeyAction(new ActionEventArgs() { Parameter1 = "", Parameter2 = ActionType.Left, Parameter3 = 1 });
                        break;
                    }
            }
        }

        private void TestAction()
        {
            if (this.InvokeRequired)
            {
                EventHandler testAction = delegate
                {
                    TestAction();
                };
                this.Invoke(testAction);
            }
            else
            {
                //screenGraphics.Clear(MainScreen.BackColor);
                /*screenGraphics.DrawString(System.DateTime.Now.ToString(), new Font("Verdana", 12), new SolidBrush(Color.Black), new PointF(10, 10));*/
                drawTool.TimeAction(new ActionEventArgs() { Parameter1 = "", Parameter2 = ActionType.Down, Parameter3 = 1 });
                //drawTool.UpdateGameRegion();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            actionTimer.StopTimer();
            actionTimer.Dispose();
        }

        private void openGameFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dataGenerator = Factories.DataFactory.GetDataGenerator(GameType.Tetris);
                dataGenerator.InitialDatas(ofd.FileName);
                drawTool = Factories.GraphicsToolFactory.GetDrawTool(GameType.Tetris);
                drawTool.InitDrawData(dataGenerator.GetAllDatas(), screenGraphics, 4, 12, 30);
                actionTimer.StartTimer();
            }
        }

    }
}
