// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// Author: Deek

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
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Applies a logical operation on two arrays.")]
	public class ArrayOperator : FsmStateActionAdvanced
	{
		public enum ArrayOperations
		{
			AND,
			OR,
			NAND
		}

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The first array to check against the second one.")]
		public FsmArray firstArray;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The second array to check against the first one.")]
		public FsmArray secondArray;

		[Tooltip("AND: Values that are in both arrays\n\n" +
				 "OR: Combines both arrays without diplicates\n\n" +
				 "NAND: Values that are not in both arrays")]
		public ArrayOperations operation;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting array.")]
		public FsmArray result;

		public override void Reset()
		{
			//resets 'everyFrame' and 'updateType'
			base.Reset();

			firstArray = null;
			secondArray = null;
		}

		public override void OnEnter()
		{
			DoTemplate();
			if(!everyFrame) Finish();
		}

		public override void OnActionUpdate()
		{
			DoTemplate();
		}

		private void DoTemplate()
		{
			if(firstArray.Length == 0)
				LogError("The first array is empty!");

			if(secondArray.Length == 0)
				LogError("The second array is empty!");

			if(firstArray.TypeConstraint != secondArray.TypeConstraint
			|| firstArray.TypeConstraint != result.TypeConstraint)
				LogError("The specified arrays aren't of the same type!");

			List<object> resultList = new List<object>();

			for(int i = 0; i < firstArray.Length; i++)
			{
				//anything but ideal, though I couldn't come up with any generic
				//approach that would accept comparisons
				switch(firstArray.TypeConstraint)
				{
					case VariableType.Float:
						float floatVal1 = (float)firstArray.Values[i];
						float floatVal2 = (float)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(floatVal1 != floatVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(floatVal1 == floatVal2) continue;
						}
						break;
					case VariableType.Int:
						int intVal1 = (int)firstArray.Values[i];
						int intVal2 = (int)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(intVal1 != intVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(intVal1 == intVal2) continue;
						}
						break;
					case VariableType.Bool:
						bool boolVal1 = (bool)firstArray.Values[i];
						bool boolVal2 = (bool)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(boolVal1 != boolVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(boolVal1 == boolVal2) continue;
						}
						break;
					case VariableType.GameObject:
						GameObject goVal1 = (GameObject)firstArray.Values[i];
						GameObject goVal2 = (GameObject)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(goVal1 != goVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(goVal1 == goVal2) continue;
						}
						break;
					case VariableType.String:
						string stringVal1 = (string)firstArray.Values[i];
						string stringVal2 = (string)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(stringVal1 != stringVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(stringVal1 == stringVal2) continue;
						}
						break;
					case VariableType.Vector2:
						Vector2 v2Val1 = (Vector2)firstArray.Values[i];
						Vector2 v2Val2 = (Vector2)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(v2Val1 != v2Val2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(v2Val1 == v2Val2) continue;
						}
						break;
					case VariableType.Vector3:
						Vector3 v3Val1 = (Vector3)firstArray.Values[i];
						Vector3 v3Val2 = (Vector3)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(v3Val1 != v3Val2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(v3Val1 == v3Val2) continue;
						}
						break;
					case VariableType.Color:
						Color colVal1 = (Color)firstArray.Values[i];
						Color colVal2 = (Color)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(colVal1 != colVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(colVal1 == colVal2) continue;
						}
						break;
					case VariableType.Rect:
						Rect rectVal1 = (Rect)firstArray.Values[i];
						Rect rectVal2 = (Rect)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(rectVal1 != rectVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(rectVal1 == rectVal2) continue;
						}
						break;
					case VariableType.Material:
						Material matVal1 = (Material)firstArray.Values[i];
						Material matVal2 = (Material)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(matVal1 != matVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(matVal1 == matVal2) continue;
						}
						break;
					case VariableType.Texture:
						Texture texVal1 = (Texture)firstArray.Values[i];
						Texture texVal2 = (Texture)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(texVal1 != texVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(texVal1 == texVal2) continue;
						}
						break;
					case VariableType.Quaternion:
						Quaternion quatVal1 = (Quaternion)firstArray.Values[i];
						Quaternion quatVal2 = (Quaternion)secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(quatVal1 != quatVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(quatVal1 == quatVal2) continue;
						}
						break;
					default:
						object objVal1 = firstArray.Values[i];
						object objVal2 = secondArray.Values[i];

						if(operation == ArrayOperations.AND)
						{
							if(objVal1 != objVal2) continue;
						} else if(operation == ArrayOperations.NAND)
						{
							if(objVal1 == objVal2) continue;
						}
						break;
				}
				resultList.Add(firstArray.Values[i]);
			}
			result.Values = resultList.ToArray();
		}
	}
}
