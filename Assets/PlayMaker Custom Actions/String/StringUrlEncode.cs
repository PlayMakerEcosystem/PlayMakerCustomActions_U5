// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: url

using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Url encode")]
    [HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12402.0")]
	public class StringUrlEncode : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;


		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

	
		public override void Reset()
		{
			stringVariable = null;
			storeResult = null;
		}

		public override void OnEnter()
		{

			storeResult.Value = UrlEncode(stringVariable.Value);
			Finish();
		}
		
		string UrlEncode(string url)
		{
			Dictionary<string, string> toBeEncoded = new Dictionary<string, string>() { { "%", "%25" }, { "!", "%21" }, { "#", "%23" }, { " ", "%20" },
				{ "$", "%24" }, { "&", "%26" }, { "'", "%27" }, { "(", "%28" }, { ")", "%29" }, { "*", "%2A" }, { "+", "%2B" }, { ",", "%2C" }, 
				{ "/", "%2F" }, { ":", "%3A" }, { ";", "%3B" }, { "=", "%3D" }, { "?", "%3F" }, { "@", "%40" }, { "[", "%5B" }, { "]", "%5D" } };
			Regex replaceRegex = new Regex(@"[%!# $&'()*+,/:;=?@\[\]]");
			MatchEvaluator matchEval = match => toBeEncoded[match.Value];
			string encoded = replaceRegex.Replace(url, matchEval);
			return encoded;
		}
		
	}
}
