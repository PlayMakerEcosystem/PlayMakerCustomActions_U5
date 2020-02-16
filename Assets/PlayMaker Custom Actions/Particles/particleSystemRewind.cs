// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10250
// Keywords: Shuriken


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Particles")]
	[Tooltip("Rewind particle. Note: For smooth rewind do not use particles option 'Play on awake' or 'Loop'")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10250")]
    public class particleSystemRewind : FsmStateAction
	{
		[RequiredField]
		[ActionSection("Setup")]
		[CheckForComponent(typeof(ParticleSystem))]
		public FsmOwnerDefault gameObject;
		[Tooltip("Set rewind time. 0 = rewind then restart. !!!Cannot change when active")]
		[TitleAttribute("Time")]
		[UIHint(UIHint.FsmFloat)]
		public FsmFloat rewindBy;
		[UIHint(UIHint.FsmBool)]
		public FsmBool inclChildren;

		private ParticleSystem ps;
		private GameObject go;

		[ActionSection("Controls")]
		public FsmBool playParticle;
		[Tooltip("Rewind enable/disable")]
		public FsmBool rewind;
		[Tooltip("Set rewind Speed. You can change Float while rewinding for effect.")]
		[TitleAttribute("Speed")]
		[UIHint(UIHint.FsmBool)]
		[HasFloatSlider(0.01f,5f)]
		public FsmFloat speed;

		[ActionSection("")]
		[TitleAttribute("Clear particles on exit")]
		public FsmBool exitClear;
		public FsmBool resetOnExit;
		public FsmBool exit;
		public FsmEvent finishEvent;

		private float duration;
		private float timeMax;
		private float timer;
		private float temp;
		private float speedTemp;
		private bool playBegin;
		private bool rBegin;

		public override void Reset()
		{
			gameObject = null;
			ps = null;
			inclChildren = false;
			playParticle = false;
			rewind = false;
			speed = 1.0f;
			exitClear = false;
			resetOnExit = false;
			exit = false;
		}

		public override void OnPreprocess()
		{
			GameObject gos = Fsm.GetOwnerDefaultTarget(gameObject);
			if (gos != null)
			{
				ps = Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<ParticleSystem>();
				ps.GetComponent<ParticleSystem>().randomSeed = 1;

				foreach (Transform trans in gos.GetComponentsInChildren<Transform>(true)){
					if (trans.gameObject.GetComponent<ParticleSystem>() != null){
						trans.gameObject.GetComponent<ParticleSystem>().randomSeed = 2;
					}
				}
			}

			if (gos == null)

			{
				Debug.LogError ("Particle rewind error - no GameObject ");
			}

			if (ps == null) 
			{
				Debug.LogError ("Particle rewind error - no particle Component");
			}

		}
		public override void OnEnter()
		{
			#if UNITY_5_6_OR_NEWER
			duration= ps.GetComponent<ParticleSystem>().main.duration;
			#else
			duration= ps.GetComponent<ParticleSystem>().duration;
			#endif

			playBegin = playParticle.Value;
			speedTemp = speed.Value;

			if (playParticle.Value == true){
			ps.GetComponent<ParticleSystem>().Play(inclChildren.Value);
			}

			if (rewindBy.Value != 0.0f)
			timeMax = duration- rewindBy.Value;

			if (duration < rewindBy.Value){
				rewindBy.Value = duration - rewindBy.Value;
			}

			OnUpdate();
		}
		
		public override void OnUpdate()
		{

			if (rewind.Value && !ps.GetComponent<ParticleSystem>().IsAlive(inclChildren.Value)) {
				Debug.LogWarning ("Past the particle lifetime - it won't work");
				if(exit.Value == true) Fsm.Event(finishEvent);
			    Finish();
			}

			if (playParticle.Value == true && ps.isPlaying == false){
				ps.GetComponent<ParticleSystem>().Play(inclChildren.Value);
			}

			if (playParticle.Value == false && ps.isPlaying == true){
				ps.GetComponent<ParticleSystem>().Pause(inclChildren.Value);
			}


			rewindGo();
			
		}


		void rewindGo(){

			if (!rewind.Value){
				timer = ps.GetComponent<ParticleSystem>().time;
				temp = timer;
			}
			
			
			if (rewind.Value){
				
				if (temp >duration){
					temp = duration;
					timer = duration;
				}
				
				
				ps.GetComponent<ParticleSystem>().Simulate(timer,inclChildren.Value);
				timer -= Time.deltaTime*Mathf.Abs(speed.Value);
				
				if (timer <= timeMax || timer <= 0.0f){
					if (rewindBy.Value == 0.0f){
						//ps.particleSystem.Clear();
						ps.GetComponent<ParticleSystem>().Simulate(0,inclChildren.Value, true);
					}
					
					continuePlay();
				}
				
			}
		}


		void continuePlay(){

			if (playParticle.Value == true && rewindBy.Value == 0 && exit.Value == false){
			rewind.Value = false;
			}

			else rewind.Value = false;

			EventsGo();

		}

		void EventsGo(){

			if (exitClear.Value == true) ps.Clear(inclChildren.Value);
			if(finishEvent != null && exit.Value == true) Fsm.Event(finishEvent);
			if (exit.Value == true) Finish();

			return;
		}

		public override void OnExit()
		{
			if (resetOnExit.Value)
			{
		
				playParticle.Value = playBegin;
				speed.Value = speedTemp;
				rewind.Value = false;

			}
		}
	}
}

