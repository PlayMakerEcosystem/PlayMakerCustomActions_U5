// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10077.0
// Keywords: Shuriken

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Particles")]
	[Tooltip("Are my Particle Emitters on or off?")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10077.0")]
    public class particleSystemMultiIsAlive : FsmStateAction
	{
		[CompoundArray("Count", "GameObject", "isAlive")]
		[RequiredField]
		[ActionSection("Setup")]
		[CheckForComponent(typeof(ParticleSystem))]
		public FsmOwnerDefault[] gameObject;
		[Tooltip("Set to True when turned on and False when turned off.")]
		[ActionSection("Data")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool[] isAlive;
		[ActionSection("Events")]
		public FsmEvent allTrueEvent;
		public FsmEvent allFalseEvent;
		[ActionSection("")]
		[Tooltip("Will disable itself if events are used")]
		public bool everyFrame;

		private ParticleSystem ps;
		private GameObject go;
		private bool falsePs;
		
		public override void Reset()
		{
			gameObject = new FsmOwnerDefault[1];
			isAlive = new FsmBool[1];
			ps = null;
			everyFrame = true;
			falsePs = false;
			allTrueEvent = null;
			allFalseEvent = null;
		}
		
		public override void OnEnter()
		{
			falsePs = false;

			if(allTrueEvent != null & allFalseEvent != null) {
				everyFrame = false;
			}

			IsPSALive();

			if (!everyFrame)
				Finish();

		}
		
		public override void OnUpdate()
		{
			IsPSALive();
		}

		public void IsPSALive()
		{
			for(int i = 0; i<gameObject.Length;i++){

				ps = Fsm.GetOwnerDefaultTarget(gameObject[i]).GetComponent<ParticleSystem>();

				if (ps.IsAlive())
			{
					isAlive[i].Value = true;
					
			}

				if (!ps.IsAlive()){
					isAlive[i].Value = false;
					falsePs = true;
			}
			}

			if(falsePs != true & allTrueEvent != null) {Fsm.Event(allTrueEvent);}
			if(falsePs != false & allFalseEvent != null) {Fsm.Event(allFalseEvent);}
		}
		
	}
}
