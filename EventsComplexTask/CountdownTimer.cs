using System;

namespace EventsComplexTask
{
    public class CountdownTimer
    {
        public event EventHandler<int>? Tick;
        public event EventHandler? Finished;

        public void Start(int seconds)
        {
            if (seconds <= 0)
            {
                throw new ArgumentException("Время должно быть больше нуля.");
            }

            for (int remaining = seconds; remaining > 0; remaining--)
            {
                Tick?.Invoke(this, remaining);
            }

            Finished?.Invoke(this, EventArgs.Empty);
        }
    }
}

