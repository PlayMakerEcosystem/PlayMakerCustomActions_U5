// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10465.0


using UnityEngine;
using System;
using System.Text;
using System.Security.Cryptography;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Encryption")]
	[Tooltip("Simple Hashing - MD5 or SH1")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10465.0")]
	public class SimpleHashing : FsmStateAction
	{

		[ActionSection("Setup")]
		public Selection selection;
		[ActionSection("Data")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;
		[RequiredField]
		public FsmString stringValue;
		[ActionSection("Options")]
		[Tooltip("Optional. You can leave blank if wanted")]
		public FsmString Password;

		public enum Selection {MD5, SH1};


		public override void Reset()
		{
			stringVariable = null;
			stringValue = null;
			Password = null;
		}

		public override void OnEnter()
		{

			DoSetStringValue();

				Finish();
		}

		
		void DoSetStringValue()
		{
			if (stringVariable == null) return;

			switch(selection){
			case Selection.SH1:
				stringValue.Value = ComputeHash (stringVariable.Value+Password,new SHA1CryptoServiceProvider());
				break;
			case Selection.MD5:
				stringValue.Value = ComputeHash (stringVariable.Value+Password,new MD5CryptoServiceProvider());
				break;

			}

			return;
		}



		public static string ComputeHash(string s, HashAlgorithm algorithm){
	
			byte[] data = algorithm.ComputeHash(Encoding.Default.GetBytes(s));
			System.Text.StringBuilder sb = new StringBuilder();
			for (int i = 0; i < data.Length; ++i) {
				sb.Append(data[i].ToString("x2"));
			}
			return sb.ToString();
		}


	}
}
