// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: GC

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Garbage Collection")]
	[Tooltip("Request a collection explicitly")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12323.0")]
	public class GarbageCollection : FsmStateAction
	{
		
		[Tooltip("Allow Garbage Collection")]
		public FsmBool collectionOn;


		public override void Reset()
		{
			collectionOn = true;
		
		}
		
		public override void OnEnter()
		{
	
			if (collectionOn.Value == true){
			System.GC.Collect();
			}

			Finish();	

		}
		
	
		
	
		
	}
}
