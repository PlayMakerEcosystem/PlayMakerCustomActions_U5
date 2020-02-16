// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10522.0


using UnityEngine;
using System;
using System.Text;
using System.Security.Cryptography;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerPrefs Encryption")]
	[Tooltip("Sets the value of the preference identified by key with Simple Hashing - MD5 or SH1.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10522.0")]
	public class PlayerPrefsSetIntWithSimpleHashing : FsmStateAction
	{
		[CompoundArray("Count", "Key", "Value")]
		[Tooltip("Case sensitive key.")]
		public FsmString[] keys;
		public FsmInt[] values;

		[ActionSection("Options")]
		[Tooltip("Optional. You can leave blank if wanted")]
		public FsmString Password;
		public Selection selection;



		public enum Selection {MD5, SH1};

		public override void Reset()
		{
			keys = new FsmString[1];
			values = new FsmInt[1];
			Password = null;

		}

		public override void OnEnter()
		{
		

			for(int i = 0; i<keys.Length;i++){
				if(!keys[i].IsNone || !keys[i].Value.Equals("")) PlayerPrefs.SetInt(keys[i].Value, values[i].IsNone ? 0 : values[i].Value);

				string tempString = values[i].Value.ToString();
				
				switch(selection){
				case Selection.SH1:
					tempString  = ComputeHash (tempString+Password,new SHA1CryptoServiceProvider());
					break;
				case Selection.MD5:
					tempString = ComputeHash (tempString+Password,new MD5CryptoServiceProvider());
					break;
					
				}

				string tempString2 =keys[i].Value+"_hash";

				if(!String.IsNullOrEmpty(tempString2)) PlayerPrefs.SetString(tempString2, tempString);
			}


			Finish();
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
