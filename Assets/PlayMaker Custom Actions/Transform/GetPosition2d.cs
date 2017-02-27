// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the Position of a Game Object and stores it in a vector2 Variable or each Axis in a Float Variable")]
	public class GetPosition2d : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmVector2 vector_2d;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat x;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat y;
		
		public Space space;
		
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			vector_2d = null;
			x = null;
			y = null;
			space = Space.World;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetPosition();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}

		public override void OnUpdate()
		{
			DoGetPosition();
		}

		void DoGetPosition()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			var position = space == Space.World ? go.transform.position : go.transform.localPosition;				
			
			vector_2d.Value = position;
			x.Value = position.x;
			y.Value = position.y;
		}


	}
}
