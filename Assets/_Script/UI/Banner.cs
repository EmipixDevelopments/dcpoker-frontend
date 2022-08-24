using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banner : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _backGround;
    [SerializeField] private Button _button;

    private Action onButtonClick;

    private void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => onButtonClick?.Invoke()) ;
    }


    private void OnEnable()
    {
        // спросить у Олега, можно ли отправлять вместе с картинкой еще текстовое сообщение. На случай, если картинка не загркзится.

        /// получить данные от сервера
        /// добавить событие
        /// загрузить изображение
        /// отобразить изображение
        /// 
        if (!UIManager.Instance)
        {
            return;
        }
        UpdateBanner();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            UpdateBanner();
        }
    }

    private static void UpdateBanner()
    {
        Debug.Log($"--- Call banner + {PokerAPI.BaseUrl}") ;
        UIManager.Instance.SocketGameManager.Banners((socket, packet, args) =>
        {
            print("Banners: " + packet.ToString());
            JSONArray arr = new JSONArray(packet.ToString());
            var resp = arr.getString(arr.length() - 1);
        });
    }
}
