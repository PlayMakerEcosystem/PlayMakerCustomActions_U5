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
	[Tooltip("Get the position of a Game Object and add an offset to that Vector. Optionally applies that offset to another/the same GameObject.")]
	public class GetPositionAddOffset : FsmStateActionAdvanced
	{
		public enum Vector3Operation
		{
			Add,
			Subtract,
			Multiply,
			Divide,
			Set
		}

		[RequiredField]
		[Tooltip("The GameObject to get the position off of.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Use a Vector3 variable or specify each axis individually.")]
		public FsmVector3 vector3Offset;

		public FsmFloat xOffset;

		public FsmFloat yOffset;

		public FsmFloat zOffset;

		[Tooltip("Wheter to use local or world space.")]
		public Space space;

		public Vector3Operation operation;

		[UIHint(UIHint.Variable)]
		[Tooltip("The starting position with offset.")]
		public FsmVector3 storeVector3Result;

		[Tooltip("If the GameObject's position should be changed with the offset. (the Value of 'Store Vector3 Result' will be the new position)")]
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
			operation = Vector3Operation.Add;
			storeVector3Result = null;
			applyOffsetToGO = new FsmGameObject() { UseVariable = true };
		}

		public override void OnEnter()
		{
			DoGetPosition();

			if(!everyFrame) Finish();
		}

		public override void OnActionUpdate()
		{
			DoGetPosition();
		}

		void DoGetPosition()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if(go == null) return;

			if(!vector3Offset.IsNone)
			{
				if(vector3Offset.Value.x != 0) xOffset.Value = vector3Offset.Value.x;
				if(vector3Offset.Value.y != 0) yOffset.Value = vector3Offset.Value.y;
				if(vector3Offset.Value.z != 0) zOffset.Value = vector3Offset.Value.z;
			}

			var position = space == Space.World ? go.transform.position : go.transform.localPosition;
			var input = new Vector3(xOffset.Value, yOffset.Value, zOffset.Value);

			switch(operation)
			{
				case Vector3Operation.Add:
					storeVector3Result.Value = position + input;
					break;
				case Vector3Operation.Subtract:
					storeVector3Result.Value = position - input;
					break;
				case Vector3Operation.Multiply:
					var multResult = Vector3.zero;
					if(xOffset.Value != 0) multResult.x = position.x * xOffset.Value;
					if(yOffset.Value != 0) multResult.y = position.y * yOffset.Value;
					if(zOffset.Value != 0) multResult.z = position.z * zOffset.Value;
					storeVector3Result.Value = multResult;
					break;
				case Vector3Operation.Divide:
					var divResult = Vector3.zero;
					if(xOffset.Value != 0) divResult.x = position.x / xOffset.Value;
					if(yOffset.Value != 0) divResult.y = position.y / yOffset.Value;
					if(zOffset.Value != 0) divResult.z = position.z / zOffset.Value;
					storeVector3Result.Value = divResult;
					break;
				case Vector3Operation.Set:
					storeVector3Result.Value = input;
					break;
			}

			if(!applyOffsetToGO.IsNone)
			{
				if(space == Space.World) applyOffsetToGO.Value.transform.position = storeVector3Result.Value;
				else applyOffsetToGO.Value.transform.localPosition = storeVector3Result.Value;
			}
		}
	}
}
