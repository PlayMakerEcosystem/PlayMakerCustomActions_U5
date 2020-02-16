// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using Mathos;
using Mathos.Parser;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory(ActionCategory.Math)]
    [Tooltip("Uses Mathos parser to implement math expression action.")]
    public class MathExpression : FsmStateAction
    {
        [Tooltip("Expression to evaluate.")]
        public FsmString expression;

        [Tooltip("Store the result in a float variable")]
        [UIHint(UIHint.Variable)]
        public FsmFloat storeResultAsFloat;

        [Tooltip("Store the result in an int variable")]
        [UIHint(UIHint.Variable)]
        public FsmInt storeResultAsInt;

        public bool everyFrame;
        
        private MathParser parser;
        private string cachedExpression;
        private ReadOnlyCollection<string> tokens;
        private List<NamedVariable> usedVariables = new List<NamedVariable>();

        public override void Awake()
        {
            parser = new MathParser();
        }

        public override void OnEnter()
        {
            ParseExpression();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            ParseExpression();
        }

        private void ParseExpression()
        {
            if (expression.Value != cachedExpression)
            {
                BuildAndCacheExpression();
            }

            foreach (var variable in usedVariables)
            {
                switch (variable.VariableType)
                {
                    case VariableType.Float:
                        parser.LocalVariables[variable.Name] = ((FsmFloat)variable).Value;
                        break;

                    case VariableType.Int:
                        parser.LocalVariables[variable.Name] = ((FsmInt)variable).Value;
                        break;

                    case VariableType.Bool:
                        parser.LocalVariables[variable.Name] = ((FsmBool) variable).Value ? 1 : 0;
                        break;

                    default:
                        Debug.Log("Unsupported variable type: " + variable.Name + " (" + variable.VariableType + ")" );
                        break;
                          
                }
                
                // parser.LocalVariables.Add(variable.Name, (double) variable.RawValue);
            }

            var result = parser.Parse(tokens);

            if (!storeResultAsFloat.IsNone)
            {
                storeResultAsFloat.Value = (float) result;
            }

            if (!storeResultAsInt.IsNone)
            {
                storeResultAsInt.Value = Mathf.FloorToInt((float) result);
            }
        }

        private void BuildAndCacheExpression()
        {
            // we can parse tokens once and save them

            tokens = parser.GetTokens(expression.Value);

            // remove any variables previously added to parser.LocalVariables
            // this should be cheaper than getting a new parser

            foreach (var variable in usedVariables)
            {
                parser.LocalVariables.Remove(variable.Name);
            }

            // find variables in tokens and add them to used variables list

            usedVariables.Clear();

            foreach (var token in tokens)
            {
                var variable = Fsm.Variables.FindVariable(token) ?? FsmVariables.GlobalVariables.FindVariable(token);
                if (variable != null && !usedVariables.Contains(variable))
                {
                    usedVariables.Add(variable);
                }
            }

            // store the expression so we know if its changed

            cachedExpression = expression.Value;
        }


    }

}
