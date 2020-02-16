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
	[Tooltip("Enable/Disable multiple GameObjects at once.")]
	public class ActivateGameObjects : FsmStateAction
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

		[Tooltip("Reset the game objects when exiting this state. Useful if you want an object to be active only while this state is active.")]
		public FsmBool resetOnExit;

		//contains all IDs of changed GameObjects to accurately reset each on exit
		private List<int> changedEntries = new List<int>();
		private int prevAmount;

		public override void Reset()
		{
			gameObjects = new FsmGameObject[0];
			enable = new FsmBool[0];
			applyToAll = new FsmBool { UseVariable = true };
			recursive = false;
			resetOnExit = false;
		}

		public override void OnEnter()
		{
			Default();

			for(int i = 0; i < gameObjects.Length; i++)
			{
				GameObject go = gameObjects[i].Value;

				if(go.activeInHierarchy != enable[i].Value)
				{
					changedEntries.Add(i);
					go.SetActive(enable[i].Value);

					if(recursive.Value)
					{
						foreach(var child in go.GetComponentsInChildren<Transform>())
						{
							child.gameObject.SetActive(enable[i].Value);
						}
					}
				}
			}

			Finish();
		}
		
		private void Default()
		{
			//if the amount of array entries changes, set the default value to all unchanged entries
			if(prevAmount != gameObjects.Length)
			{
				int i = 0;
				foreach(var go in gameObjects)
				{
					if(!go.Value)
					{
						go.UseVariable = true;
						enable[i].Value = true;
					}
					
					i++;
				}
				prevAmount = gameObjects.Length;
			}

			//sets all 'Enable' bools to the one from 'Enable All', if it's not None and "unlock" unset GO's
			if(!applyToAll.IsNone)
			{
				foreach(var go in gameObjects)
				{
					if(go.IsNone) go.UseVariable = false;
				}

				foreach(var item in enable)
				{
					item.Value = applyToAll.Value;
				}
			} else
			{
				foreach(var go in gameObjects)
				{
					if(go == null)
						go.UseVariable = true;
				}
			}
		}

		public override void OnExit()
		{
			//skip if not wanting to reset
			if(!resetOnExit.Value) return;

			//reverse active state if it was changed
			foreach(var entry in changedEntries)
			{
				GameObject go = gameObjects[entry].Value;
				go.SetActive(!go.activeInHierarchy);
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
	}
}
