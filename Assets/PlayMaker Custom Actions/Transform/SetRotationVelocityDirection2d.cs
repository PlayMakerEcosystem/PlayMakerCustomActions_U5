// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets the Rotation of a Game Object to the force / velocity direction 2D - NOTE: sprite at 0.0.0 should face right")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11284.0")]
	public class SetRotationVelocityDirection2d : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		[CheckForComponent(typeof(Rigidbody2D))]
		public FsmOwnerDefault gameObject;

		[ActionSection("Update Setup")]
		public updateType updateTypeSelect;
		public enum updateType
		{
			Update,
			FixedUpdate,
			LateUpdate
		};


		public FsmBool everyFrame;
	
		public override void Reset()
		{
			gameObject = null;
			updateTypeSelect = updateType.FixedUpdate;
			everyFrame = true;
		
		}

		public override void OnPreprocess()
		{
			if (updateTypeSelect == updateType.FixedUpdate )
			{
				Fsm.HandleFixedUpdate = true;
			}

			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateTypeSelect == updateType.LateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		public override void OnEnter()
		{
			
			Doit();

		}

		public override void OnUpdate()
		{
			if (updateTypeSelect == updateType.Update )
			{
				Doit();
			}
		}
		public override void OnLateUpdate()
		{
			if (updateTypeSelect == updateType.LateUpdate )
			{
				Doit();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateTypeSelect == updateType.FixedUpdate )
			{
				Doit();
			}
		}

		void Doit()
		{
			var _target = Fsm.GetOwnerDefaultTarget(gameObject);

			Vector2 dir = _target.GetComponent<Rigidbody2D>().velocity;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			_target.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

			if (!everyFrame.Value)
			{
				Finish();
			}

		}




	}
}
