// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10241

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Enables/Disables a Script in a single GameObject or a range by tag (with layer filter).")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10241")]
    public class EnableScript : FsmStateAction
	{
		[ActionSection("Setup")]
        [Tooltip("The GameObject that owns the Behaviour.")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Optionally drag a script directly into this field (Script name will be ignored).")]
		[TitleAttribute("Dropdown")]
		public Behaviour component;

		[Tooltip("The name of the Script to enable/disable. Note: No space in script name. Name must be same as Project view name")]
		[TitleAttribute("or Script Name")]
		public FsmString script;

		[ActionSection("Options")]
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
        [Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Should the object Children be included?")]
		public FsmBool inclChildren;
	
		[ActionSection("Script Option")]
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True to enable/disable all scripts in gameobject (Script name or component will be ignored).")]
		public FsmBool allScripts;



		Behaviour componentTarget;

		public override void Reset()
		{
			gameObject = null;
			script = null;
			component = null;
			enable = true;
			allScripts = false;
			inclChildren = false;

		}


		public override void OnEnter()
		{
			var gos = Fsm.GetOwnerDefaultTarget(gameObject);
			if (gos == null)
			{
				Debug.LogWarning("missing gameObject: "+ gos.name);
				Finish();
			}

			if (allScripts.Value == false && component == null && string.IsNullOrEmpty(script.Value))
			{
				Debug.LogWarning(gos.name + " missing script or co: " + script.Value);
				Finish();
			}



			if (allScripts.Value == false)
			DoEnableScript(Fsm.GetOwnerDefaultTarget(gameObject));

			if (allScripts.Value == true)
				DisableAll(Fsm.GetOwnerDefaultTarget(gameObject));

			Finish();
		}

		void DoEnableScript(GameObject go)
		{
			if (go == null)
			{
				return;
			}

			if (component != null)
			{
				componentTarget = component as Behaviour;
				componentTarget.enabled = enable.Value;
			}
			else
			{
				(go.gameObject.GetComponent(script.Value) as Behaviour).enabled = enable.Value;
			}

			if (inclChildren.Value == true)
			{

				for(int i=0; i< go.transform.childCount; i++)
				{
					var child = go.transform.GetChild(i).gameObject;
					if(child != null){

					if (!string.IsNullOrEmpty(script.Value))
					{
					(child.gameObject.GetComponent(script.Value) as Behaviour).enabled = enable.Value;
					}
					else if (string.IsNullOrEmpty(script.Value)){

							Debug.LogWarning("!!!!!! Dropdown does not work with incl child. Please input script name !!!!!!");
							return;
							}
					
						}
				}
				}

			}


		void DisableAll(GameObject go)
		{

			Behaviour[] scriptComponents = go.gameObject.GetComponents<Behaviour>();    
			foreach(Behaviour script in scriptComponents) {
			script.enabled = enable.Value;
			}

			if (inclChildren.Value == true)
			{
				Behaviour[] scriptChildComponents = go.gameObject.GetComponentsInChildren<Behaviour>();    
				foreach(Behaviour script in scriptChildComponents) {
					script.enabled = enable.Value;
				}
			}
		}

	}
}
