// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Author : Deek

using System.IO;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("System")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=15458.0")]
	[Tooltip("Returns the file-extension of an Asset at the speficied path.")]
	public class GetFileExtension : FsmStateAction
	{
		[Tooltip("Set the path the file is at, starting after Assets (/Some/File/At/foo.extension).")]
		public FsmString path;

		[UIHint(UIHint.Variable)]
		[Tooltip("The file-extension.")]
		public FsmString extension;

		public override void Reset()
		{
			path = null;
			extension = null;
		}

		public override void OnEnter()
		{

			if(path.Value != null && path.Value != "")
			{
				extension.Value = Path.GetExtension(path.Value);
				if(extension.IsNone)
				{
					Debug.LogWarning("GetFileExtension.cs: File couldn't be found!");
				}
			} else
			{
				Debug.LogWarning("GetFileExtension.cs: File-Path is empty!");
			}

			Finish();
		}
	}
}
