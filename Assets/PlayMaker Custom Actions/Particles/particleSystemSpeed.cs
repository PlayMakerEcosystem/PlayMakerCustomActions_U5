// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10088
// Keywords: Shuriken


using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Particles")]
	[Tooltip("Control particle(s) speed")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10088")]
    public class particleSystemSpeed : FsmStateAction
	{
		[CompoundArray("Count", "GameObject", "speed")]
		[RequiredField]
		[ActionSection("Setup")]
		[CheckForComponent(typeof(ParticleSystem))]
		public FsmOwnerDefault[] gameObject;
		[Tooltip("Set Speed. Cannot be below 0")]
		[TitleAttribute("Speed")]
		[UIHint(UIHint.FsmBool)]
		public FsmFloat[] speed;

		private ParticleSystem ps;
		private GameObject go;

		[ActionSection("")]
		[Tooltip("Will disable itself if events are used")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = new FsmOwnerDefault[1];
			speed  = new FsmFloat[1];
			ps = null;
		}
		
		public override void OnEnter()
		{

			for(int i = 0; i<gameObject.Length;i++){
			
				ps = Fsm.GetOwnerDefaultTarget(gameObject[i]).GetComponent<ParticleSystem>();

				if (speed[i].Value <0.0f) Debug.LogWarning ("Negative particle speed is not supported. Corrected for now by actions - please correct. Must be 0.0f or above");
				ps.particleSystem.playbackSpeed = Mathf.Abs(speed[i].Value);

				}
	


			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			
			for(int i = 0; i<gameObject.Length;i++){
				
				ps = Fsm.GetOwnerDefaultTarget(gameObject[i]).GetComponent<ParticleSystem>();

				ps.particleSystem.playbackSpeed = Mathf.Abs(speed[i].Value);
				
			}
			
			
			
			if (!everyFrame)
				Finish();
		}
	}
		
}
