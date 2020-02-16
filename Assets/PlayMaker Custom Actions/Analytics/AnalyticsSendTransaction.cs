// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine.Analytics;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Analytics")]
	[Tooltip("Tracking Monetization. Requires a price parameter, a currency and an optional Apple iTunes / Google Play receipt string.")]
	public class AnalyticsSendTransaction : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Product ID")]
		public FsmString productId;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The amount")]
		public FsmFloat amount;


		[UIHint(UIHint.Variable)]
		[Tooltip("Receipt data (iOS) / receipt ID (android) / for in-app purchases to verify purchases with Apple iTunes / Google Play. Use null in the absence of receipts.")]
		public FsmString receiptPurchaseData;

	
		[UIHint(UIHint.Variable)]
		[Tooltip("Android receipt signature. If using native Android use the INAPP_DATA_SIGNATURE string containing the signature of the purchase data that was signed with the private key of the developer. The data signature uses the RSASSA-PKCS1-v1_5 scheme. Pass in null in absence of a signature.")]
		public FsmString signature;


		[UIHint(UIHint.Variable)]
		[Tooltip("The Currency. Abbreviation of the currency used for the transaction. For example “USD” (United States Dollars). See http://en.wikipedia.org/wiki/ISO_4217 for a standardized list of currency abbreviations.")]
		public FsmString currency;
		[ActionSection("Result")]

		[Tooltip("Result")]
		[ObjectType(typeof(AnalyticsResult))]
		[UIHint(UIHint.Variable)]
		public FsmEnum result;
	
		[Tooltip("Event Sent if execution went OK")]
		public FsmEvent success;

		[Tooltip("Event Sent if execution failed. Check result for more infos")]
		public FsmEvent failure;

		public override void Reset()
		{
			productId = null;
			amount = 0.99f;
			currency = "USD";

			receiptPurchaseData = new FsmString(){UseVariable=true};
			signature = new FsmString(){UseVariable=true};

			result = AnalyticsResult.AnalyticsDisabled;
			success = null;
			failure = null;
		}

		public override void OnEnter()
		{
			AnalyticsResult _result;
			if (!receiptPurchaseData.IsNone || !signature.IsNone) {
				_result = Analytics.Transaction(productId.Value,(decimal)amount.Value,currency.Value,receiptPurchaseData.Value,signature.Value);
			} else {
				_result = Analytics.Transaction(productId.Value,(decimal)amount.Value,currency.Value,null,null);
			}

			if (!result.IsNone) {
				result.Value = _result;
			}

			Fsm.Event(_result == AnalyticsResult.Ok?success:failure);

			Finish();		
		}
	}
}