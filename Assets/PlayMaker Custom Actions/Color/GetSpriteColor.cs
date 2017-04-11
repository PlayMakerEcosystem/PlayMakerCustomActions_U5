// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Get the color of sprite renderer")]
	[HelpUrl("")]
	public class GetSpriteColor : FsmStateAction
	{
		

		public FsmOwnerDefault gameObject;
		public FsmColor color;
		public FsmBool everyFrame;


		private SpriteRenderer sRenderer;
		private GameObject go;
		
		public override void Reset()
		{
			gameObject = null;
			color = null;
			everyFrame =false;
		
		}
		
		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			sRenderer = go.GetComponent<SpriteRenderer>();

			SpriteColo();
			
			if (!everyFrame.Value)
				Finish();	

		}
		
		public override void OnUpdate()
		{
			SpriteColo();
		}
		
		void SpriteColo()
		{

			color.Value = sRenderer.color;

		}
		
	
		
	}
}
