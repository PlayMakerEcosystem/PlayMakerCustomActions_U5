using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEditor;
using UnityEngine;


public class CreateFsmTemplate : Editor {


    [MenuItem("PlayMaker/Test/Generate FsmTemplate")]
    public static void GenerateFsmTemplate()
    {

        FsmTemplate fsmTemplate = (FsmTemplate)CreateInstance(typeof(FsmTemplate));
        fsmTemplate.Category = "test";

        fsmTemplate.fsm = new Fsm();
        fsmTemplate.fsm.Reset(null);
        fsmTemplate.fsm.Name = "Test Fsm Template";
        fsmTemplate.fsm.UsedInTemplate = fsmTemplate;


        FsmState lastState = new FsmState(fsmTemplate.fsm)
        {
            Name = "Done",
            Position = new Rect(130, 120, 128, 15)
        };

        FsmState myState = new FsmState(fsmTemplate.fsm)
        {
            Name = "My State",
            Position = new Rect(130, 60, 128, 30),
            Transitions = new[]
          {
                new FsmTransition {
                    FsmEvent = FsmEvent.Finished,
                    ToState = lastState.Name
                }
            }
        };

        FsmEvent startEvent = new FsmEvent("TEST EVENT");

        FsmTransition startTransition = new FsmTransition
        {
            FsmEvent = startEvent,
            ToState = myState.Name
        };

        //var finishAction = new FinishHotspotAction();
        //// Documentation says: "Called in the editor when an action is added to a state or reset". Not clear if via code is needed, apparently it does not generate errors.
        ////finishAction.Reset();
        //finishState.Actions = new FsmStateAction[] { finishAction };
        //finishState.SaveActions();

        fsmTemplate.fsm.States = new[] { myState, lastState };
        fsmTemplate.fsm.Events = new[] { startEvent };
        fsmTemplate.fsm.GlobalTransitions = new[] { startTransition };

        AssetDatabase.CreateAsset(fsmTemplate, @"Assets\Playmaker Custom Templates\test\" + "Test" + ".asset");
        AssetDatabase.SaveAssets();


    }

    [MenuItem("PlayMaker/Test/Generate FsmTemplate bad")]
    public static void GenerateTemplateBad()
    {
        string effectName = "testBad";

        var fsmTemplate = (FsmTemplate)CreateInstance(typeof(FsmTemplate));

        fsmTemplate.Category = "HotspotEffect";

        fsmTemplate.fsm = new Fsm();
        fsmTemplate.fsm.Reset(null);

        var startEvent = new FsmEvent("START "+ effectName);
        var startTransition = new FsmTransition
        {
            FsmEvent = startEvent,
            ToState = effectName
        };
        var applyState = new FsmState(fsmTemplate.fsm)
        {
            Name = effectName,
            Position = new Rect(130, 60, 128, 16),
            Transitions = new[]
            {
            new FsmTransition {
                FsmEvent = FsmEvent.Finished,
                ToState = "After " + effectName
            }
        }
        };
        var finishState = new FsmState(fsmTemplate.fsm)
        {
            Name = "After " + effectName,
            Position = new Rect(130, 120, 128, 16)
        };
        var finishAction = new DebugLog();
        finishState.Actions = new FsmStateAction[] { finishAction };
        finishState.SaveActions();

        FsmState[] _states = fsmTemplate.fsm.States;
        ArrayUtility.Add(ref _states, finishState);
        ArrayUtility.Add(ref _states, applyState);
        fsmTemplate.fsm.States = _states;

        fsmTemplate.fsm.Events = new[] { startEvent };
        fsmTemplate.fsm.GlobalTransitions = new[] { startTransition };
        fsmTemplate.fsm.Name = effectName;
        fsmTemplate.fsm.UsedInTemplate = fsmTemplate;


        AssetDatabase.CreateAsset(fsmTemplate, @"Assets\Playmaker Custom Templates\test\" + effectName + ".asset");
        AssetDatabase.SaveAssets();
    }
}
