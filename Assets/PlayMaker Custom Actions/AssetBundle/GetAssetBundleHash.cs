// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("AssetBundle")]
	[Tooltip("Get the hash for the given AssetBundle, and its isValid value.")]
	public class GetAssetBundleHashIsValid : FsmStateAction
	{
		[Tooltip("Name of the asset bundle.")]
		public FsmString assetBundleName;

		[Tooltip("True if AssetBundle Hash is valid")]
		[UIHint(UIHint.Variable)]
		public FsmBool isValid;

		[Tooltip("Event sent if AssetBundle Hash isvalid")]
		public FsmEvent isValidEvent;

		[Tooltip("Event sent if AssetBundle Hash is not valid")]
		public FsmEvent isNotValidEvent;

		public override void Reset()
		{
			isValid = null;
			assetBundleName = null;
		}

		public override void OnEnter()
		{
			Hash128 _hash =	AssetBundleManifest.GetAssetBundleHash (assetBundleName.Value);

			bool _isValid = _hash != null && _hash.isValid;

			isValid.Value = _isValid;

			Fsm.Event (_isValid ? isValidEvent : isNotValidEvent);

				
			Finish();
		}
	}
}