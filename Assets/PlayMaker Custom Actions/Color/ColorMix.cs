// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Made by: Thore http://hutonggames.com/playmakerforum/index.php?topic=19454.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Mixes two colors together.")]
	public class ColorMix : FsmStateAction
	{
        [Tooltip("Set the first color to blend.")]
        public FsmColor color1;
        [Tooltip("Set the second color to blend.")]
        public FsmColor color2;
        [Tooltip("Between 0-1. 0.5 equal mix. Below 0.5 bias towards first color. Above 0.5 bias towards second color.")]
        public FsmFloat mixRatio = 0.5f;
        [Tooltip("Set a color variable to store the result.")]
        public FsmColor colorResult;

        public bool everyFrame;
     
		// Code that runs on entering the state.
		public override void OnEnter()
		{
            colorResult.Value = Color.Lerp(color1.Value, color2.Value, mixRatio.Value);
            if(!everyFrame) Finish();
		}

        public override void OnUpdate()
        {
            colorResult.Value = Color.Lerp(color1.Value, color2.Value, mixRatio.Value);
        }
	}
}
