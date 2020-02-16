// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Measures the Square Distance between 2 Game Objects and stores the result in a Float Variable. It's faster than distance, so for relative comparision, it's preferable")]
	public class GetSqrMagnitude : FsmStateAction
	{
		[RequiredField]
        [Tooltip("Measure distance from this GameObject.")]
		public FsmOwnerDefault gameObject;
		
        [RequiredField]
		[Tooltip("Target GameObject.")]
        public FsmGameObject target;
		
        [RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the square distance in a float variable.")]
		public FsmFloat storeResult;
		
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			target = null;
			storeResult = null;
			everyFrame = true;
		}
		
		public override void OnEnter()
		{
			DoGetDistance();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}
		public override void OnUpdate()
		{
			DoGetDistance();
		}		
		
		void DoGetDistance()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (go == null || target.Value == null || storeResult == null)
		    {
		        return;
		    }
					
			storeResult.Value = Vector3.SqrMagnitude(go.transform.position-target.Value.transform.position);
		}

	}
}