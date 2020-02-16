// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10085.0
// Keywords: Shuriken

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Particles")]
	[Tooltip("Are all my Particle System on/playing?")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10085.0")]
    public class particleSystemIsAliveBridge : FsmStateAction
	{

		[ActionSection("Bools")]
		[TitleAttribute("Is Active or Playing")]
		public FsmBool isPlaying;
		[TitleAttribute("Is Inactive")]
		public FsmBool isStopped;
		[ActionSection("Events")]
		public FsmEvent isPlayingEvent;
		[TitleAttribute("is Inactive Event")]
		public FsmEvent isStoppedEvent;
		[ActionSection("")]
		[Tooltip("Will disable itself if both events are used")]
		public bool everyFrame;
		[Tooltip("Set everyframe to every 15 frames - needed for when many particles are in scene")]
		public bool optimization;
		[Tooltip("Perform the method in LateUpdate")]
		public bool lateUpdate;

		private bool boolTemp;
		private static int lastRecalculation = 15;
		private int frameCount;

		public override void Reset()
		{
			isPlaying = false;
			isStopped = false;

			isPlayingEvent = null;
			isStoppedEvent =  null;

			everyFrame = false;
			lateUpdate = true;
		}


		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER
			if (lateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}
		public override void OnEnter()
		{

			if(isPlayingEvent != null & isStoppedEvent != null) 
			{
				everyFrame = false;
			}

			IsPSAliveBridge();

			if (!everyFrame)
				Finish();

		}

		public override void OnUpdate(){


			if (!lateUpdate)
			{

				frameCount++;

			if (optimization & frameCount >= lastRecalculation){
			IsPSAliveBridge();

			}

			if (!optimization) IsPSAliveBridge();

			}
		}

		public override void OnLateUpdate()
		{
		
			if (lateUpdate)
			{
				frameCount++;
		
				if (optimization & frameCount >= lastRecalculation){
					IsPSAliveBridge();
					
				}
				
				if (!optimization) IsPSAliveBridge();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public void IsPSAliveBridge()
		{

			frameCount = 0;

			ParticleSystem[] ps = ParticleSystem.FindObjectsOfType(typeof(ParticleSystem)) as ParticleSystem[];
			
			foreach(ParticleSystem system in ps)
			{
				boolTemp = false;
				
				if(system.isPlaying)
				{
					boolTemp = true;
					
				}
				if(boolTemp){
					
					break;
				}
			}
			
			if(boolTemp) {
				isPlaying.Value = true;
				isStopped.Value = false;
				if(isPlayingEvent != null) 
				{
					Fsm.Event(isPlayingEvent);
				}
			}
			
			else {
				isPlaying.Value = false;
				isStopped.Value = true;
				
				if(isStoppedEvent != null) 
				{
					Fsm.Event(isStoppedEvent);
				}

			}
			return;
		}
	}
}
