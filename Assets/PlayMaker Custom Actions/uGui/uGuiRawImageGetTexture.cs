// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Get the texture of a UGui RawImage component.")]
	public class uGuiRawImageGetTexture : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(UnityEngine.UI.RawImage))]
		[Tooltip("The GameObject with the RawImage ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The texture of the UGui RawImage component.")]
		public FsmTexture texture;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;


		private UnityEngine.UI.RawImage _texture;

		Texture _originalTexture;

		public override void Reset()
		{
			gameObject = null;
			texture = null;
			resetOnExit = null;
		}
		
		public override void OnEnter()
		{

			GameObject _go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go!=null)
			{
				_texture = _go.GetComponent<UnityEngine.UI.RawImage>();
			}

			if (resetOnExit.Value)
			{
				_originalTexture = _texture.texture;
			}

			DoSetValue();

			Finish();
		}

		void DoSetValue()
		{

			if (_texture!=null)
			{
				texture.Value = _texture.texture as Texture2D;
			}
		}

		public override void OnExit()
		{
			if (resetOnExit.Value)
			{
				texture.Value = _originalTexture  as Texture2D;
			}
		}
	}
}
