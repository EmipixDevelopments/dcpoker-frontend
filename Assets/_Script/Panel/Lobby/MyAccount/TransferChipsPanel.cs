using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransferChipsPanel : MonoBehaviour {

	#region PUBLIC_VARIABLES
	[Header ("Inputfield")]
	public TMP_InputField inptReceiverId;
	public TMP_InputField inptAmount;
	public TMP_InputField inptPassword;

	[Header ("Text")]
	public TextMeshProUGUI txtMessage;

	[Header ("Transform")]
	public Transform transformPopup;
	#endregion

	#region PRIVATE_VARIABLES
	#endregion

	#region UNITY_CALLBACKS
	void OnEnable()
	{
		inptReceiverId.text = "";
		inptAmount.text = "";
		inptPassword.text = "";
	}
	#endregion

	#region DELEGATE_CALLBACKS
	#endregion

	#region PUBLIC_METHODS
	public void LowerCapsFunction()
	{
		inptReceiverId.text = inptReceiverId.text.ToLower();
	}

	public void KeyboardOpen(float positionY)
	{
        #if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        transformPopup.localPosition = new Vector3 (96, positionY, 0);
		#endif
	}

	public void KeyboardClose()
	{
        #if UNITY_EDITOR || UNITY_IOS || UNITY_ANDROID
        transformPopup.localPosition = new Vector3 (96, 0, 0);
		#endif
	}

	public void SubmitButtonTap()
	{
		if (Validation ()) 
		{
			UIManager.Instance.DisplayLoader ("");
			UIManager.Instance.SocketGameManager.TransferChips (inptReceiverId.text, double.Parse (inptAmount.text), inptPassword.text, (socket, packet, args) => {
				Debug.Log ("TransferChips response  : " + packet.ToString ());
				UIManager.Instance.HideLoader ();

				JSONArray arr = new JSONArray (packet.ToString ());
				string Source;
				Source = arr.getString (arr.length()-1);
				var resp1 = Source;

				PokerEventResponse resp = JsonUtility.FromJson<PokerEventResponse>(resp1);

				if (resp.status.Equals (Constants.PokerAPI.KeyStatusSuccess)) {
					UIManager.Instance.backgroundEventManager.GetProfileEventCall();
				}
				UIManager.Instance.DisplayMessagePanel (resp.message);
				this.Close();
			});
		}
	}

	public void ClosePanelBtnTap(bool IsOPen)
	{
		if (IsOPen) 
		{
			this.Open ();
		}
		else 
		{
			this.Close ();
		}

	}
	#endregion

	#region PRIVATE_METHODS
	#endregion

	#region COROUTINES
	#endregion

	#region GETTER_SETTER
	private bool Validation ()
	{ 
		string userId = inptReceiverId.text;
		string amount = inptAmount.text;
		string password = inptPassword.text;

		if (userId.Length == 0) {
			txtMessage.text = Constants.Messages.Register.UsernameEmpty;
			return false;
		} else if (amount.Length == 0) {
			txtMessage.text = Constants.Messages.TransferChips.ChipsAmountEmpty;
			return false;
		} else if (double.Parse (amount) <= 0) {
			txtMessage.text = Constants.Messages.TransferChips.ChipsAmountGreaterThan + " 0";
			return false;
		} else if (password.Length < 6) {
			txtMessage.text = Constants.Messages.Register.MinPasswordLength;
			return false;
		} else if (userId == UIManager.Instance.assetOfGame.SavedLoginData.Username) {
			txtMessage.text = "You can not transfer chips to yourself.";
			return false;
		}

		txtMessage.text = "";
		return true;
	}

	#endregion
}
