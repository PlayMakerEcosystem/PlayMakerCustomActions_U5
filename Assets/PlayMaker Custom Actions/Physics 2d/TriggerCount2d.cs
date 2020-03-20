// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Detect collisions with objects that have RigidBody2D components. \nNOTE: The system events, TRIGGER ENTER 2D, TRIGGER STAY 2D, and TRIGGER EXIT 2D are sent when any 2D object collides with the trigger. Use this action to filter collisions by Tag.")]
	public class TriggerCount2d : FsmStateAction
	{
		[UIHint(UIHint.Tag)]
		public FsmString collideTag;

		public FsmInt collideCount;

		public FsmBool collidedBool;
		public bool everyFrame;

		public override void Reset()
		{
			collideTag = "Untagged";
			collideCount = null;
			collidedBool = null;
			everyFrame = false;
		}
		
		public override void OnPreprocess()
		{

			Fsm.HandleTriggerEnter2D = true;
					
			Fsm.HandleTriggerExit2D = true;
		
		}
		
		public override void OnEnter()
		{
			if (!everyFrame)
				Finish();
		}

		public override void DoTriggerEnter2D(Collider2D other)
			{
				if (other.gameObject.tag == collideTag.Value)
				{
					collideCount.Value = collideCount.Value + 1;
				}
			}

		public override void DoTriggerExit2D(Collider2D other)
			{
				if (other.gameObject.tag == collideTag.Value)
				{
					collideCount.Value = collideCount.Value - 1;
				}
			}
		
		public override void OnUpdate()
		{
			if (collideCount.Value == 0 )
			{
				collidedBool.Value = false;
			}
			else
			{
				collidedBool.Value = true;
			}
		}
		
	}
}
