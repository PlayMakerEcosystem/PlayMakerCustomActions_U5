// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Send event if you are inside or outside a 2D collider")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11673.0")]
	public class InsideCollider2DInfo : FsmStateAction
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



			Collider2D temp =  colliderTarget.Value as Collider2D;
			
			Bounds colliderBounds = temp.bounds;
			Sprite tempRen = go.GetComponent<Sprite>();
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
