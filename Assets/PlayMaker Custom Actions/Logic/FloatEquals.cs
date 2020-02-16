// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=9910



using UnityEngine;
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the comparison of multiple Floats.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=9910")]
    public class FloatEquals : FsmStateAction
	{

		[ActionSection("Main float")]
		[Tooltip("Main float to compare to")]
		public FsmFloat keyFloat;

		[ActionSection("Floats to compare with")]
		[RequiredField]
		public FsmFloat[] floats;

		[ActionSection("Simple Tolerance")]
		[UIHint(UIHint.Description)]
		public string descriptionArea = "on";
		[RequiredField]
		[Tooltip("Tolerance for the Equal test (almost equal). 0 =off (will use the advance tolerance instead) / 1+ =on ")]
		public FsmFloat tolerance;

		[ActionSection("Advance Tolerance")]
		[UIHint(UIHint.Description)]
		[Tooltip("Tolerance above(+) the main number.")]
		public FsmFloat tolerancePositive;
		[Tooltip("Tolerance below(-) the main number.")]
		public FsmFloat toleranceNegative;

		[ActionSection("Set bools")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Set bool to true if all floats equal")]
		public FsmBool isEqual;
		[UIHint(UIHint.Variable)]
		[Tooltip("Set bool to true if floats are not equal")]
		public FsmBool isNotEqual;

		[ActionSection("Events")]
		[Tooltip("Event sent if all floats equal")]
		public FsmEvent equal;
		[Tooltip("Event sent if floats are not equal")]
		public FsmEvent notEqual;
		[ActionSection("")]
		public bool everyFrame;
		
		public override void Reset()
		{
			floats = new FsmFloat[1];
			keyFloat = 0f;
			tolerance = 0f;
			tolerancePositive= 0f;
			toleranceNegative= 0f;
			isEqual = false;
			isNotEqual = false;
			equal = null;
			notEqual = null;
			
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoFloatCompare();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoFloatCompare();
		}		

		void DoFloatCompare()
		{
			if (floats.Length>=1)
			
			{
				float _base = keyFloat.Value;
			
				if (tolerance.Value > 0){

					descriptionArea = "on";


					foreach(FsmFloat _float in floats){
					

					if ((Mathf.Abs(_base - _float.Value) <= tolerance.Value)==false)  
					{
							isEqual.Value = false;
							isNotEqual.Value = true;
						Fsm.Event(notEqual);	
						return;
					}
			
				}
			}
			
				else if (tolerance.Value == 0){
			
					descriptionArea = "off";
				

			{
				
				foreach(FsmFloat _float in floats)
				{

					if (((Mathf.Abs(_base - _float.Value)) == 0)==false) {

						if (((_base - _float.Value) > 0)==false)  {
						
						
						if ((Mathf.Abs(_base - _float.Value) <= tolerancePositive.Value)==false)
						{
										isEqual.Value = false;
										isNotEqual.Value = true;
								Fsm.Event(notEqual);	
								return;
						}
						}


						else if (((_base - _float.Value) > 0)==true)  {

							if (((_base - _float.Value) <= (toleranceNegative.Value))==false)
							{
										isEqual.Value = false;
										isNotEqual.Value = true;
								Fsm.Event(notEqual);	
								return;
							}


						}
					}

				}
			}
				}
				isEqual.Value = true;
				isNotEqual.Value = false;
			Fsm.Event(equal);
	
		}
		}

		public override string ErrorCheck()
		{
			if (floats.Length <1)
			{
				return "Action needs more than 1 float to compare";
			}
			return "";
		}
	}
}
