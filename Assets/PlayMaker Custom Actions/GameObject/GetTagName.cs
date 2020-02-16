// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Original action by h.goren@enderunstudios.com

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.GameObject)]
    [Tooltip("Gets the name of a given tag")]
    public class GetTagName : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Tag)]
        public FsmString tag;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmString storeResult;

        public override void Reset()
        {
            tag = "Untagged";
            storeResult = null;
        }

        public override void OnEnter()
        {
            if (string.IsNullOrEmpty(tag.Value))
            {
                storeResult.Value = string.Empty;
            }
            else
            {
                storeResult.Value = tag.Value;
            }

            Finish();
        }
    }
}