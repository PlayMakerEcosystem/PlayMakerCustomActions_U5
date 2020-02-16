// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// original source: https://bitbucket.org/gambitforhire/twitter-search-with-unity

using UnityEngine;

using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Twitter")]
	[Tooltip("Twitter API 1.1 - GET / POST - open help url for more info")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=12477.0")]
	public class Twitter_Api : FsmStateAction
	{

		[ActionSection("OAuth Setup")]
		[RequiredField]
		public FsmString oauthConsumerKey;
		[RequiredField]
		public FsmString oauthConsumerSecret;
		[RequiredField]
		public FsmString oauthToken;
		[RequiredField]
		public FsmString oauthTokenSecret;
	
		private string oauthNonce;

		[ActionSection("Query Setup")]
		[RequiredField]
		[Tooltip("Build a query string in this format twitter '(Parameters=Value)' For example: (q=Playmaker)(count=100)(result_type=popular)")]
		public FsmString query;

		[ActionSection("URL Setup")]
		[Tooltip("The main Api Url - Leave blank for default")]
		public FsmString setTwitterUrl;
		[Tooltip("The api url extension - Leave blank for default(search/tweets.json) ")]
		public FsmString setTwitterUrlType;
		private string twitterUrl;

		[ActionSection("Debug")]
		[Tooltip("See data in unity console")]
		public FsmBool debugOn;
	
		[ActionSection("Result")]
		[Tooltip("Raw Json data from Twitter")]
		public FsmString jsonSearchResult;
		[Tooltip("Your query timestamp")]
		public FsmString oauthTimeStamp;
		[UIHint(UIHint.Variable)] 
		[Tooltip("How far the download progressed (0-1).")]
		public FsmFloat progress;
		[Tooltip("Error message if there was an error during the download.")]
		public FsmString errorString;

		[ActionSection("Events")] 
		[Tooltip("Event to send if there was an error.")]
		public FsmEvent isError;
		[Tooltip("Event to send when the data has finished loading (progress = 1).")]
		public FsmEvent isDone;

		private WWW wwwObject;


		public override void Reset()
		{
			setTwitterUrl = "https://api.twitter.com/1.1/";
			setTwitterUrlType = "search/tweets.json";
			oauthConsumerKey = null;
			oauthConsumerSecret = null;
			oauthToken = null;
			oauthTokenSecret = null;
			jsonSearchResult = null;
			query = null;
			errorString = null;
			isDone = null;
			isError = null;
			progress = null;
			oauthTimeStamp = null;
			debugOn = false;
		}

		public override void OnEnter()
		{
            progress.Value = 0.0f;
			twitterUrl = setTwitterUrl.Value+setTwitterUrlType;

			PrepareOAuthData();
			RunQuery();
		}

		public override void OnUpdate()
		{
		
			if (wwwObject == null || !string.IsNullOrEmpty(wwwObject.error))
			{
				errorString.Value = "WWW Object is Null!";
				Finish();
				Fsm.Event(isError);
				return;
			}

			progress.Value = wwwObject.progress;
			
			if (progress.Value.Equals(1f))
			{
				errorString.Value = wwwObject.error;

				jsonSearchResult.Value = wwwObject.text;

				Fsm.Event(string.IsNullOrEmpty(errorString.Value) ? isDone : isError);
				
				Finish();
			}


		}

		public void RunQuery()
		{

			var keywords = query.Value;
			
			SortedDictionary<string, string> twitterParamsDictionary = new SortedDictionary<string, string>();

			Regex re = new Regex(@"\(([^=]+)=([^=]+)\)");

			// need to replace by for
			foreach(Match m in re.Matches(keywords))
			{
				twitterParamsDictionary.Add(Uri.EscapeDataString(m.Groups[1].Value), Uri.EscapeDataString((m.Groups[2].Value)));
			}

			if (debugOn.Value == true)	{
				Debug.Log("<color=#4A96ADff><b>[Twitter query URL:]</b></color> "+twitterUrl);
					foreach(var s in twitterParamsDictionary){
					Debug.Log("<color=#A8CD1Bff><b>[Query split check:]</b></color> "+ s.Key+ "=" +s.Value);
					}
			}
			
			wwwObject = CreateTwitterAPIQuery(twitterUrl, twitterParamsDictionary);

		}

		
		private WWW CreateTwitterAPIQuery(string twitterUrl, SortedDictionary<string, string> twitterParamsDictionary)
		{
			string signature = CreateSignature(twitterUrl, twitterParamsDictionary);
			if (debugOn.Value == true)	{
				Debug.Log("<color=#E9E581ff><b>[OAuth Signature:]</b></color> " + signature);
			}
			string authHeaderParam = CreateAuthorizationHeaderParameter(signature, this.oauthTimeStamp.Value);
			if (debugOn.Value == true)	{
				Debug.Log("<color=#E9E581ff><b>[Auth Header:]</b></color> " + authHeaderParam);
			}
			Dictionary<string, string> headers = new Dictionary<string, string>();
			headers["Authorization"] = authHeaderParam;
			
			string twitterParams = ParamDictionaryToString(twitterParamsDictionary);

			WWW query = new WWW(twitterUrl + "?" + twitterParams, null, headers);	

			return query;
		}

		
		private void PrepareOAuthData() {
			oauthNonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture)));
			TimeSpan _timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);		
			oauthTimeStamp.Value = Convert.ToInt64(_timeSpan.TotalSeconds).ToString(CultureInfo.InvariantCulture);
			
			// Override the nounce and timestamp here if troubleshooting with Twitter's OAuth Tool
			//oauthNonce = "69db07d069ac50cd673f52ee08678596";
			//oauthTimeStamp = "1442419142";
		}
		
		// Taken from http://www.i-avington.com/Posts/Post/making-a-twitter-oauth-api-call-using-c
		private string CreateSignature(string url, SortedDictionary<string, string> searchParamsDictionary)
		{
			//string builder will be used to append all the key value pairs
			StringBuilder signatureBaseStringBuilder = new StringBuilder();
			signatureBaseStringBuilder.Append("GET&");
			signatureBaseStringBuilder.Append(Uri.EscapeDataString(url));
			signatureBaseStringBuilder.Append("&");
			
			//the key value pairs have to be sorted by encoded key
			SortedDictionary<string, string> urlParamsDictionary = new SortedDictionary<string, string>
			{
				{"oauth_version", "1.0"},
				{"oauth_consumer_key", this.oauthConsumerKey.Value},
				{"oauth_nonce", this.oauthNonce},
				{"oauth_signature_method", "HMAC-SHA1"},
				{"oauth_timestamp", this.oauthTimeStamp.Value},
				{"oauth_token", this.oauthToken.Value}
			};
			
			foreach (KeyValuePair<string, string> keyValuePair in searchParamsDictionary)
			{
				urlParamsDictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}    
			
			signatureBaseStringBuilder.Append(Uri.EscapeDataString(ParamDictionaryToString(urlParamsDictionary)));
			string signatureBaseString = signatureBaseStringBuilder.ToString();

			if (debugOn.Value == true)	{
				Debug.Log("<color=#E9E581ff><b>[Signature Base String:]</b></color> " + signatureBaseString);
			}
			//generation the signature key the hash will use
			string signatureKey =
				Uri.EscapeDataString(this.oauthConsumerSecret.Value) + "&" +
					Uri.EscapeDataString(this.oauthTokenSecret.Value);
			
			HMACSHA1 hmacsha1 = new HMACSHA1(
				new ASCIIEncoding().GetBytes(signatureKey));
			
			//hash the values
			string signatureString = Convert.ToBase64String(
				hmacsha1.ComputeHash(
				new ASCIIEncoding().GetBytes(signatureBaseString)));
			
			return signatureString;
		}
		
		private string CreateAuthorizationHeaderParameter(string signature, string timeStamp)
		{
			string authorizationHeaderParams = String.Empty;
			authorizationHeaderParams += "OAuth ";
			
			authorizationHeaderParams += "oauth_consumer_key="
				+ "\"" + Uri.EscapeDataString(this.oauthConsumerKey.Value) + "\", ";
			
			authorizationHeaderParams += "oauth_nonce=" + "\"" +
				Uri.EscapeDataString(this.oauthNonce) + "\", ";
			
			authorizationHeaderParams += "oauth_signature=" + "\""
				+ Uri.EscapeDataString(signature) + "\", ";
			
			authorizationHeaderParams += "oauth_signature_method=" + "\"" +
				Uri.EscapeDataString("HMAC-SHA1") +
					"\", ";
			
			authorizationHeaderParams += "oauth_timestamp=" + "\"" +
				Uri.EscapeDataString(timeStamp) + "\", ";        
			
			authorizationHeaderParams += "oauth_token=" + "\"" +
				Uri.EscapeDataString(this.oauthToken.Value) + "\", ";
			
			authorizationHeaderParams += "oauth_version=" + "\"" +
				Uri.EscapeDataString("1.0") + "\"";
			return authorizationHeaderParams;
		}
		
		private string ParamDictionaryToString(IDictionary<string, string> paramsDictionary) {
			StringBuilder dictionaryStringBuilder = new StringBuilder();       
			foreach (KeyValuePair<string, string> keyValuePair in paramsDictionary)
			{
				//append a = between the key and the value and a & after the value
				dictionaryStringBuilder.Append(string.Format("{0}={1}&", keyValuePair.Key, keyValuePair.Value));
			}
			
			// Get rid of the extra & at the end of the string
			string paramString = dictionaryStringBuilder.ToString().Substring(0, dictionaryStringBuilder.Length - 1);
			return paramString;
		}


	}


}

