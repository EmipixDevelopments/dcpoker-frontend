using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerCard : MonoBehaviour
{
    public Image card;

    public string currentCard = "8C";

    private bool isOpened = false;
    private Vector3 initialEulerAngle;

    void OnEnable()
    {
        initialEulerAngle = transform.localEulerAngles;
    }

    void OnDisable()
    {
        transform.localEulerAngles = initialEulerAngle;
        card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, 0f, true);
    }

    //	void Update ()
    //	{
    //		if (Input.GetKeyDown (KeyCode.R))
    //			ResetImage ();
    //		else if (Input.GetKeyDown (KeyCode.G)) {
    //			PlayAnimation (currentCard);
    //		}
    //	}

    public void DisplayCardWithoutAnimation(string currentCard)
    {
        StopCoroutine("FlipAmination");
        this.transform.eulerAngles = Vector3.zero;
        this.currentCard = currentCard;
        ChangeImage();
    }

    public void PlayAnimation(string currentCard)
    {
        StopCoroutine("FlipAmination");
        this.transform.eulerAngles = Vector3.zero;


        if (isOpened)
        {
            DisplayCardWithoutAnimation(currentCard);
        }
        else
        {
            this.currentCard = currentCard;
            if (gameObject.activeSelf)
            {
                StartCoroutine("FlipAmination");
            }
        }
    }

    public void PlayAnimation(string currentCardValue, float time)
    {
        if (isOpened)
        {
            DisplayCardWithoutAnimation(currentCardValue);
        }
        else
        {
            this.currentCard = currentCardValue;
            if (gameObject.activeSelf)
            {
                Invoke("InvokeAnimation", time);
            }
        }
    }

    private void InvokeAnimation()
    {
        StopCoroutine("FlipAmination");
        StartCoroutine("FlipAmination");
    }

    /// <summary>
    /// Resets the image
    /// </summary>
    public void ResetImage()
    {
        isOpened = false;
        card.sprite = UIManager.Instance.assetOfGame.PokerCards.BackCard;
        this.SetAlpha(2);
        card.CrossFadeAlpha(Constants.Poker.MatchedCardAlpha, 0f, true);
    }

    public void SetBackCard()
    {
        card.sprite = UIManager.Instance.assetOfGame.PokerCards.BackCard;
    }

    public void SetAlpha(float alphaValue, float duration = 0)
    {
        if (card.color.a != alphaValue)
            card.CrossFadeAlpha(alphaValue, duration, true);
    }
    private void ChangeImage()
    {
        isOpened = true;
        card.sprite = Utility.Instance.GetCard(currentCard);
        //		Debug.Log ("CurrentCard Without Anim ==> ChangeImage" + currentCard);

    }

    private IEnumerator FlipAmination()
    {
        //		float steps = 10;
        //		for (int j = 0; j < 90 / steps; j++) {
        //			transform.Rotate (Vector3.up * steps);
        //			yield return null;
        //		}
        float flipSpeed = 0.3f;
        //Utility.Instance.RotateObject(transform, new Vector3(0, 0, 0), new Vector3(0, 90, 0), flipSpeed);
        transform.eulerAngles = new Vector3(initialEulerAngle.x, 0, initialEulerAngle.z);
        yield return new WaitForSecondsRealtime(flipSpeed);
        ChangeImage();
        Utility.Instance.RotateObject(transform, new Vector3(0, 90, 0), new Vector3(0, 0, 0), flipSpeed);
        //		for (int j = 0; j < 90 / steps; j++) {
        //			transform.Rotate (-Vector3.up * steps);
        //			yield return null;
        //		}
        yield return new WaitForSecondsRealtime(flipSpeed);
        card.Open();
        this.transform.eulerAngles = Vector3.zero;
    }
}