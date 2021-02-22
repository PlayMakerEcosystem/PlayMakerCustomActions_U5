using static HutongGames.PlayMaker.Actions.FloatAndBoolDecision;

namespace HutongGames.PlayMaker.Actions.Logic
{
    
    /// <summary>
    /// Author is Istvan Nemeth. <see cref="https://github.com/PlayMakerEcosystem/PlayMakerCustomActions_U5"/>
    /// </summary>
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Make decision by accepting multiple data types. Returns a bool.")]
    public class IntAndFloatAndBoolDecision : FsmStateAction
    {
        [RequiredField] [UIHint(UIHint.Variable)] [Tooltip("The int variable should be tested.")]
        public FsmInt targetInt;

        [RequiredField] [UIHint(UIHint.Variable)] [Tooltip("The tester int variable.")]
        public FsmInt testerInt;
        
        [RequiredField]
        [Tooltip("Operation which is used between the target and tester.")]
        public Operation intOperation = Operation.Equals;
        
        [RequiredField] [UIHint(UIHint.Variable)] [Tooltip("The float variable should be tested.")]
        public FsmFloat targetFloat;

        [RequiredField] [UIHint(UIHint.Variable)] [Tooltip("The tester float variable.")]
        public FsmFloat testerFloat;
        
        [RequiredField]
        [Tooltip("Operation which is used between the target and tester.")]
        public Operation floatOperation = Operation.Equals;
        
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Boolean to be tested, which is used with short circuit and statement.")]
        public FsmBool targetBool;

        [Tooltip("Event to send if the Bool variable is True.")]
        public FsmEvent isTrue;

        [Tooltip("Event to send if the Bool variable is False.")]
        public FsmEvent isFalse;

        public bool everyFrame;
        
        // Code that runs on entering the state.
        public override void OnEnter()
        {
            Fsm.Event(areTheyTrue().Value ? isTrue : isFalse);

            if (!everyFrame)
                Finish();
        }
        
        // Code that runs every frame.
        public override void OnUpdate()
        {
            Fsm.Event(areTheyTrue().Value ? isTrue : isFalse);
        }

        public override void Reset()
        {
            targetFloat = null;
            testerFloat = null;
            targetBool = false;
            isTrue = null;
            isFalse = null;
            everyFrame = false;
        }
        
        private FsmBool areTheyTrue()
        {
            FsmBool result = new FsmBool();

            if (intOperation == Operation.Equals)
            {
                result = targetInt.Value.Equals(testerInt.Value);
                if (floatOperation == Operation.Equals)
                {
                    result = result.Value && targetFloat.Value.Equals(testerFloat.Value) && targetBool.Value;
                }
            }


            switch (floatOperation)
            {
                case Operation.Equals:
                    result = targetFloat.Value.Equals(testerFloat.Value) && targetBool.Value;
                    break;

                case Operation.Greater:
                    result = targetFloat.Value > testerFloat.Value && targetBool.Value;
                    break;

                case Operation.Lower:
                    result = targetFloat.Value < testerFloat.Value && targetBool.Value;
                    break;

                case Operation.GreaterThenEquals:
                    result = targetFloat.Value >= testerFloat.Value && targetBool.Value;
                    break;

                case Operation.LowerThenEquals:
                    result = targetFloat.Value <= testerFloat.Value && targetBool.Value;
                    break;
            }

            return result;
        }
    }
}