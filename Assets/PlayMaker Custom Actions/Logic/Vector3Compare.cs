//License: Attribution 4.0 International (CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Sends Events based on the comparison of 2 Vector3s (checks which one is closer to the center point (0, 0, 0), to detect, which one is bigger or smaller).")]
	public class Vector3Compare : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The first Vector3 variable.")]
		public FsmVector3 firstVector3;

		[RequiredField]
		[Tooltip("The second Vector3 variable.")]
		public FsmVector3 secondVector3;

		[RequiredField]
		[Tooltip("Tolerance for the Equal test (almost equal).")]
		public FsmFloat tolerance;

		[Tooltip("Event sent if Vector3 1 equals Vector3 2 (within Tolerance)")]
		public FsmEvent equal;

		[Tooltip("Event sent if Vector3 1 doesn't equal Vector3 2 (within Tolerance)")]
		public FsmEvent notEqual;

		[Tooltip("Event sent if Vector3 1 is less than Vector3 2")]
		public FsmEvent lessThan;

		[Tooltip("Event sent if Vector3 1 is greater than Vector3 2")]
		public FsmEvent greaterThan;

		[Tooltip("Repeat every frame. Useful if the variables are changing and you're waiting for a particular result.")]
		public bool everyFrame;

		public override void Reset()
		{
			firstVector3 = new FsmVector3();
			secondVector3 = new FsmVector3();
			tolerance = 0f;
			equal = null;
			notEqual = null;
			lessThan = null;
			greaterThan = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoCompare();

			if(!everyFrame) Finish();
		}

		public override void OnUpdate()
		{
			DoCompare();
		}

		void DoCompare()
		{
			if(Mathf.Abs(firstVector3.Value.x - secondVector3.Value.x) <= tolerance.Value
				&& Mathf.Abs(firstVector3.Value.y - secondVector3.Value.y) <= tolerance.Value
				&& Mathf.Abs(firstVector3.Value.z - secondVector3.Value.z) <= tolerance.Value)
			{
				Fsm.Event(equal);
			} else
			{
				Fsm.Event(notEqual);
			}

			//--- first concept: comparing each axis against each other (bound to give flawed results) ---

			//if(firstVector3.Value.x < secondVector3.Value.x
			//	|| firstVector3.Value.y < secondVector3.Value.y
			//	|| firstVector3.Value.z < secondVector3.Value.z)
			//{
			//	Fsm.Event(lessThan);
			//	return;
			//}

			//if(firstVector3.Value.x > secondVector3.Value.x
			//	|| firstVector3.Value.y > secondVector3.Value.y
			//	|| firstVector3.Value.z > secondVector3.Value.z)
			//{
			//	Fsm.Event(greaterThan);
			//}

			//--- ---- ----

			float firstDist = Vector3.Distance(new Vector3(0, 0, 0), firstVector3.Value);
			float secondDist = Vector3.Distance(new Vector3(0, 0, 0), secondVector3.Value);

			if(firstDist > secondDist)
				Fsm.Event(greaterThan);
			else
				Fsm.Event(lessThan);
		}

		public override string ErrorCheck()
		{
			if(FsmEvent.IsNullOrEmpty(equal) &&
				FsmEvent.IsNullOrEmpty(lessThan) &&
				FsmEvent.IsNullOrEmpty(greaterThan))
				return "Action sends no events!";

			return "";
		}
	}
}