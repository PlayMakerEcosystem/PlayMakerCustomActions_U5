// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Set the center of a Sphere Collider")]
	[HelpUrl("")]
	public class SetSphereColliderCenter : ComponentAction<Rigidbody>
	{
		[RequiredField]
		[CheckForComponent(typeof(SphereCollider))]
        [Tooltip("The GameObject to apply the center to.")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("A Vector3 center to add. Optionally override any axis with the X, Y, Z parameters.")]
		public FsmVector3 vector;

        [Tooltip("center along the X axis. To leave unchanged, set to 'None'.")]
		public FsmFloat x;

        [Tooltip("center along the Y axis. To leave unchanged, set to 'None'.")]
		public FsmFloat y;

        [Tooltip("center along the Z axis. To leave unchanged, set to 'None'.")]
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
			SphereCollider collider = go.GetComponent<SphereCollider>();



			var center = vector.IsNone ? new Vector3(x.Value, y.Value, z.Value) : vector.Value;

			if (!x.IsNone) center.x = x.Value;
			if (!y.IsNone) center.y = y.Value;
			if (!z.IsNone) center.z = z.Value;
					
			collider.center = center;
	
		}
	}
}
