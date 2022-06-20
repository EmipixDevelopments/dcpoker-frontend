using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class RegisterPanel : MonoBehaviour
{

    #region PUBLIC_VARIABLES
    public TMP_InputField inputFieldName;
    public TMP_InputField inputFieldUsername;
    public TMP_InputField inputFieldPassword;
    public TMP_InputField inputFieldPhoneNumber;
    public TMP_InputField inputFieldConfirmPassword;
    public TMP_InputField inputfieldRefferalCode;
    //	public InputField inputFieldName;
    //	public InputField inputFieldUsername;
    //	public InputField inputFieldPassword;
    //	public InputField inputFieldEmail;
    //	public InputField inputFieldConfirmPassword;
    public Text TxtError;
    #endregion

    #region PRIVATE_VARIABLES

    #endregion

    #region UNITY_CALLBACKS
    // Use this for initialization
    // Update is called once per frame
    void OnEnable()
    {
        /*
#if UNITY_WEBGL || UNITY_EDITOR || UNITY_STANDALONE
        gameObject.transform.localScale = new Vector2(.8f, .8f);
#elif UNITY_IOS
		if(SystemInfo.deviceModel.Contains("iPad")) {
			gameObject.transform.localScale = new Vector2(.8f,.8f);
		}
#else
        gameObject.transform.localScale = new Vector2(1f, 1f);
#endif
        */
        ResetAllInputFields();
    }

    #endregion

    #region DELEGATE_CALLBACKS

    #endregion

    #region PUBLIC_METHODS
    public void closeButtonTap()
    {
        UIManager.Instance.SoundManager.OnButtonClick();
        this.Close();
    }
    public void LowerCapsFunction()
    {
        inputFieldUsername.text = inputFieldUsername.text.ToLower();
    }

    public void OnRegisterButtonTap()
    {
        string Name = inputFieldName.text;
        string username = inputFieldUsername.text.ToLower(); ;
        string password = inputFieldPassword.text;
        string phNo = inputFieldPhoneNumber.text;
        string confirmPassword = inputFieldConfirmPassword.text;
        string rCode = inputfieldRefferalCode.text;
        if (UIManager.Instance.SocketGameManager.HasInternetConnection())
        {
            UIManager.Instance.SoundManager.OnButtonClick();
            if (IsLoginDetailValid())
            {
                removeAllMessage();
                UIManager.Instance.DisplayLoader("");

                UIManager.Instance.SocketGameManager.RegisterPlayer(username, password, phNo, rCode, (socket, packet, args) =>
                {
                    Debug.Log("RegisterPlayer  => " + packet.ToString());

                    UIManager.Instance.HideLoader();
                    JSONArray arr = new JSONArray(packet.ToString());
                    string Source;
                    Source = arr.getString(arr.length() - 1);
                    var resp = Source;
                    PokerEventResponse registrationResp = JsonUtility.FromJson<PokerEventResponse>(resp);

                    if (registrationResp.status.Equals(Constants.PokerAPI.KeyStatusSuccess))
                    {
                        UIManager.Instance.DisplayMessagePanel(registrationResp.message, () =>
                          {
                              this.Close();
                              UIManager.Instance.HidePopup();
                          });

                    }
                    else
                    {
                        UIManager.Instance.DisplayMessagePanel(registrationResp.message, null);
                    }
                });
            }
        }
    }

    private bool IsLoginDetailValid()
    {
        StopCoroutine(textempti());
        removeAllMessage();

        string Name = inputFieldName.text;
        string username = inputFieldUsername.text;
        string Mobile = inputFieldPhoneNumber.text;
        string password = inputFieldPassword.text;
        string confirmPassword = inputFieldConfirmPassword.text;
        string accNo = inputfieldRefferalCode.text;

        /*if (string.IsNullOrEmpty(Name))
		{
			TxtError.text = Constants.Messages.Register.FirstNameEmpty;
			StartCoroutine(textempti());
			return false;
		}*/
        if (string.IsNullOrEmpty(username))
        {
            TxtError.text = Constants.Messages.Register.UsernameEmpty;
            StartCoroutine(textempti());
            return false;
        }

        else if (!IsUsernameValid())
        {
            TxtError.text = Constants.Messages.Register.UsernameInvalid;
            StartCoroutine(textempti());
            return false;
        }
        //else if (!IsEmailValid())
        //{
        //	TxtError.text = Constants.Messages.Register.EmailInvalid;
        //	StartCoroutine(textempti());
        //	return false;
        //}

        /* else if (string.IsNullOrEmpty(Mobile))
         {
             TxtError.text = Constants.Messages.Register.MobileEmpty;
             StartCoroutine(textempti());
             return false;
         }
         else if (!IsMobileValid())
         {
             TxtError.text = Constants.Messages.Register.MobileInvalid;
             StartCoroutine(textempti());
             return false;
         }*/

        /*if (string.IsNullOrEmpty(accNo))
        {
            TxtError.text = Constants.Messages.Register.AccountNumberEmpty;
            StartCoroutine(textempti());
            return false;
        }*/
        else if (string.IsNullOrEmpty(password))
        {
            TxtError.text = Constants.Messages.Register.PasswordEmpty;
            StartCoroutine(textempti());
            return false;
        }
        else if (password.Length < Constants.Messages.Login.PasswordLength)
        {
            TxtError.text = Constants.Messages.Register.MinPasswordLength;
            StartCoroutine(textempti());
            return false;
        }
        else if (string.IsNullOrEmpty(confirmPassword))
        {
            TxtError.text = Constants.Messages.Register.ConfirmPasswordEmpty;
            StartCoroutine(textempti());
            return false;
        }
        else if (!password.Equals(confirmPassword))
        {
            TxtError.text = Constants.Messages.Register.PasswordNotMatched;
            StartCoroutine(textempti());
            return false;
        }
        return true;
    }
    #endregion

    #region PRIVATE_METHODS
    private void ResetAllInputFields()
    {
        inputFieldName.text = "";
        inputFieldUsername.text = "";
        inputFieldPhoneNumber.text = "";
        inputFieldPassword.text = "";
        inputFieldConfirmPassword.text = "";
        inputfieldRefferalCode.text = "";
        removeAllMessage();
        JSON_Object jsonObj = new JSON_Object(UIManager.Instance.StoreDetails);
        //print("registrationResp => " + jsonObj.getString("country_calling_code"));
        string code = jsonObj.getString("country_calling_code").ToString();


        inputFieldPhoneNumber.placeholder.GetComponent<TextMeshProUGUI>().text = code + "********  (Optional)";
    }
    void removeAllMessage()
    {
        for (int i = 0; i < TxtError.text.Length; i++)
        {
            TxtError.text = "";
        }
    }

    //	void ApiCall()
    //	{
    //		apimain ("ddf", 0);
    //	}
    //
    //	void apimain(string s,int id)
    //	{
    //		id.Equals(0)
    //		
    //	}
    #endregion

    #region COROUTINES
    IEnumerator textempti()
    {
        yield return new WaitForSeconds(4.3f);
        TxtError.text = "";
    }
    #endregion

    #region GETTER_SETTER
    private bool IsEmailValid()
    {
        string email = inputFieldPhoneNumber.text;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);

        if (!match.Success)
        {
            return false;
        }

        return true;
    }
    private bool IsMobileValid()
    {
        string number = inputFieldPhoneNumber.text;
        Regex regex = new Regex(@"^(\+[0-9]{9})$");
        Match match = regex.Match(number);

        if (!match.Success)
        {
            if (number.Length < 10)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsUsernameValid()
    {
        string number = inputFieldUsername.text;

        if (number.Length < 4)
        {
            return false;
        }
        return true;
    }
    #endregion
}
/*#region PUBLIC_VARIABLES

	#endregion

	#region PRIVATE_VARIABLES

	#endregion

	#region UNITY_CALLBACKS
	// Use this for initialization
	// Update is called once per frame
	#endregion

	#region DELEGATE_CALLBACKS
    #endregion

	#region PUBLIC_METHODS
	#endregion

	#region PRIVATE_METHODS
	#endregion

	#region COROUTINES
	#endregion

	#region GETTER_SETTER
	#endregion
*/