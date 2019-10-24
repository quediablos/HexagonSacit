using UnityEngine;
using UnityEditor;

namespace HexagonSacit
{

    /// <summary>
    /// Represents time value for the units and updated in each frame.
    /// All time values are in terms of seconds.
    /// </summary>
    public class AccurateTimer
    {
        private const float COMPARISON_MARGIN = 0.0001f;

        public float time;
        public float limit;
        public bool hasLimit;
        private bool frozen;

        private AccurateTimer()
        {
            this.time = 0;
            this.frozen = false;
        }

        public static AccurateTimer createWithLimit(float limit)
        {
            AccurateTimer timer = new AccurateTimer();
            timer.limit = limit;
            timer.hasLimit = true;
            return timer;
        }

        public static AccurateTimer createWithoutLimit()
        {
            AccurateTimer timer = new AccurateTimer();
            timer.limit = 0f;
            timer.hasLimit = false;
            return timer;
        }

        public void update()
        {
            if (!frozen)
            {
                if (hasLimit)
                {
                    if (!isFinished())
                        time += Time.deltaTime;
                }
                else
                {
                    time += Time.deltaTime;
                }
            }

        }

        public bool isAt(float value, float margin = COMPARISON_MARGIN)
        {
            return Mathf.Abs(time - value) <= COMPARISON_MARGIN;
        }

        public bool isAfter(float value)
        {
            return (time - value) > COMPARISON_MARGIN;
        }

        public bool isBefore(float value)
        {
            return (value - time) > COMPARISON_MARGIN;
        }

        public bool isFinished()
        {
            if (!hasLimit)
                return false;

            return isAt(limit) || isAfter(limit);
        }

        public void setAt(float value)
        {
            time = value;
        }

        public AccurateTimer reset()
        {
            time = 0;
            return this;
        }

        public AccurateTimer freeze(bool freeze)
        {
            this.frozen = freeze;
            return this;
        }
    }
}