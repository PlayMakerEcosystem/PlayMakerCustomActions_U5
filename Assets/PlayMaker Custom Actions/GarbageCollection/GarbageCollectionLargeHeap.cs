// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: GC

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Garbage Collection")]
	[Tooltip("This strategy works best for games where allocations (and therefore collections) are relatively infrequent and can be handled during pauses in gameplay.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12323.0")]
	public class GarbageCollectionLargeHeap : FsmStateAction
	{
		[ActionSection("Large Heap Setup")]
		[Tooltip("!!! Power of two - default is 1024")]
		public FsmInt byteSize;


		public override void Reset()
		{
	
			byteSize = 1024;
		}
		
		public override void OnEnter()
		{
			bool b = IsPowerOfTwo(byteSize.Value);

			if (b == true){
			var tmp = new System.Object[byteSize.Value];
			

			for (int i = 0; i < byteSize.Value; i++)
				tmp[i] = new byte[byteSize.Value];
			

			tmp = null;
			}

			else {

				Debug.LogWarning("<b>[GarbageCollectionLargeHeap]</b><color=#FF9900ff>  'byteSize' number is not power of 2 - nothing happened  - Please review!</color>", this.Owner);
			}

			Finish();
		}

		bool IsPowerOfTwo(int x)
		{
			return (x & (x - 1)) == 0;
		}



	
		
	}
}
