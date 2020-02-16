// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: line renderer number of vertex position

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Renderer)]
	[Tooltip("Set the number of positions of a lineRenderer")]
	public class SetLineRendererPositionsCount : FsmStateAction
	{

		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("The GameObject with the LineRenderer component.")]
		[CheckForComponent(typeof(LineRenderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The number of positions")]
		[RequiredField]
		public FsmInt count;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		LineRenderer _lr;

		public override void Reset ()
		{
			gameObject = null;
			count = 2;
			everyFrame = false;

		}

		public override void OnEnter ()
		{
			SetPositionCount();

			if (!everyFrame)
			{
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			SetPositionCount();
		}

		void SetPositionCount()
		{
			var go = Fsm.GetOwnerDefaultTarget (gameObject);
			if (go == null) {
				return;
			}
			
			_lr = go.GetComponent<LineRenderer>();

			if (_lr == null)
			{
				return;
			}

			#if UNITY_5_5_OR_NEWER 
				_lr.positionCount = Mathf.Max (1,count.Value);
			#else
				_lr.SetVertexCount(Mathf.Max (1,count.Value));
			#endif

		}
	}
}
