// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests the given Bool Variables and counts the ones that are true and false")]
	public class CountBoolStates : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The Bool variables to check.")]
		public FsmBool[] boolVariables;
	
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the true result in an Int variable.")]
		public FsmInt trueCount;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the false result in an Int variable.")]
        public FsmInt falseCount;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

		public override void Reset()
		{
			boolVariables = null;
            trueCount = null;
            falseCount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			CheckBools();
			
			if (!everyFrame)
			{
			    Finish();
			}		
		}
		
		public override void OnUpdate()
		{
			CheckBools();
		}
		
		void CheckBools()
		{
			if (boolVariables.Length == 0) return;
			
			var numberOfTrueBools = 0;
			var numberOfFalseBools = 0;
			
			for (var i = 0; i < boolVariables.Length; i++) 
			{
				if (boolVariables[i].Value)
				{
					++numberOfTrueBools;
				}
                else
                {
                    ++numberOfFalseBools;
                }
			}

           if(!trueCount.IsNone) trueCount.Value = numberOfTrueBools;
           if(!falseCount.IsNone) falseCount.Value = numberOfFalseBools;
		}
	}
}