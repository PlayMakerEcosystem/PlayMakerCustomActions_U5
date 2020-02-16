// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10077.0
// Keywords: Shuriken

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Particles")]
	[Tooltip("Is my Particle Emitter on or off?")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10077.0")]
    public class particleSystemIsAlive : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("The particle system component owner.")]
		[CheckForComponent(typeof(ParticleSystem))]
		public FsmOwnerDefault gameObject;
		[Tooltip("Set to True when turned on and False when turned off.")]
		[ActionSection("Data")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool isAlive;
		[Tooltip("How long is the particle duration?")]
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat getDuration;
		[ActionSection("Events")]
		public FsmEvent trueEvent;
		public FsmEvent falseEvent;
		[ActionSection("")]
		[Tooltip("Will disable itself if events are used")]
		public bool everyFrame;

		private ParticleSystem ps;
		private GameObject go;

		public override void Reset()
		{
			gameObject = null;
			isAlive = false;
			getDuration = 0f;
			ps = null;
			everyFrame = false;
			trueEvent = null;
			falseEvent =  null;
		}
		
		public override void OnEnter()
		{
		
			if(trueEvent != null & falseEvent != null) {
				everyFrame = false;
			}

			ps = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<ParticleSystem>();
			#if UNITY_5_6_OR_NEWER
			getDuration.Value = ps.main.duration;
			#else
			getDuration.Value = ps.duration;
			#endif

			
			if (!ps.IsAlive() & getDuration.Value <= 0)
			{
				Debug.LogWarning("error");
				Finish();
				return;
			}

			IsPSAlive();

			if (!everyFrame)
				Finish();

		}
		
		public override void OnUpdate()
		{
			IsPSAlive();
		}

		public void IsPSAlive()
		{

			if (ps.IsAlive())
			{
					isAlive.Value = true;
				if(trueEvent != null) {Fsm.Event(trueEvent);}
			}

			if (!ps.IsAlive())
			{
					isAlive.Value = false;
				if(falseEvent != null) {Fsm.Event(falseEvent);}
			}

		}

		
	}
}
