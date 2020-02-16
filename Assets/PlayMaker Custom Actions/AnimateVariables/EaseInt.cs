// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Author: Deek

using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.AnimateVariables)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Easing Animation - Int")]
	public class EaseInt : EaseFsmAction
	{
		public enum RoundTo
		{
			Nearest,
			Ceiling,
			Floor
		}

		[RequiredField]
		public FsmInt fromValue;
		[RequiredField]
		public FsmInt toValue;
		[UIHint(UIHint.Variable)]
		public FsmInt intVariable;
		public RoundTo roundTo;
		
		private bool finishInNextStep = false;
		
		public override void Reset (){
			base.Reset();
			fromValue = null;
			toValue = null;
			intVariable = null;
			roundTo = RoundTo.Nearest;
			finishInNextStep = false;
		}
		                   
		
		public override void OnEnter ()
		{
			base.OnEnter();
			fromFloats = new float[1];
			fromFloats[0] = (float)fromValue.Value;
			toFloats = new float[1];
			toFloats[0] = (float)toValue.Value;
			resultFloats = new float[1];
			finishInNextStep = false;
            intVariable.Value = fromValue.Value;
		}
		
		public override void OnExit (){
			base.OnExit();
		}
			
		public override void OnUpdate(){
			base.OnUpdate();
			if(!intVariable.IsNone && isRunning){
				double d = (double)resultFloats[0];

				switch(roundTo)
				{
					case RoundTo.Nearest:
						intVariable.Value = (int)Math.Round(d); break;
					case RoundTo.Ceiling:
						intVariable.Value = (int)Math.Ceiling(d); break;
					case RoundTo.Floor:
						intVariable.Value = (int)Math.Floor(d); break;
				}
			}
			
			if(finishInNextStep){
				Finish();
				if(finishEvent != null)	Fsm.Event(finishEvent);
			}
			
			if(finishAction && !finishInNextStep){
				if(!intVariable.IsNone){
					intVariable.Value = reverse.IsNone ? toValue.Value : reverse.Value ? fromValue.Value : toValue.Value; 
				}
				finishInNextStep = true;
			}
		}
	}
}