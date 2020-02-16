// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets the Scale of a Game Object by percentage (%)")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12404.0")]
	public class SetScaleByPercentage : FsmStateAction
	{
		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("The GameObject to scale.")]
		public FsmOwnerDefault gameObject;

		[HasFloatSlider(0f, 200f)]
		[Tooltip("Increase or reduce by %")]
		[TitleAttribute("By %")]
		public FsmFloat global;
		
		public FsmFloat x;
		public FsmFloat y;
		public FsmFloat z;


		[ActionSection("Option")]
		[Tooltip("Repeat every frame.")]
		public FsmBool everyFrame;

		[Tooltip("Perform in LateUpdate. This is useful if you want to override the position of objects that are animated or otherwise positioned in Update.")]
		public bool lateUpdate;	

		[ActionSection("Output")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Use stored Vector3 value, and/or set each axis below.")]
		public FsmVector3 vector;

		private Vector3 originalScale;
		private GameObject go;

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			global = 100f;
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			z = new FsmFloat { UseVariable = true };
			everyFrame = false;
			lateUpdate = false;
		}

		public override void OnPreprocess()
		{
			#if PLAYMAKER_1_8_5_OR_NEWER

			if(lateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (go == null)
			{
				Debug.LogWarning("<b>[SetScaleByPercentage]</b><color=#FF9900ff> Empty GameObject - Please review!</color>", this.Owner);
				Finish();
			}

			originalScale = go.transform.localScale;

			DoSetScale();
			
			if (!everyFrame.Value)
			{
				Finish();
			}		
		}

		public override void OnUpdate()
		{
			if (!lateUpdate)
			{
				DoSetScale();
			}
		}

		public override void OnLateUpdate()
		{
			if (lateUpdate)
			{
				DoSetScale();
			}

			if (!everyFrame.Value)
			{
				Finish();
			}
		}

		void DoSetScale()
		{
			var scale = originalScale;

			if (global.Value < 0f )
			{
				Debug.LogWarning("<b>[SetScaleByPercentage]</b><color=#FF9900ff> Does not go below 0% - Please review!</color>", this.Owner);
				global.Value = 0f;
			}


			if (!x.IsNone) {
			
				scale.x = (originalScale.x*x.Value)/100;

			}

			else {

				scale.x = (originalScale.x*global.Value)/100;

			}

			if (!y.IsNone) {

			
				scale.y = (originalScale.y*y.Value)/100;

			}

			else {
			
				scale.y = (originalScale.y*global.Value)/100;

			}

			if (!z.IsNone) {

				scale.z = (originalScale.z*z.Value)/100;

			}

			else {

				scale.z = (originalScale.z*global.Value)/100;

			}



			go.transform.localScale = scale;
			vector.Value = scale;
		}


	}
}
