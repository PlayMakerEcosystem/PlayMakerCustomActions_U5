using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Time)]
    [Tooltip("Compares two time values (given in String datatype in HH:mm:ss format) and gives back its difference converted into Float datatype.")]
    public class TimeDiffCalculator : FsmStateAction
    {
        [Tooltip("Starting time")]
        public FsmString StartTime;

        [Tooltip("Ending time")]
        public FsmString EndTime;

        [Tooltip("The difference of ending time subtracting starting time, stored in seconds (float datatype).")]
        public FsmFloat Result;

        public override void OnEnter()
        {
            if (FsmString.IsNullOrEmpty(StartTime))
            {
                if (FsmString.IsNullOrEmpty(EndTime))
                {
                    var start = DateTime.Parse(StartTime.ToString());
                    var end = DateTime.Parse(EndTime.ToString());

                    Debug.Log("start is " + start + " and end is " + end);

                    Result = end.Subtract(start).Seconds;
                    Debug.Log("Result is " + Result);
                }
            }
            Finish();
        }

        public override void Reset()
        {
            StartTime = string.Empty;
            EndTime = string.Empty;
            Result = float.NaN;
        }
    }
}
