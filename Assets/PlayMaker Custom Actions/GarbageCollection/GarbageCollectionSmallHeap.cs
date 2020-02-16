// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: GC

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Garbage Collection")]
	[Tooltip("This strategy is often best for games that have long periods of gameplay where a smooth framerate is the main concern")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12323.0")]
	public class GarbageCollectionSmallHeap : FsmStateAction
	{
		[ActionSection("Small Heap Setup")]
		[Tooltip("Allow Garbage Collection")]
		public FsmBool collectionOn;
		public FsmInt frameCount;

		[ActionSection("Options")]
		[Tooltip("Stop the collection")]
		public FsmBool forceCancelled;
		public FsmEvent cancelledEvent;

		public override void Reset()
		{
			collectionOn = true;
			forceCancelled = false;
			cancelledEvent = null;
			frameCount = 30;
		}
		
		public override void OnEnter()
		{
	
		}


		public override void OnUpdate(){
			
			if (forceCancelled.Value == true)
			{

				Fsm.Event(cancelledEvent);
				Finish();
				
			}

			if (collectionOn.Value == true){

			if (Time.frameCount % frameCount.Value == 0)
			{
				System.GC.Collect();
			}
			
			}

		}

	
		
	}
}
