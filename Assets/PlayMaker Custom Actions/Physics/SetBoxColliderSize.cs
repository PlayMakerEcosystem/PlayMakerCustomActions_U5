// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Set the size of boxCollider

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Set the size of a Box Collider")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12040.0")]
	public class SetBoxColliderSize : ComponentAction<Rigidbody>
	{
		[RequiredField]
		[CheckForComponent(typeof(BoxCollider))]
        [Tooltip("The GameObject to apply the size to.")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("A Vector3 size to add. Optionally override any axis with the X, Y, Z parameters.")]
		public FsmVector3 vector;

        [Tooltip("size along the X axis. To leave unchanged, set to 'None'.")]
		public FsmFloat x;

        [Tooltip("size along the Y axis. To leave unchanged, set to 'None'.")]
		public FsmFloat y;

        [Tooltip("size along the Z axis. To leave unchanged, set to 'None'.")]
		public FsmFloat z;

        [Tooltip("Repeat every frame while the state is active.")]
        public FsmBool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			z = new FsmFloat { UseVariable = true };
			everyFrame = false;
		}


		public override void OnEnter()
		{
			DoChange();
			
			if (!everyFrame.Value)
			{
				Finish();
			}		
		}

		public override void OnFixedUpdate()
		{
			DoChange();
		}

		void DoChange()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			BoxCollider collider = go.GetComponent<BoxCollider>();

			var size = vector.IsNone ? new Vector3(x.Value, y.Value, z.Value) : vector.Value;

			if (!x.IsNone) size.x = x.Value;
			if (!y.IsNone) size.y = y.Value;
			if (!z.IsNone) size.z = z.Value;
					
			collider.size = size;
	
		}
	}
}
