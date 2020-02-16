// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Performs most possible operations on 2 Vector3: Dot product, Cross product, Distance, Angle, Project, Reflect, Add, Subtract, Multiply, Divide, Min, Max. Advanced lets you specify the Update type (when to perform the action).")]
	public class Vector3OperatorAdvanced : FsmStateActionAdvanced
	{
		public enum Vector3Operation
		{
			DotProduct,
			CrossProduct,
			Distance,
			Angle,
			Project,
			Reflect,
			Add,
			Subtract,
			Multiply,
			Divide,
			Min,
			Max
		}

		[RequiredField]
		public FsmVector3 vector1;
		[RequiredField]
		public FsmVector3 vector2;
		public Vector3Operation operation = Vector3Operation.Add;

		[UIHint(UIHint.Variable)]
		public FsmVector3 storeVector3Result;

		[UIHint(UIHint.Variable)]
		public FsmFloat storeFloatResult;

		public override void Reset()
		{
			base.Reset();

			vector1 = null;
			vector2 = null;
			operation = Vector3Operation.Add;
			storeVector3Result = null;
			storeFloatResult = null;
		}

		public override void OnActionUpdate()
		{
			DoVector3Operator();
		}

		void DoVector3Operator()
		{
			var v1 = vector1.Value;
			var v2 = vector2.Value;

			switch(operation)
			{
				case Vector3Operation.DotProduct:
					storeFloatResult.Value = Vector3.Dot(v1, v2);
					break;

				case Vector3Operation.CrossProduct:
					storeVector3Result.Value = Vector3.Cross(v1, v2);
					break;

				case Vector3Operation.Distance:
					storeFloatResult.Value = Vector3.Distance(v1, v2);
					break;

				case Vector3Operation.Angle:
					storeFloatResult.Value = Vector3.Angle(v1, v2);
					break;

				case Vector3Operation.Project:
					storeVector3Result.Value = Vector3.Project(v1, v2);
					break;

				case Vector3Operation.Reflect:
					storeVector3Result.Value = Vector3.Reflect(v1, v2);
					break;

				case Vector3Operation.Add:
					storeVector3Result.Value = v1 + v2;
					break;

				case Vector3Operation.Subtract:
					storeVector3Result.Value = v1 - v2;
					break;

				case Vector3Operation.Multiply:
					var multResult = Vector3.zero;
					multResult.x = v1.x * v2.x;
					multResult.y = v1.y * v2.y;
					multResult.z = v1.z * v2.z;
					storeVector3Result.Value = multResult;
					break;

				case Vector3Operation.Divide:
					var divResult = Vector3.zero;
					divResult.x = v1.x / v2.x;
					divResult.y = v1.y / v2.y;
					divResult.z = v1.z / v2.z;
					storeVector3Result.Value = divResult;
					break;

				case Vector3Operation.Min:
					storeVector3Result.Value = Vector3.Min(v1, v2);
					break;

				case Vector3Operation.Max:
					storeVector3Result.Value = Vector3.Max(v1, v2);
					break;
			}
		}
	}
}
