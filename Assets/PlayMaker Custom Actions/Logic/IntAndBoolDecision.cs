namespace HutongGames.PlayMaker.Actions
{
    
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Make decision by accepting multiple data types. Returns a bool.")]
    public class IntAndBoolDecision: FsmStateAction
    {
        public enum Operation
        {
            Greater,
            Lower,
            Equals,
            GreaterThenEquals,
            LowerThenEquals
        }

        [RequiredField] [UIHint(UIHint.Variable)] [Tooltip("The float variable should be tested.")]
        public FsmInt targetVariable;

        [RequiredField] [Tooltip("The tester float variable.")]
        public FsmInt testerVariable;

        [RequiredField] [Tooltip("Operation which is used between the target and tester.")]
        public Operation operation = Operation.Equals;

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
            targetVariable = null;
            testerVariable = null;
            targetBool = false;
            isTrue = null;
            isFalse = null;
            everyFrame = false;
        }

        // Perform custom error checking here.
        public override string ErrorCheck()
        {
            return null;
        }


        private FsmBool areTheyTrue()
        {
            FsmBool result = new FsmBool();
            
            switch (operation)
            {
                case Operation.Equals:
                    result = targetVariable.Value.Equals(testerVariable.Value) && targetBool.Value;
                    break;

                case Operation.Greater:
                    result = targetVariable.Value > testerVariable.Value && targetBool.Value;
                    break;

                case Operation.Lower:
                    result = targetVariable.Value < testerVariable.Value && targetBool.Value;
                    break;

                case Operation.GreaterThenEquals:
                    result = targetVariable.Value >= testerVariable.Value && targetBool.Value;
                    break;

                case Operation.LowerThenEquals:
                    result = targetVariable.Value <= testerVariable.Value && targetBool.Value;
                    break;
            }

            return result;
        }
    }
}