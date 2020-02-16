// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets the world space pose of the wheel accounting for ground contact, suspension limits, steer angle, and rotation angle (angles in degrees).")]
	public class WheelColliderGetWorldPose : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(WheelCollider))]
		[Tooltip("The GameObject with the WheelCollider component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("The WheelCollider World Position")]
		public FsmVector3 position;
			
		[UIHint(UIHint.Variable)]
		[Tooltip("The WheelCollider World rotation")]
		public FsmQuaternion rotation;
			
		[Tooltip("Matches the wheel Collider world Position and Rotation")]
		public FsmOwnerDefault transform;

		[Tooltip("Repeat every frame.")]		
		public bool everyFrame;
			
		WheelCollider _component;

		Vector3 _position;
		Quaternion _rotation;
		GameObject _dummy;

		public override void Reset()
		{
			gameObject = null;
			
			position = null;
			rotation = null;
			transform = new FsmOwnerDefault();
			transform.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			transform.GameObject = new FsmGameObject(){UseVariable=true};

			everyFrame = true;
		}
			
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_component = _go.GetComponent<WheelCollider>();
			}

			GetWorldPose();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
			
		public override void OnUpdate()
		{
			GetWorldPose();
		}
			
		void GetWorldPose()
		{
			if (_component==null)
			{
				return;
			}

			_component.GetWorldPose(out _position,out _rotation);
			if (!position.IsNone)
			{
				position.Value = _position;
			}
			if (!rotation.IsNone)
			{
				rotation.Value = _rotation;
			}

			_dummy = Fsm.GetOwnerDefaultTarget(transform);
			if (_dummy!=null)
			{
				_dummy.transform.position = _position;
				_dummy.transform.rotation = _rotation;
			}
		}
	}
}

