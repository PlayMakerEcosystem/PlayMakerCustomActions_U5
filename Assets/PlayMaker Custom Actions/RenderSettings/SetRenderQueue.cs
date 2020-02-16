// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the RenderQueue of an game object's materials. The default render queue for Transparent objects is 3000. If you want something to draw on top, choose a higher queue number")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11274.0")]
	public class SetRenderQueue : FsmStateAction
	{
		
		[ActionSection("Setup")]
		[Tooltip("The GameObject that owns the Renderer.")]

		public FsmOwnerDefault gameObject;

		[Tooltip("Render queue")]
		[TitleAttribute("RenderQueue Order")]

		public FsmInt[] m_queues;

		//[ActionSection("Options")]

		
		public override void Reset()
		{
			gameObject = null;
			m_queues = new FsmInt[1]{3000};
			
		}


		public override void OnEnter()
		{

			var gos = Fsm.GetOwnerDefaultTarget(gameObject);
			if (gos == null)
			{
				Debug.LogWarning("missing gameObject: "+ gos.name);
				Finish();
			}


			RenderSetupObj();

			Finish();
			
		}

		
		void RenderSetupObj(){

			Renderer _target = Fsm.GetOwnerDefaultTarget(gameObject).gameObject.GetComponent<Renderer>();

			if (_target == null)
			{
				Debug.LogWarning("missing renderer: "+ Fsm.GetOwnerDefaultTarget(gameObject).name);
				Finish();
			}

			if (m_queues == null) {
				Debug.LogWarning("missing queues data: "+ Fsm.GetOwnerDefaultTarget(gameObject).name);
				Finish();
			}

			Material[] materials = _target.materials;

			for (int i = 0; i < materials.Length && i < m_queues.Length; ++i) {
				materials[i].renderQueue = m_queues[i].Value;
			}
			Finish();
		}
			


		}
		
		
	}
