// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the RenderSettings Ambient Equator Color.")]
	public class SetAmbientEquatorColor : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Ambient Equator Color")]
		public FsmColor ambientEquatorColor;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;


		public override void Reset()
		{
			ambientEquatorColor = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetAmbientEquatorColor();

			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			DoSetAmbientEquatorColor();
		}

		
		void DoSetAmbientEquatorColor()
		{
			RenderSettings.ambientEquatorColor = ambientEquatorColor.Value;
		}
	}
}