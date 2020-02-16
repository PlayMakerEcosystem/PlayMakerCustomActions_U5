// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// http://hutonggames.com/playmakerforum/index.php?topic=19190.msg83571#msg83571
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets a Float Variable to a random value between Min/Max.")]
	public class RandomFloatSeed : FsmStateAction
	{
        
		[RequiredField]
		public FsmInt seed;
		[RequiredField]
		public FsmFloat min;
		[RequiredField]
		public FsmFloat max;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;

		public override void Reset()
		{
			min = 0f;
			max = 1f;
            
			storeResult = null;
		}

		public override void OnEnter()
		{
            
			Random.InitState(seed.Value);
			storeResult.Value = Random.Range(min.Value, max.Value);
			
			Finish();
		}
	}
}