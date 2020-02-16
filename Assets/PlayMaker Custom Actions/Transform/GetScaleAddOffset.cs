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
	[Tooltip("Get the scale of a Game Object and add an offset to that Vector. Optionally applies that offset to another/the same GameObject.")]
	public class GetScaleAddOffset : FsmStateActionAdvanced
	{
		public enum Vector3Operation
		{
			Add,
			Subtract,
			Multiply,
			Divide
		}

		[RequiredField]
		[Tooltip("The GameObject to get the scale off of.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Use a Vector3 variable or specify each axis individually.")]
		public FsmVector3 vector3Offset;

		public FsmFloat xOffset;

		public FsmFloat yOffset;

		public FsmFloat zOffset;

		public Vector3Operation operation;

		[UIHint(UIHint.Variable)]
		[Tooltip("The starting scale with Offset.")]
		public FsmVector3 storeVector3Result;

		[Tooltip("If the GameObject's scale should be changed with the offset. (the Value of 'Store Vector3 Result' will be the new scale)")]
		public FsmGameObject applyOffsetToGO;

		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			vector3Offset = new FsmVector3() { UseVariable = true };
			xOffset = null;
			yOffset = null;
			zOffset = null;
			operation = Vector3Operation.Add;
			storeVector3Result = null;
			applyOffsetToGO = new FsmGameObject() { UseVariable = true };
		}

		public override void OnEnter()
		{
			DoGetScale();
			if(!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoGetScale();
		}

		void DoGetScale()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if(go == null)
			{
				return;
			}

			if(vector3Offset != null && !vector3Offset.IsNone)
			{
				xOffset.Value = vector3Offset.Value.x;
				yOffset.Value = vector3Offset.Value.y;
				zOffset.Value = vector3Offset.Value.z;
			}

			var scale = go.transform.localScale;
			var input = new Vector3(xOffset.Value, yOffset.Value, zOffset.Value);

			switch(operation)
			{
				case Vector3Operation.Add:
					storeVector3Result.Value = scale + input;
					break;
				case Vector3Operation.Subtract:
					storeVector3Result.Value = scale - input;
					break;
				case Vector3Operation.Multiply:
					var multResult = Vector3.zero;
					multResult.x = scale.x * xOffset.Value;
					multResult.y = scale.y * yOffset.Value;
					multResult.z = scale.z * zOffset.Value;
					storeVector3Result.Value = multResult;
					break;
				case Vector3Operation.Divide:
					var divResult = Vector3.zero;
					divResult.x = scale.x / xOffset.Value;
					divResult.y = scale.y / yOffset.Value;
					divResult.z = scale.z / zOffset.Value;
					storeVector3Result.Value = divResult;
					break;

			}

			if(!applyOffsetToGO.IsNone)
			{
				applyOffsetToGO.Value.transform.localScale = storeVector3Result.Value;
			}
		}
	}
}
