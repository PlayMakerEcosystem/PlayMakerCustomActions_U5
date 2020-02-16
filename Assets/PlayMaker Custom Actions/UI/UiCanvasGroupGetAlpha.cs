
// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Get CanvasGroup Alpha.")]
    public class UiCanvasGroupGetAlpha : ComponentAction<CanvasGroup>
    {
        [RequiredField]
        [CheckForComponent(typeof(CanvasGroup))]
        [Tooltip("The GameObject with a UI CanvasGroup component.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The alpha of the UI CanvasGroup.")]
        public FsmFloat alpha;

        [Tooltip("Repeats every frame, useful for animation")]
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            alpha = null;

            everyFrame = false;
        }

        public override void OnEnter()
        {
           
            DoGetValue();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetValue();
        }

        private void DoGetValue()
        {
            if (UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
                alpha.Value = this.cachedComponent.alpha;
            }
        }



    }
}