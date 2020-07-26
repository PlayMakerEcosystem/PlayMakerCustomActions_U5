// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.
// author : ransomink
// Keywords: 2d, get, gravity, physics
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Get the gravity as a Vector2 Variable, or store the XY channels in Float Variables.")]
	public class GetGravity2d : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmVector2 storeVector;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeX;
		[UIHint(UIHint.Variable)]
		public FsmFloat StoreY;
		public bool everyFrame;
		
		public override void Reset()
		{
			storeVector = null;
			storeX = new FsmFloat { UseVariable = true };
			StoreY = new FsmFloat { UseVariable = true };
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetGravity();
			
			if (!everyFrame) Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetGravity();
		}
		
		void DoGetGravity()
		{
			Vector2 gravity = Physics2D.gravity;
			
			if (!storeX.IsNone) storeX.Value = gravity.x;
			if (!StoreY.IsNone) StoreY.Value = gravity.y;
			
			storeVector.Value = gravity;
		}
	}
}

