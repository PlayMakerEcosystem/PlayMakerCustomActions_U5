// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Made by djaydino -- http://www.jinxtergames.com/ --

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Creates colour from HSV input.")]
	public class SetHSVToColor : FsmStateAction
	{

        [RequiredField]
        [HasFloatSlider(0,1)]
		public FsmFloat hue;

        [RequiredField]
        [HasFloatSlider(0,1)]
		public FsmFloat saturation;

        [RequiredField]
        [HasFloatSlider(0,1)]
		public FsmFloat brightness;

        [Tooltip("The alpha value")]
        [HasFloatSlider(0, 1)]
        public FsmFloat alpha;

        [RequiredField]
        [Tooltip("Output HDR colours. If true, the returned colour will not be clamped to [0..1].")]
        public FsmBool hdr;

        [ActionSection("Result")]

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmColor colorVariable;


		
		public bool everyFrame;

		public override void Reset()
		{
			colorVariable = null;
            hue = 0;
			saturation = 0;
			brightness = 0;
            hdr = false;
            alpha = new FsmFloat() { UseVariable = true };

            everyFrame = false;
		}

		public override void OnEnter()
		{
            DoSetHSVToRGBA();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
            DoSetHSVToRGBA();
		}

		void DoSetHSVToRGBA()
		{
            var newColor = Color.HSVToRGB(hue.Value, saturation.Value, brightness.Value, hdr.Value);

            if (!alpha.IsNone)
            {
                newColor.a = alpha.Value;
            }


                colorVariable.Value = newColor;

        }
	}
}