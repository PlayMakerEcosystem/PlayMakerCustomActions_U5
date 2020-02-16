// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek

/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Enables a Component if it's disabled and vice versa.")]
	public class ToggleComponent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The component to toggle. Supports scripts, colliders and renderer.")]
		public FsmObject component;

		[Tooltip("Wheter to run this action every frame or only once.")]
		public FsmBool everyFrame;

		public override void Reset()
		{
			component = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoToggleComponent();

			if(!everyFrame.Value) Finish();
		}

		public override void OnUpdate()
		{
			DoToggleComponent();
		}

		private void DoToggleComponent()
		{
			Behaviour be = component.Value as Behaviour;
			Renderer re = component.Value as Renderer;
			Collider col = component.Value as Collider;

			if(be)
				be.enabled = !be.enabled;
			else if(re)
				re.enabled = !re.enabled;
			else if(col)
				col.enabled = !col.enabled;
		}
	}
}
