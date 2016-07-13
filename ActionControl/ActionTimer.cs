using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ActionControl
{
    public class ActionTimer : IDisposable
    {
        #region Private Property
        /// <summary>
        /// Timer for action.
        /// </summary>
        private Thread actionTimerThread;

        /// <summary>
        /// Interval for timer, default value is 1 second (1000 millisecond)
        /// </summary>
        private int actionTimerInterval = 1000;

        /// <summary>
        /// Action delegate.
        /// </summary>
        private TimerAction actionForTimer = null;

        /// <summary>
        /// Make timer stop.
        /// </summary>
        private bool isTimerStop = true;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for timer.
        /// </summary>
        /// <param name="interval"></param>
        public ActionTimer(int interval, TimerAction timerAction)
        {
            actionTimerInterval = interval;
            actionForTimer = timerAction;
            actionTimerThread = new Thread(new ThreadStart(TimerStartFunctin));
            actionTimerThread.Start();
        }

        /// <summary>
        /// Start the timer.
        /// </summary>
        public void StartTimer()
        {
            if (actionTimerThread != null)
            {
                isTimerStop = false;
            }
        }

        /// <summary>
        /// Stop the timer.
        /// </summary>
        public void StopTimer()
        {
            if (actionTimerThread != null)
            {
                isTimerStop = true;
            }
        }

        /// <summary>
        /// Dispose the timer.
        /// </summary>
        public void Dispose()
        {
            actionTimerThread.Interrupt();
            actionTimerThread = null;
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Doing something when timer start.
        /// </summary>
        private void TimerStartFunctin()
        {
            if (actionForTimer != null)
            {
                while (true)
                {
                    try
                    {
                        if (!isTimerStop)
                        {
                            Thread.Sleep(actionTimerInterval);
                            actionForTimer();
                        }
                        else
                        {
                            Thread.Sleep(1);
                        }
                    }
                    catch (ThreadInterruptedException ex)
                    {
                        return;
                    }
                }
            }
        }
        #endregion
    }
}
