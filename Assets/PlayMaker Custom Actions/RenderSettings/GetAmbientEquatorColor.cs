// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.Rendering;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Gets the RenderSettings Ambient Equator Color.")]
	public class GetAmbientEquatorColor : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Ambient Equator Color")]
		public FsmColor ambientEquatorColor;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			ambientEquatorColor = null;
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
			ambientEquatorColor.Value = RenderSettings.ambientEquatorColor;
		}
	}
}