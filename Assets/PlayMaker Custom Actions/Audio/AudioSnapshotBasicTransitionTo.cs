// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __ACTION__ ---*/
// Keywords: Sound
//  v1.0 for UNITY 5+

using UnityEngine;
using HutongGames.PlayMaker;
using UnityEngine.Audio;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Performs an interpolated transition towards this snapshot over the time interval specified.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11603.0")]
	public class AudioSnapshotBasicTransitionTo : FsmStateAction
	{

		[ActionSection("Snapshot Setup")]
		public FsmObject ToSnapshot;
		
		[ActionSection("Setup")]
		public FsmFloat timeToReach;

		private AudioMixerSnapshot snapShotTarget;
		
		public override void Reset()
		{

			ToSnapshot = null;
			timeToReach = 0.0f;
		}
		
		public override void OnEnter()
		{
			snapShotTarget = ToSnapshot.Value as AudioMixerSnapshot;

			snapShotTarget.TransitionTo(timeToReach.Value);

			Finish();
		}

	}
	
}
