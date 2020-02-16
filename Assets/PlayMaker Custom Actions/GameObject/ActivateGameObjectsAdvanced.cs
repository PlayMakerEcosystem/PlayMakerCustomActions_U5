// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Author: Deek
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
  [ActionCategory(ActionCategory.GameObject)]
  [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
  [Tooltip(
    "Enable/Disable multiple GameObjects at once. Advanced version allows for a delay between each GameObject and to run this action every frame.")]
  public class ActivateGameObjectsAdvanced : FsmStateAction
  {
    [CompoundArray("Amount", "GameObject", "Enable")]
    [RequiredField]
    [Tooltip("The current GameObject to enable/disable.")]
    public FsmGameObject[] gameObjects;

    [Tooltip("Wheter to enable/disable the current GameObject.")]
    public FsmBool[] enable;

    [Tooltip("If not 'None', sets all 'Enable' bools to true or false.")]
    public FsmBool applyToAll;

    [Tooltip("Recursively activate/deactivate all children.")]
    public FsmBool recursive;

    [Tooltip(
      "Reset the game objects when exiting this state. Useful if you want an object to be active only while this state is active.")]
    public FsmBool resetOnExit;


    [ActionSection("Optionally")] [Tooltip("Define a delay between enabling/disabling each GameObject.")]
    public FsmFloat delay;

    [Tooltip("Repeat this action every frame.")]
    public FsmBool everyFrame;

    //contains all IDs of changed GameObjects to accurately reset each on exit
    private List<int> changedEntries = new List<int>();
    private float timer;
    private int prevAmount;

    public override void Reset()
    {
      gameObjects = new FsmGameObject[0];
      enable = new FsmBool[0];
      applyToAll = new FsmBool {UseVariable = true};
      recursive = false;
      resetOnExit = false;
      
			delay = 0f;
      everyFrame = false;
    }

    public override void OnEnter()
    {
      changedEntries.Clear();
      prevAmount = 0;
      timer = 0f;

      DoActivateGameObjects();

      if (!everyFrame.Value && delay.Value <= 0) Finish();
    }

    public override void OnUpdate()
    {
      if (!everyFrame.Value) timer += Time.deltaTime;

      DoActivateGameObjects();
    }

    private void DoActivateGameObjects()
    {
      Default();

      bool finishedLoop = false;

      for (int i = 0; i < gameObjects.Length; i++)
      {
        if (changedEntries.Contains(i)) continue;

        if (timer >= delay.Value || everyFrame.Value)
        {
          GameObject go = gameObjects[i].Value;
          bool en = enable[i].Value;

          if (go.activeInHierarchy != en)
          {
            changedEntries.Add(i);
            go.SetActive(en);

            if (recursive.Value)
            {
              foreach (var child in go.GetComponentsInChildren<Transform>(true))
                child.gameObject.SetActive(en);
            }
          }

          if (i == gameObjects.Length - 1) finishedLoop = true;

          timer = 0f;
        }
      }

      if (finishedLoop && delay.Value > 0f && !everyFrame.Value) Finish();
      if (finishedLoop && everyFrame.Value) changedEntries.Clear();
    }

    private void Default()
    {
      //if the amount of array entries changes, set the default value to all unchanged entries
      if (prevAmount != gameObjects.Length)
      {
        int i = 0;

        foreach (var go in gameObjects)
        {
          if (!go.Value)
          {
            go.UseVariable = true;
            enable[i].Value = true;
          }

          i++;
        }

        prevAmount = gameObjects.Length;
      }
      else
      {
        //sets all 'Enable' bools to the one from 'Enable All', if it's not None
        if (!applyToAll.IsNone)
        {
          foreach (var go in gameObjects)
            go.UseVariable = false;

          foreach (var item in enable)
            item.Value = applyToAll.Value;
        }
      }
    }

    public override void OnExit()
    {
      //skip if not wanting to reset
      if (!resetOnExit.Value) return;

      //reverse active state if it was changed
      foreach (var entry in changedEntries)
      {
        GameObject go = gameObjects[entry].Value;
        bool en = !go.activeInHierarchy;
        go.SetActive(en);

        if (!recursive.Value) continue;

        foreach (var child in go.GetComponentsInChildren<Transform>(true))
        {
          child.gameObject.SetActive(en);
        }
      }
    }

    //explicitly declare using OnGUI
    public override void OnPreprocess()
    {
      Fsm.HandleOnGUI = true;
    }

    public override void OnGUI()
    {
      Default();
    }

    public override string ErrorCheck()
    {
      if (everyFrame.Value && delay.Value > 0)
        return "Please set either a delay or select 'Every Frame', as only one of the two options can be applied!";
      
      return "";
    }
  }
}