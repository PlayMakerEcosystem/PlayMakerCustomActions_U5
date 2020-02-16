// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Performs most possible operations on 2 Vector2: Dot product, Distance, Angle, Add, Subtract, Multiply, Divide, Min, Max. Advanced version lets you choose the Update type (when to perform the action).")]
	public class Vector2OperatorAdvanced : FsmStateActionAdvanced
	{

		public enum Vector2Operation
		{
			DotProduct,
			Distance,
			Angle,
			Add,
			Subtract,
			Multiply,
			Divide,
			Min,
			Max
		}

		[RequiredField]
		[Tooltip("The first vector")]
		public FsmVector2 vector1;

		[RequiredField]
		[Tooltip("The second vector")]
		public FsmVector2 vector2;

		[Tooltip("The operation")]
		public Vector2Operation operation = Vector2Operation.Add;

		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2 result when it applies.")]
		public FsmVector2 storeVector2Result;

		[UIHint(UIHint.Variable)]
		[Tooltip("The float result when it applies")]
		public FsmFloat storeFloatResult;

		public override void Reset()
		{
			base.Reset();

			vector1 = null;
			vector2 = null;
			operation = Vector2Operation.Add;
			storeVector2Result = null;
			storeFloatResult = null;
		}


		public override void OnActionUpdate()
		{
			DoVector2Operator();
		}

		void DoVector2Operator()
		{
			var v1 = vector1.Value;
			var v2 = vector2.Value;

			switch(operation)
			{
				case Vector2Operation.DotProduct:
					storeFloatResult.Value = Vector2.Dot(v1, v2);
					break;

				case Vector2Operation.Distance:
					storeFloatResult.Value = Vector2.Distance(v1, v2);
					break;

				case Vector2Operation.Angle:
					storeFloatResult.Value = Vector2.Angle(v1, v2);
					break;

				case Vector2Operation.Add:
					storeVector2Result.Value = v1 + v2;
					break;

				case Vector2Operation.Subtract:
					storeVector2Result.Value = v1 - v2;
					break;

				case Vector2Operation.Multiply:
					var multResult = Vector2.zero;
					multResult.x = v1.x * v2.x;
					multResult.y = v1.y * v2.y;
					storeVector2Result.Value = multResult;
					break;

				case Vector2Operation.Divide:
					var divResult = Vector2.zero;
					divResult.x = v1.x / v2.x;
					divResult.y = v1.y / v2.y;
					storeVector2Result.Value = divResult;
					break;

				case Vector2Operation.Min:
					storeVector2Result.Value = Vector2.Min(v1, v2);
					break;

				case Vector2Operation.Max:
					storeVector2Result.Value = Vector2.Max(v1, v2);
					break;
			}
		}
	}
}
