// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10241

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Enables/Disables a Script by Tag (with layer filter).")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10241")]
    public class EnableScriptByTag : FsmStateAction
	{
		[ActionSection("Setup")]


		[Tooltip("The name of the Script to enable/disable. Note: No space in script name. Name must be same as Project view name")]
		[TitleAttribute("Script Name")]
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

		[ActionSection("Tag and Layer Options")]
		[UIHint(UIHint.Tag)]
		public FsmString tag;
		[TitleAttribute("incl Layer Filter")]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Also filter by layer?")]
		public FsmBool layerFilterOn;
		[Tooltip("Filter layer on child?")]
		public FsmBool inclLayerFilterOnChild;
		[UIHint(UIHint.Layer)]
		public int layer;




		Behaviour componentTarget;

		public override void Reset()
		{
			script = null;
			enable = true;
			allScripts = false;
			inclChildren = false;
			layerFilterOn = false;
			layer = 0;
			inclLayerFilterOnChild = false;
		}


		public override void OnEnter()
		{

			if (allScripts.Value == false && string.IsNullOrEmpty(script.Value))
			{
				Debug.LogWarning(" !!! Check your setup ");
				return;
			}

			if (allScripts.Value == false && layerFilterOn.Value == false)
				TagSetup();

			if (allScripts.Value == false && layerFilterOn.Value == true)
				TagAndLayerSetup();
		

			if (allScripts.Value == true && layerFilterOn.Value == false)
				DisableAll();

			if (allScripts.Value == true && layerFilterOn.Value == true)
				DisableAllwithlayer();

	

			Finish();
		}


		void DisableAll()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			for(int i = 0; i<objtag.Length;i++){
				
				Behaviour[] scriptComponents = objtag[i].gameObject.GetComponents<Behaviour>();    
				foreach(Behaviour script in scriptComponents) {
					
					script.enabled = enable.Value;
				}
				
				if (inclChildren.Value == true)
				{
					Behaviour[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<Behaviour>();    
					foreach(Behaviour script in scriptChildComponents) {
						script.enabled = enable.Value;
					}
				}
			}
		}

		void DisableAllwithlayer()
		{

			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			for(int i = 0; i<objtag.Length;i++){
				
				if (objtag[i].layer == layer){
					
					
					Behaviour[] scriptComponents = objtag[i].gameObject.GetComponents<Behaviour>();    
					foreach(Behaviour script in scriptComponents) {
						
						script.enabled = enable.Value;
					}
					
					if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
					{
						Behaviour[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<Behaviour>();    
						foreach(Behaviour script in scriptChildComponents) {
							script.enabled = enable.Value;
						}
					}
					
					if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
					{
						
						foreach(Behaviour script in objtag[i].gameObject.GetComponentsInChildren<Behaviour>()) {
							if (script.gameObject.layer == layer)
								script.enabled = enable.Value;
						}
						
					}
					
				}
			}

		}

		void TagSetup()
		{

			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);

			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}

			for(int i = 0; i<objtag.Length;i++){

			{
					var target = objtag[i].gameObject.GetComponent(script.Value);

					if (target != null)
						(target.GetComponent(script.Value) as Behaviour).enabled = enable.Value;
			}


			if (inclChildren.Value == true)
			{
				
					for(int a=0; a<objtag[i].transform.childCount; a++)
					{
						var child = objtag[i].transform.GetChild(a).gameObject;
						if(child != null)

						{
						(child.gameObject.GetComponent(script.Value) as Behaviour).enabled = enable.Value;
						}

					}
				
			}

			}

		}

		void TagAndLayerSetup()
		{
			
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);

			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			for(int i = 0; i<objtag.Length;i++){

				if (objtag[i].layer == layer){

				{
						var target = objtag[i].gameObject.GetComponent(script.Value);
						
						if (target != null)
						(target.GetComponent(script.Value) as Behaviour).enabled = enable.Value;
				}
				
				
					if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false){
				{
					
					for(int a=0; a<objtag[i].transform.childCount; a++)
					{
						var child = objtag[i].transform.GetChild(a).gameObject;
						if(child != null)

						{
							(child.gameObject.GetComponent(script.Value) as Behaviour).enabled = enable.Value;
						}
				
					}
					
				}

				}
					if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true){
						{
							
							for(int a=0; a<objtag[i].transform.childCount; a++)
							{
								var child = objtag[i].transform.GetChild(a).gameObject;
								if (child.layer == layer){
								if(child != null)
							
								{
									(child.gameObject.GetComponent(script.Value) as Behaviour).enabled = enable.Value;
								}
						
								}
							}
							
						}
						
					}


			}
			
		}



		}
	}
}
