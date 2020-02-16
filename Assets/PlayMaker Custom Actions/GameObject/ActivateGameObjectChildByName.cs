// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: activate, gameobject, child, children
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions {
  [ActionCategory(ActionCategory.GameObject)]
  [Tooltip("Activates/deactivates one or more Game Object children by name. Use this to hide/show areas, or enable/disable many Behaviours at once. Optionally reverse the action on exit.")]
  public class ActivateGameObjectChildByName : FsmStateAction {
    [RequiredField]
    [Tooltip("The parent GameObject.")]
    public FsmOwnerDefault gameObject;
    [Tooltip("Name(s) of children to activate/deactivate. Slashes may be used to denote heirarchical paths.")]
    public FsmString[] names;
    [RequiredField]
    [Tooltip("Activate/deactivate the game object.")]
    public FsmBool activate;
    [Tooltip("Apply recursively to child objects.")]
    public FsmBool recursive;
    [Tooltip("Reverse activate action on exit.")]
    public FsmBool resetOnExit;
    [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
    public bool everyFrame;

    // store the game object that we activated on enter
    // so we can de-activate it on exit.
    HashSet<GameObject> activatedGameObjects = new HashSet<GameObject>();
    bool equal;

    public override void Reset() {
      gameObject = null;
      names = new FsmString[1];
      activate = false;
      recursive = false;
      everyFrame = false;
      resetOnExit = false;
    }

    public override void OnEnter() {
      activatedGameObjects.Clear();
      DoActivateGameObject();

      if (!everyFrame) {
        Finish();
      }
    }

    public override void OnUpdate() {
      DoActivateGameObject();
    }

    public override void OnExit() {
      if (!resetOnExit.Value) {
        return;
      }
      foreach (var obj in activatedGameObjects) {
        obj.SetActive(!activate.Value);
      }
    }

    void DoActivateGameObject() {
      var root = Fsm.GetOwnerDefaultTarget(gameObject);

      if (root == null) {
        return;
      }

      DoActivateGameObject(root);
    }

    void DoActivateGameObject(GameObject node) {
      if (node == null) {
        return;
      }

      foreach (var childName in names) {
        var child = node.transform.Find(childName.Value);
        if (child != null) {
          activatedGameObjects.Add(child.gameObject);
          child.gameObject.SetActive(activate.Value);
          // If childName is a path/heirarchy, activate all intermediate nodes.
          var link = child;
          while (link.transform.parent != node.transform && link.transform.parent != null) {
            link = link.parent;
            activatedGameObjects.Add(link.gameObject);
            link.gameObject.SetActive(activate.Value);
          }
          if (recursive.Value) {
            DoActivateGameObject(child.gameObject);
          }
        }
      }
    }
  }
}