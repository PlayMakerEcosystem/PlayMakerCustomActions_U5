// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Keywords: timestamp

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Time)]
    [Tooltip("Read a epoch timestamp (Unix)")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=13541.0")]
    public class ReadEpoch : FsmStateAction
    {
        [ActionSection("Input")]
        [Tooltip("Unix epoch/timestamp")]
        public FsmInt getEpoch;

        public bool everyFrame;

        [ActionSection("Output")]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store epoch / timestamp as a string.")]
        public FsmString dateTime;
        [Tooltip("Optional format string. E.g., MM/dd/yyyy HH:mm")]
        public FsmString format;
        [Tooltip("Convert to local time")]
        public FsmBool localTime;

        public override void Reset()
        {
            getEpoch = null;
            dateTime = null;
            localTime = false;
            format = "MM/dd/yyyy HH:mm";
        }

        public override void OnEnter()
        {
            ReadEpochTime();
            if (!everyFrame) Finish();
        }

        public override void OnUpdate()
        {
            ReadEpochTime();
        }

        void ReadEpochTime()
        {
            if (localTime.Value == true)
            {
                var t = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((int)getEpoch.Value);
                dateTime.Value = TimeZone.CurrentTimeZone.ToLocalTime(t).ToString(format.Value);
            }
            else
            {
                DateTime unixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                dateTime.Value = unixDateTime.AddSeconds((int)getEpoch.Value).ToString(format.Value);
            }
        }
    }
}
