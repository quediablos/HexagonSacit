using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace HexagonSacit
{


    /// <summary>
    /// Includes all the timers of a unit
    /// </summary>
    public class TimerVault
    {
        Dictionary<string, AccurateTimer> timers = new Dictionary<string, AccurateTimer>();

        /// <summary>
        /// Updates all the included timers
        /// </summary>
        public void update()
        {
            foreach (KeyValuePair<string, AccurateTimer> pair in timers)
            {
                pair.Value.update();
            }
        }

        public AccurateTimer get(string timerName)
        {
            return timers[timerName];
        }

        public void add(string timerName, AccurateTimer timer)
        {
            timers.Add(timerName, timer);
        }
    }
}