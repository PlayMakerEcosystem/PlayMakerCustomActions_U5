// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
    [HelpUrl("")]
	[Tooltip("Sets the angular drag that applies to rotational movement and is set up separately from the linear drag that affects positional movement.")]
	public class SetAngularDrag : ComponentAction<Rigidbody>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[HasFloatSlider(0.0f,10f)]
		public FsmFloat drag;
		
		[Tooltip("Repeat every frame. Typically this would be set to True.")]
		public bool everyFrame;

		private Rigidbody rb;

		public override void Reset()
		{
			gameObject = null;
			drag = 1;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleFixedUpdate = true;
		}

		public override void OnEnter()
		{

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			rb = go.GetComponent<Rigidbody>();

			DoSetDrag();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnFixedUpdate()
		{
			DoSetDrag();

			if (!everyFrame)
			{
				Finish();
			}
		}

		void DoSetDrag()
		{
			if (!rb)
			{
				return;
			}
		   
			rb.angularDrag = drag.Value;
		}
	}
}

