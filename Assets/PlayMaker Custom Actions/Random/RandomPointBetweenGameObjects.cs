// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Random")]
	[Tooltip("Returns a random point on the distance between two GameObjects.")]
	public class RandomPointBetweenGameObjects : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The starting point of the slope.")]
		public FsmGameObject firstGameObject;

		[RequiredField]
		[Tooltip("The last point on the imaginary slope.")]
		public FsmGameObject secondGameObject;

		[Tooltip("Wheter to return the random point in local or world space.")]
		public Space space;

		[UIHint(UIHint.Variable)]
		[Tooltip("A random result between the given points.")]
		public FsmVector3 storePoint;

		[Tooltip("Wheter to run every frame.")]
		public FsmBool everyFrame;

		public override void Reset()
		{
			firstGameObject = null;
			secondGameObject = null;
			storePoint = null;
		}

		public override void OnEnter()
		{
			DoGetPointOnSlope();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			DoGetPointOnSlope();
		}

		void DoGetPointOnSlope()
		{
			Vector3 firstPos = space == Space.World ? firstGameObject.Value.transform.position 
													: firstGameObject.Value.transform.localPosition;

			Vector3 secondPos = space == Space.World ? secondGameObject.Value.transform.position
													: secondGameObject.Value.transform.localPosition;

			storePoint.Value = Random.Range(0, Vector3.Distance(firstPos, secondPos))
							 * Vector3.Normalize(secondPos - firstPos) 
							 + firstPos;
		}
	}
}
