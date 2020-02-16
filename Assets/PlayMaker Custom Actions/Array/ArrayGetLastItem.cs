// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Get a last item of an array")]
    public class ArrayGetLastItem : FsmStateAction
    {
        [RequiredField] 
        [UIHint(UIHint.Variable)] 
        [Tooltip("The Array Variable")] 
        public FsmArray array;



        [RequiredField]
        [MatchElementType("array")]
        [UIHint(UIHint.Variable)] 
        [Tooltip("Store the last value in a variable.")] 
        public FsmVar storeValue;

        [Tooltip("The last index in the array.")] 
        public FsmInt storeIndex;
        

        public override void Reset()
        {
            array = null;
            storeIndex = null;
            storeValue = null;
        }

        public override void OnEnter()
        {
            ExecuteAction();
            
            Finish();
        }

        public override void OnUpdate()
        {
            ExecuteAction();
        }

        private void ExecuteAction()
        {
            if (array.IsNone)
            {
                return;
            }

            if (!storeValue.IsNone) 
                storeValue.SetValue(array.Get(array.Length-1));
            
            if (!storeIndex.IsNone)
                storeIndex.Value = array.Length - 1;

        }


#if UNITY_EDITOR
        public override string AutoName()
        {
            return ActionHelpers.GetValueLabel(storeValue.NamedVar) + "=" + array.Name + "[" + ActionHelpers.GetValueLabel(storeIndex) + "]";
        }
#endif
    }
}
