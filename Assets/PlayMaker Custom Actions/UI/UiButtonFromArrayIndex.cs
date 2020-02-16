// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Set up multiple button events in a single action.")]
    public class UiButtonFromArrayIndex : FsmStateAction
    {

        //[RequiredField]
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.GameObject)]
        [Tooltip("The Array Variable to use. This need to be the gameobjects with the Button component")]
        public FsmArray buttonArray;

        [Tooltip("Where to send the events.")]
        public FsmEventTarget eventTarget;

        [Tooltip("Send an  event when a button is Clicked.")]
        public FsmEvent clickEvent;

        [Tooltip("Get the index number from the pressed button. (1st button is index 0, 2nd button is index 1, and so on")]
        public FsmInt clickIndex;

        [SerializeField]
        private UnityEngine.UI.Button[] buttons;

        [SerializeField]
        private GameObject[] cachedGameObjects;

        private UnityAction[] actions;

        private int clickedButton;

        public override void Reset()
        {
            buttonArray = null;
            clickEvent = null;
            clickIndex = new FsmInt() { UseVariable = true };

        }

        /// <summary>
        /// Try to do all GetComponent calls in Preprocess as part of build
        /// But sometimes the values are not known at build time...
        /// </summary>

        public override void OnPreprocess()
        {
            InitButtons();
        }

        public override string ErrorCheck()
        {
            if (buttonArray.IsNone)
                return "No Array set";
            else return "";
        }

        private void InitButtons()
        {
            //Debug.Log(buttonArray.ElementType);
            buttons = new UnityEngine.UI.Button[buttonArray.Length];
            cachedGameObjects = new GameObject[buttonArray.Length];
            actions = new UnityAction[buttonArray.Length];

            for (var i = 0; i < buttonArray.Length; i++)
            {
                GameObject go = buttonArray.Get(i) as GameObject;
                if (go != null)
                {
                    if (cachedGameObjects[i] != go)
                    {
                        buttons[i] = go.GetComponent<UnityEngine.UI.Button>();
                        cachedGameObjects[i] = go;
                    }
                }
            }
        }

        public override void OnEnter()
        {
            InitButtons();

            for (var i = 0; i < buttons.Length; i++)
            {
                var button = buttons[i];
                if (button == null) continue;

                var index = i;
                actions[i] = () => { OnClick(index); };
                button.onClick.AddListener(actions[i]);
            }
        }

        public override void OnExit()
        {
            for (var i = 0; i < buttonArray.Length; i++)
            {
                GameObject go = buttonArray.Get(i) as GameObject;
                if (go == null) continue;
                go.GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(actions[i]);
            }
        }

        public void OnClick(int index)
        {
            clickIndex.Value = index;

            Fsm.Event(eventTarget, clickEvent);
            Finish();
        }
    }
}