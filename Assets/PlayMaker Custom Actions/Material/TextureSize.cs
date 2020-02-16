// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;
using System.Text;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Size of String (b, kb, mb, etc)")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12359.0")]
	public class TextureSize : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmTexture textureVariable;


		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

		[Tooltip("Always in mb")]
		public FsmFloat floatMb;
	
		public override void Reset()
		{
			textureVariable = null;
			storeResult = null;
			floatMb = null;
		}

		public override void OnEnter()
		{
			Texture2D texItem = textureVariable.Value as Texture2D;
			byte[] tex = texItem.GetRawTextureData();

			storeResult.Value = BytesToString(tex.LongLength);
			floatMb.Value = (float)(tex.LongLength / (1024.0 * 1024.0));

			texItem = null;
			tex = null;

			Finish();
		}
		
		public String BytesToString(long byteCount)
		{
			string[] suf = { "B", "KiB", "KB", "MB", "GB", "TB", "PB", "EB" };
			if (byteCount == 0)
				return "0" + suf[0];
			long bytes = Math.Abs(byteCount);
			int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
			double num = Math.Round(bytes / Math.Pow(1024, place), 1);
			return (Math.Sign(byteCount) * num).ToString() + suf[place];
		}
		
	}
}
