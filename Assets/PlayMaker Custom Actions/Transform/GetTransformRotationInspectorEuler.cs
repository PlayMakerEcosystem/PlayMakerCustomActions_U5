// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the rotation euler angles from a gameobject, in the same way as the Unity inspector")]
	public class GetTransformRotationInspectorEuler : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.Variable)]
		[Title("Euler Angles")]
		public FsmVector3 vector;
		[UIHint(UIHint.Variable)]
		public FsmFloat xAngle;
		[UIHint(UIHint.Variable)]
		public FsmFloat yAngle;
		[UIHint(UIHint.Variable)]
		public FsmFloat zAngle;

		public bool everyFrame;

		
		Vector3 angle ;
		float x;
		float y;
		float z;
		
		public override void Reset()
		{
			gameObject = null;
			vector = null;
			xAngle = null;
			yAngle = null;
			zAngle = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetRotation();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}

		public override void OnUpdate()
		{
			DoGetRotation();
		}

		void DoGetRotation()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			angle = go.transform.eulerAngles;
			x = angle.x;
			y = angle.y; 
			z = angle.z;

			if (Vector3.Dot(go.transform.up, Vector3.up) >= 0f)
			{
				if (angle.x >= 0f && angle.x <= 90f)
				{
					x = angle.x;
				}
				if (angle.x >= 270f && angle.x <= 360f)
				{
					x = angle.x - 360f;
				}
			}
			if (Vector3.Dot(go.transform.up, Vector3.up) < 0f)
			{
				if (angle.x >= 0f && angle.x <= 90f)
				{
					x = 180 - angle.x;
				}
				if (angle.x >= 270f && angle.x <= 360f)
				{
					x = 180 - angle.x;
				}
			}

			if (angle.y > 180)
			{
				y = angle.y - 360f;
			}

			if (angle.z > 180)
			{
				z = angle.z - 360f;
			}

			angle.x = x;
			angle.y = y;
			angle.z = z;
			
			vector.Value = angle;
			xAngle.Value = x;
			yAngle.Value = y;
			zAngle.Value = z;
			//Debug.Log(angle + " :::: " + Mathf.Round(x) + " , " + Mathf.Round(y) + " , " + Mathf.Round(z));
		}


	}
}