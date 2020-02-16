// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Source http://hutonggames.com/playmakerforum/index.php?topic=10600.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Sets fix/constant velocity of a 2d Game Object.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10600.0")]
	public class SetVelocityConstantSpeed2d : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.FsmFloat)]
		[Tooltip("Velocity speed")]
		public FsmFloat speed;

		[UIHint(UIHint.FsmBool)]
		[Tooltip("Exit the action")]
		public FsmBool exitAction;

		private Rigidbody2D rb;
		
		public override void Reset()
		{
			gameObject = null;
			speed = 1f;
			exitAction = false;
		}
		
		public override void OnPreprocess()
		{
			Fsm.HandleFixedUpdate = true;
		}		

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			rb = go.GetComponent<Rigidbody2D>();
		}
		
		public override void OnFixedUpdate()
		{
			rb.velocity = speed.Value * (rb.velocity.normalized);

			if (exitAction.Value == true)
			{
				Finish();
			}
		}

	}
}
