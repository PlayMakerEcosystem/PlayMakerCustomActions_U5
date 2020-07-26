

// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.
// author : ransomink
// Keywords: 3d, get, gravity, physics
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Get the gravity as a Vector3 Variable, or store the XYZ channels in Float Variables.")]
	public class GetGravity : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeVector;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeX;
		[UIHint(UIHint.Variable)]
		public FsmFloat StoreY;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeZ;
		public bool everyFrame;
		
		public override void Reset()
		{
			storeVector = null;
			storeX = new FsmFloat { UseVariable = true };
			StoreY = new FsmFloat { UseVariable = true };
			storeZ = new FsmFloat { UseVariable = true };
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
			Vector3 gravity = Physics.gravity;
			
			if (!storeX.IsNone) storeX.Value = gravity.x;
			if (!StoreY.IsNone) StoreY.Value = gravity.y;
			if (!storeZ.IsNone) storeZ.Value = gravity.z;
			
			storeVector.Value = gravity;
		}
	}
}

