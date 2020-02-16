// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: merge combine copy add range addrange

using System.Linq;
using UnityEditor;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Concatenate arrays together")]
	public class ArrayConcatenate : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Arrays to concatenate together ")]
		public FsmArray[] arrays;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmArray result;

		private object[] _source;
		
		public override void Reset()
		{
			arrays = null;
			result = null;
		}

		public override void OnEnter()
		{
			if (!arrays.Contains(result))
			{
				_source =	result.Values;
			}
			else
			{
				_source =  new object[0];
			}

			foreach (FsmArray fsmArray in arrays)
			{
				ArrayUtility.AddRange(ref _source,fsmArray.Values);
			}
			
			result.Values = _source;
			result.SaveChanges();
			
			Finish();
		}
	}
}

