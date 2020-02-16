// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: multi, multiple
// Source http://hutonggames.com/playmakerforum/index.php?topic=9989.0

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Texture")]
	[Tooltip("Sets the value of many Texture Variable.")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9989.0")]
	public class SetMultiTextureValue : FsmStateAction
	{
		[CompoundArray("Count", "Texture Variable", "Value")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmTexture[] textureVariable;
		public FsmTexture[] values;
		public bool everyFrame;
		
		public override void Reset()
		{
			textureVariable = new FsmTexture[1];
			values = new FsmTexture[1];
			everyFrame = false;
		}
		

		public override void OnEnter()
		{
			DoSetTextureValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetTextureValue();
		}

		public void DoSetTextureValue()
		{
			for(int i = 0; i<textureVariable.Length;i++){
				if(!textureVariable[i].IsNone) 
					textureVariable[i].Value = values[i].Value;
			}
		}
	}
}
