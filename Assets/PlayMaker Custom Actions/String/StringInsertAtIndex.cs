// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Made by Djaydino http://www.jinxtergames.com/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.String)]
    [Tooltip("add a character into the string at index nr")]
    public class StringInsertAtIndex : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmString stringVariable;

        [Tooltip("The character to insert")]
        public FsmString character;

        [Tooltip("Index to place the character")]
        public FsmInt index;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The string result with the character included")]
        public FsmString stringResult;

        public FsmEvent error;

        public override void Reset()
        {
            stringVariable = null;
            index = null;
            character = null;
            stringResult = null;
        }

        public override void OnEnter()
        {
            if (stringVariable.Value.Length >= index.Value)
            {
                stringResult.Value = stringVariable.Value.Insert(index.Value, character.Value);
            }
            else
            {
                Fsm.Event(error);
            }
            Finish();
        }
    }
}