// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Resources")]
	[Tooltip("UnLoads unused Resources. Be careful to use it only once at a time as it crates hickups especially on mobile.")]
	public class ResourcesUnLoadUnusedAssets : FsmStateAction
	{

		public FsmEvent UnloadDoneEvent;
		
		AsyncOperation _op;
		
		public override void Reset()
		{
			UnloadDoneEvent = null;
		}
		
		public override void OnEnter()
		{
		  _op	= Resources.UnloadUnusedAssets();
		}
		
		public override void OnUpdate()
		{
		  if (_op!=null)
			{
				if (_op.isDone)
				{
					Fsm.Event(UnloadDoneEvent);
					Finish();
				}
			}
		}
		
	}
}

