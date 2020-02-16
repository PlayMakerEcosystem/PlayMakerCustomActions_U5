// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					  ]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Checks if an integer variable is between one or multiple ranges and sends an event accordingly. Since the ranges go in order, you should define the smallest int first (even if negative).")]
	public class IntCompareRange : FsmStateActionAdvanced
	{
		[RequiredField]
		[Tooltip("The Integer to compare with set ranges.")]
		public FsmInt integerValue;

		[CompoundArray("Range Amount", "Bigger Than", "Event")]
		[RequiredField]
		[Tooltip("If the given int is bigger than this and smaller than the next range, send the event.")]
		public FsmInt[] biggerThan;

		[RequiredField]
		[Tooltip("The event to send if in this range.")]
		public FsmEvent[] currEvent;

		[Tooltip("Send an event if the int is smaller than the first given range.")]
		public FsmEvent smallerThanFirst;

		public override void Reset()
		{
			base.Reset();

			integerValue = new FsmInt() { UseVariable = true };
			biggerThan = null;
			currEvent = null;
			smallerThanFirst = null;
		}

		public override void OnEnter()
		{
			DoIntCompareRange();

			if(!everyFrame) Finish();
		}

		public override void OnActionUpdate()
		{
			DoIntCompareRange();
		}

		void DoIntCompareRange()
		{
			if(biggerThan.Length == 0)
			{
				UnityEngine.Debug.LogError("No range has been set!");
				return;
			}

			//go through all ranges from behind
			for(int i = biggerThan.Length; i-- > 0;)
			{
				//loop through all previous ranges
				for(int j = 0; j < i; j++)
				{
					//if any range-value is bigger than the current one, throw an error
					if(biggerThan[j].Value >= biggerThan[i].Value)
					{
						UnityEngine.Debug.LogError("Range-Value #" + (i + 1) + " can't be smaller than/equal to #" + (j + 1) + "!");
						return;
					}
				}

				//if not on the last range
				if((i + 1) < biggerThan.Length)
				{
					//if the integer is in the current range, send the matching event
					if(integerValue.Value >= biggerThan[i].Value && integerValue.Value < biggerThan[i + 1].Value)
					{
						Fsm.Event(currEvent[i]);
					}
				} else
				{
					//if on last range and bigger than that
					if(integerValue.Value >= biggerThan[i].Value) Fsm.Event(currEvent[i]);
				}
			}

			//send the smallerThanFirst event if the integer is smaller than the first range
			if(integerValue.Value < biggerThan[0].Value) Fsm.Event(smallerThanFirst);
		}
	}
}
