// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					  ]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Get the rotation of a Game Object and add an offset to that Quaternion. Optionally applies that offset to another/the same GameObject.")]
	public class GetRotationAddOffset : FsmStateActionAdvanced
	{
		public enum QuaternionOperation
		{
			Add,
			Subtract,
			Multiply,
			Divide,
			Set
		}

		[RequiredField]
		[Tooltip("The GameObject to get the rotation off of.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Use a Vector3 variable or specify each axis individually.")]
		public FsmVector3 vector3Offset;

		public FsmFloat xOffset;

		public FsmFloat yOffset;

		public FsmFloat zOffset;

		[Tooltip("Wheter to use local or world space.")]
		public Space space;

		public QuaternionOperation operation;

		[UIHint(UIHint.Variable)]
		[Tooltip("The starting rotation with offset.")]
		public FsmVector3 storeVector3Result;

		[Tooltip("If the GameObject's rotation should be changed with the offset. (the Value of 'Store Vector3 Result' will be the new rotation)")]
		public FsmGameObject applyOffsetToGO;

		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			vector3Offset = new FsmVector3() { UseVariable = true };
			xOffset = null;
			yOffset = null;
			zOffset = null;
			space = Space.Self;
			operation = QuaternionOperation.Add;
			storeVector3Result = null;
			applyOffsetToGO = new FsmGameObject() { UseVariable = true };
		}

		public override void OnEnter()
		{
			DoGetRotation();

			if(!everyFrame) Finish();
		}

		public override void OnActionUpdate()
		{
			DoGetRotation();
		}

		void DoGetRotation()
		{
			Quaternion quat = new Quaternion();
			float x = xOffset.Value;
			float y = yOffset.Value;
			float z = zOffset.Value;

			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if(go == null) return;

			if(!vector3Offset.IsNone)
			{
				if(vector3Offset.Value.x != 0) x = vector3Offset.Value.x;
				if(vector3Offset.Value.y != 0) y = vector3Offset.Value.y;
				if(vector3Offset.Value.z != 0) z = vector3Offset.Value.z;
			}

			var rotation = space == Space.World ? go.transform.rotation : go.transform.localRotation;
			var input = new Quaternion(x, y, z, rotation.w);

			switch(operation)
			{
				case QuaternionOperation.Add:
					quat = rotation * input;
					storeVector3Result.Value = new Vector3(quat.x, quat.y, quat.z);
					break;
				case QuaternionOperation.Subtract:
					quat = rotation * Quaternion.Inverse(input);
					storeVector3Result.Value = new Vector3(quat.x, quat.y, quat.z);
					break;
				case QuaternionOperation.Multiply:
					var multResult = Vector3.zero;
					if(x != 0) multResult.x = rotation.x * x;
					if(y != 0) multResult.y = rotation.y * y;
					if(z != 0) multResult.z = rotation.z * z;
					storeVector3Result.Value = multResult;
					quat = new Quaternion(multResult.x, multResult.y, multResult.z, rotation.w);
					break;
				case QuaternionOperation.Divide:
					var divResult = Vector3.zero;
					if(x != 0) divResult.x = rotation.x / x;
					if(y != 0) divResult.y = rotation.y / y;
					if(z != 0) divResult.z = rotation.z / z;
					storeVector3Result.Value = divResult;
					quat = new Quaternion(divResult.x, divResult.y, divResult.z, rotation.w);
					break;
				case QuaternionOperation.Set:
					storeVector3Result.Value = new Vector3(x, y, z);
					quat = input;
					break;
			}

			if(!applyOffsetToGO.IsNone)
			{
				if(space == Space.World) applyOffsetToGO.Value.transform.rotation = quat;
				else applyOffsetToGO.Value.transform.localRotation = quat;
			}
		}
	}
}
