using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;
//using Syst![alt text][1]em.Collections.Generic;


public class flipfont : MonoBehaviour
{

    Text myText; //You can also make this public and attach your UI text here.

    string individualLine = ""; //Control individual line in the multi-line text component.

    int numberOfAlphabetsInSingleLine = 20;

    string sampleString = "";

    void Awake()
    {
        myText = GetComponent<Text>();
    }


    public void setRTL(string sampleString)
    {
        List<string> listofWords = sampleString.Split(' ').ToList(); //Extract words from the sentence

        foreach (string s in listofWords)
        {

            if (individualLine.Length >= numberOfAlphabetsInSingleLine)
            {
                myText.text += Reverse(individualLine) + "\n"; //Add a new line feed at the end, since we cannot accomodate more characters here.
                individualLine = ""; //Reset this string for new line.
            }

            individualLine += s + " ";

        }
    }
    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

}