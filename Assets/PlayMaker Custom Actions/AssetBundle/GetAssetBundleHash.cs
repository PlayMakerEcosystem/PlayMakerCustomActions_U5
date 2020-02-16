// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("AssetBundle")]
	[Tooltip("Get the hash for the given AssetBundle, and its isValid value.")]
	public class GetAssetBundleHashIsValid : FsmStateAction
	{
		
		[ObjectType(typeof(AssetBundle))]
		[Tooltip("the AssetBundle")]
		public FsmObject assetBundle;

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
			bool _isValid = false;

			AssetBundle _ab = assetBundle.Value as AssetBundle;
			if (assetBundle != null)
			{
				AssetBundleManifest _m = (AssetBundleManifest)_ab.LoadAsset<AssetBundleManifest> ("assetbundlemanifest");

				if (_m != null)
				{
					Hash128 _hash = _m.GetAssetBundleHash (assetBundleName.Value);

					_isValid = _hash.isValid;
				}

			}

			isValid.Value = _isValid;

			Fsm.Event (_isValid ? isValidEvent : isNotValidEvent);

				
			Finish();
		}
	}
}