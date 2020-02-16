// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Send event if you are inside or outside a collider")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11673.0")]
	public class InsideColliderInfo : FsmStateAction
	{
	
		public FsmObject colliderTarget;
		[UIHint(UIHint.Variable)]
		public FsmOwnerDefault gameObject;

		public FsmBool insideCollider;
		public FsmEvent insideEvent;
		public FsmEvent outsideEvent;

		public override void Reset()
		{
			colliderTarget = null;
			gameObject = null;
			insideCollider = null;
			insideEvent = null;
			outsideEvent =null;
		}
		

		public override void OnEnter()
		{

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go == null)
			{
				Finish();
			}


	

			Collider temp =  colliderTarget.Value as Collider;
			
			Bounds colliderBounds = temp.bounds;
			Renderer tempRen = go.GetComponent<Renderer>();
			Bounds myObj = tempRen.bounds;


			insideCollider.Value = colliderBounds.Intersects(myObj);

			if (insideCollider.Value ==  true)

			{
				Fsm.Event(insideEvent);
			}

			if (insideCollider.Value ==  false)
				
			{
				Fsm.Event(outsideEvent);
			}

			else {
			Finish();
			}
		}
	}
}
