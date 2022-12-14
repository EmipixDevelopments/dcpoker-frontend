using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(TMP_Text))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class LinkOpener : MonoBehaviour, IPointerClickHandler 
{
    private TextMeshProUGUI textMessage;
 /*  public void OnPointerClick(PointerEventData eventData) {
        TMP_Text pTextMeshPro = GetComponent<TMP_Text>();
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, eventData.position, null);  // If you are not in a Canvas using Screen Overlay, put your camera instead of null
        if (linkIndex != -1) { // was a link clicked?
            TMP_LinkInfo linkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }*/

  public void OnPointerClick(PointerEventData eventData) {
      //  TMP_Text pTextMeshPro = GetComponent<TMP_Text>();

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMessage, Input.mousePosition, null);
        if( linkIndex != -1 ) { // was a link clicked?
            TMP_LinkInfo linkInfo = textMessage.textInfo.linkInfo[linkIndex];

            // open the link id as a url, which is the metadata we added in the text field
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}
