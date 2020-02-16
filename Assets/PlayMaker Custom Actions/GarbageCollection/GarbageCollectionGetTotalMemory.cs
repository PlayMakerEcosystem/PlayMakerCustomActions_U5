// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: GC

using UnityEngine;
using System.Collections;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Garbage Collection")]
	[Tooltip("Get Mono Total Memory")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12323.0")]
	public class GarbageCollectionGetTotalMemory : FsmStateAction
	{
		
		[Tooltip("Allow Garbage Collection")]
		public FsmBool getGcMem;
		public FsmFloat resultMb;
		public FsmBool everyframe;

		public override void Reset()
		{
			getGcMem = true;
			resultMb = 0;
			everyframe = false;
		}
		
		public override void OnEnter()
		{
	
			if (getGcMem.Value == true){
				long t = GC.GetTotalMemory(false);
				resultMb.Value = Convert.ToSingle(ConvertBytesToMegabytes(t));
			}

			if (everyframe.Value == false)
			Finish();

		}
		
		public override void OnUpdate()
		{

			if (getGcMem.Value == true){
				long x = GC.GetTotalMemory(false);
				resultMb.Value = Convert.ToSingle(ConvertBytesToMegabytes(x));
			}

			if (everyframe.Value == false)
			Finish();

		}

		static double ConvertBytesToMegabytes(long bytes)
		{
			return (bytes / 1024f) / 1024f;
		}
			
		
	}
}

