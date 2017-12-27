// License: Attribution 4.0 International (CC BY 4.0)
//Author: Deek
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UnityObject)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Enable/Disable multiple Components at once.")]
	public class ActivateComponents : FsmStateAction
	{
		[CompoundArray("Amount", "Component", "Enable")]

		[RequiredField]
		[Tooltip("The current component to enable/disable.")]
		public FsmObject[] components;

		[Tooltip("Wheter to enable/disable the current component.")]
		public FsmBool[] enable;

		[Tooltip("If not 'None', sets all 'Enable' bools to true or false.")]
		public FsmBool applyToAll;

		[Tooltip("Reset the game objects when exiting this state. Useful if you want an object to be active only while this state is active.")]
		public FsmBool resetOnExit;

		//contains all IDs of changed components to accurately reset each on exit
		private List<int> changedEntries = new List<int>();
		private int prevAmount = 0;

		public override void Reset()
		{
			components = new FsmObject[0];
			enable = new FsmBool[0];
			applyToAll = new FsmBool() { UseVariable = true };
			resetOnExit = false;
		}

		public override void OnEnter()
		{
			Default();

			for(int i = 0; i < components.Length; i++)
			{
				var currValue = components[i].Value;
				var currType = currValue.GetType();
				bool en = enable[i].Value;

				if(currType == typeof(GameObject))
				{
					LogError("Component #" + i + " can't be of type 'GameObject'!");
				}

				Behaviour be = currValue as Behaviour;
				Renderer re = currValue as Renderer;
				Collider col = currValue as Collider;

				if(be)
				{
					if(be.enabled != en)
					{
						changedEntries.Add(i);
						be.enabled = en;
						continue;
					}
				}

				if(re)
				{
					if(re.enabled != en)
					{
						changedEntries.Add(i);
						re.enabled = en;
						continue;
					}
				}

				if(col)
				{
					if(col.enabled != en)
					{
						changedEntries.Add(i);
						col.enabled = en;
						continue;
					}
				}

				//--- if the current component type isn't supported ---
				string status = en ? "enabled" : "disabled";
				LogError("Component " + currType.ToString() + " on " + currValue.name
									  + " can't be " + status + " with this action!");
			}

			Finish();
		}

		public override void OnExit()
		{
			//skip if not wanting to reset
			if(!resetOnExit.Value) return;

			//reverse active state if it was changed
			foreach(var entry in changedEntries)
			{
				var currValue = components[entry].Value;

				Behaviour be = currValue as Behaviour;
				Renderer re = currValue as Renderer;
				Collider col = currValue as Collider;

				if(be) be.enabled = !be.enabled;
				if(re) re.enabled = !re.enabled;
				if(col) col.enabled = !col.enabled;
			}
		}

		public override void OnGUI()
		{
			Default();
		}

		void Default()
		{
			//if the amount of array entries changes, set the default value to all unchanged entries
			if(prevAmount != components.Length)
			{
				int i = 0;
				foreach(var be in components)
				{
					if(!be.Value)
					{
						be.UseVariable = true;
						enable[i].Value = true;
					}
					i++;
				}
				prevAmount = components.Length;
			}

			//sets all 'Enable' bools to the one from 'Enable All', if it's not None
			if(!applyToAll.IsNone)
			{
				foreach(var item in enable)
				{
					item.Value = applyToAll.Value;
				}
			}
		}
	}
}
