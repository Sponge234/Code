using System;
using System.Collections;
namespace homework
{
    public class TickArgs : EventArgs
    {
        public DateTime Time { get; set; }
        public TickArgs(DateTime t)
        {
            Time = t;
        }
    }

    public class AlarmArgs : EventArgs
    {
        public DateTime Time { get; set; }
        public AlarmArgs(DateTime t)
        {
            Time = t;
        }
    }

    public class Clock
    {
        private Timer TickTimer;
        private DateTime AlarmTime;
        private bool isRunning;

        public event EventHandler<TickArgs> Tick;
        public event EventHandler<AlarmArgs> Alarm;
        public Clock() 
        {
            TickTimer = new Timer(TickCallback, null, 0, 1000);
        }

        public void Start()
        {
            isRunning = true;
            Console.WriteLine($"闹钟已启动，当前时间：{DateTime.Now:HH:mm:ss}");
        }

        public void Stop()
        {
            isRunning = false;
            Console.WriteLine("闹钟已停止");
        }

        private void TickCallback(object state)
        {
            if (!isRunning) return;

            DateTime now = DateTime.Now;
            OnTick(now);

            if (now.Hour == AlarmTime.Hour &&
                now.Minute == AlarmTime.Minute &&
                now.Second == AlarmTime.Second)
            {
                OnAlarm(now);
                Stop();
            }
        }

        public void OnTick(DateTime time)
        {
            if (Tick != null)
            {
                Tick(this, new TickArgs(time));
            }
        }

        public void OnAlarm(DateTime time)
        {
            if (Alarm != null)
            {
                Alarm(this, new AlarmArgs(time));
            }
        }

        public void SetAlarm(int hour, int minute, int second)
        {
            AlarmTime = DateTime.Now.Date.AddHours(hour).AddMinutes(minute).AddSeconds(second);
            if (AlarmTime <= DateTime.Now)
                AlarmTime = AlarmTime.AddDays(1);
        }

    }
}
