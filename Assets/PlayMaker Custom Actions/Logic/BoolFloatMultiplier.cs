// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: Multiplier Bool
// Made by Thore. Use for whatever nefarious purpose you like.

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Multiply a float based on a bool variable.")]
	public class BoolFloatMultiplier : FsmStateAction
	{

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Bool variable that determines the multiplication.")]
        public FsmBool boolVariable;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The float variable to work with")]
        public FsmFloat floatVariable;

        [Tooltip("Factor used when the bool is true.")]
        public FsmFloat multiplyIfTrue;

        [Tooltip("Factor used when the bool is false.")]
        public FsmFloat multiplyIfFalse;

        [Tooltip("Reset the float to what it was upon entering the state.")]
        public bool resetFloat;
	    
	    [ActionSection("result")]
	    [RequiredField]
	    [UIHint(UIHint.Variable)]
	    [Tooltip("The result")]
	    public FsmFloat result;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        private float storedFloat;

        public override void Reset()
        {
            boolVariable = null;
            floatVariable = null;
            multiplyIfTrue = null;
            multiplyIfFalse = null;
            result = null;
            everyFrame = false;
            resetFloat = false;
        }


        public override void OnEnter()
		{
		    storedFloat = floatVariable.Value;
		    
            DoTheModification();

            if (!everyFrame)
            {
                Finish();
            }

        }

        public void DoTheModification()
        {
            if (boolVariable.Value) result.Value = floatVariable.Value * multiplyIfTrue.Value;

            if (!boolVariable.Value) result.Value = floatVariable.Value * multiplyIfFalse.Value;

        }


        public override void OnUpdate()
        {
            DoTheModification();
        }

        public override void OnExit()
        {
            if (resetFloat) result.Value = storedFloat;

        }


    }
}
