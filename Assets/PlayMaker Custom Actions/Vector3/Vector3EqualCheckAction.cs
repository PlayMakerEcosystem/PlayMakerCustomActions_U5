// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategoryAttribute (ActionCategory.Vector3)]
	[Tooltip ("Sends an Event if two Vector3 are equal (in wanted magnitude)")]
	public class Vector3EqualCheckAction : FsmStateAction
	{
		[RequiredFieldAttribute]
		[UIHintAttribute (UIHint.Variable)]
		[Tooltip ("First Vector3 to test.")]
		public FsmVector3 first;

		[RequiredFieldAttribute]
		[UIHintAttribute (UIHint.Variable)]
		[Tooltip ("Second Vector3 to test.")]
		public FsmVector3 second;

		[Tooltip ("Wanted magnitude for check.")]
		public FsmFloat magnitudeForEquality = 0.0001f;

		[Tooltip ("Event that will be sent if they are equal.")]
		public FsmEvent equalsEvent;

		[Tooltip ("Event that will be sent if they are NOT equal.")]
		public FsmEvent notEqualsEvent;

		[Tooltip ("Repeat every frame.")]
		public bool everyFrame;


		public override void Reset ()
		{
			first = Vector3.zero;
			second = Vector3.zero;
			magnitudeForEquality = 0.0001f;
			equalsEvent = null;
			notEqualsEvent = null;
			everyFrame = false;
		}

		public override void OnEnter ()
		{
			CheckVector3s ();
			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate ()
		{
			CheckVector3s ();
		}

		void CheckVector3s ()
		{
			if (first.IsNone || second.IsNone) {
				return;
			}
			if (V3Equal (first.Value, second.Value)) {
				Fsm.Event ((equalsEvent));
			} else {
				Fsm.Event (notEqualsEvent);
			}
			return;
		}

		public bool V3Equal (Vector3 a, Vector3 b)
		{
			return Vector3.SqrMagnitude (a - b) < magnitudeForEquality.Value;
		}
	}
}