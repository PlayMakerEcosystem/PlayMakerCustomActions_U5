// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// will be included in official on playmaker 1.9.1

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Sets the color of sprite renderer")]
	[HelpUrl("")]
	public class SetSpriteColor : FsmStateAction
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

			sRenderer.color = color.Value;

		}
		
	
		
	}
}
