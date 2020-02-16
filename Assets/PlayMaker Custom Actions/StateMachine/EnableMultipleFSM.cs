// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// author : djaydino
// Keywords: multi, multiple
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Enable/Disable multiple FSM Components on GameObjects")]


    public class EnableMultipleFSM : FsmStateAction
    {
        [CompoundArray("Count", "FSM", "Enable")]
        [UIHint(UIHint.Variable)]
        [RequiredField]
        public PlayMakerFSM[] fsmComponent;

        [Tooltip("Set to True to enable, False to disable.")]
        public FsmBool[] enable;

        [Tooltip("Reset the initial enabled state when exiting the state.")]
        public FsmBool resetOnExit;

        public override void Reset()
        {
            fsmComponent = null;
            enable = null;
            resetOnExit = false;
        }

        public override void OnEnter()
        {
            DoMultiEnableFSM();

            Finish();
        }

        void DoMultiEnableFSM()
        {
            for (int i = 0; i < fsmComponent.Length; i++)
            {

                fsmComponent[i].enabled = enable[i].Value;
            }
        }

        public override void OnExit()
        {
            if (resetOnExit.Value)
            {
                for (int i = 0; i < fsmComponent.Length; i++)
                {
                    fsmComponent[i].enabled = !enable[i].Value;
                }
            }
        }
    }
}