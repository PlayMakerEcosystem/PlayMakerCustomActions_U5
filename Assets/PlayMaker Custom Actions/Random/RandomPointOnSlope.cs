// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Random")]
	[Tooltip("Returns a random point between two Vector3 variables. Useful for 2.5D games, e.g. if you want GameObjects to spawn further down the farther away they are or vice versa.")]
	public class RandomPointOnSlope : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The starting point of the slope.")]
		public FsmVector3 slopeStart;

		[RequiredField]
		[Tooltip("The last point on the imaginary slope.")]
		public FsmVector3 slopeEnd;

		[UIHint(UIHint.Variable)]
		[Tooltip("A random result between the given points.")]
		public FsmVector3 storePoint;

		[Tooltip("Wheter to run every frame.")]
		public FsmBool everyFrame;

		public override void Reset()
		{
			slopeStart = null;
			slopeEnd = null;
			storePoint = null;
		}

		public override void OnEnter()
		{
			DoGetPointOnSlope();

			if(!everyFrame.Value)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetPointOnSlope();
		}

		void DoGetPointOnSlope()
		{
			storePoint.Value = Random.Range(0, Vector3.Distance(slopeStart.Value, slopeEnd.Value))
							 * Vector3.Normalize(slopeEnd.Value - slopeStart.Value)
							 + slopeStart.Value;
		}
	}
}
