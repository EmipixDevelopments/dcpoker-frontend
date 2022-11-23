using System;
using System.Collections;
using Constants;
using UnityEngine;
using UnityEngine.Networking;

public class UrlSprite
{
    private UrlImage _myUrlImage;
    private MonoBehaviour _monoBehaviour;
    
    public UrlSprite(MonoBehaviour monoBehaviour = null)
    {
        _myUrlImage = new UrlImage();

        if (monoBehaviour == null)
        {
            _monoBehaviour = new GameObject("url_sprite_mono_behaviour").AddComponent<UrlSpriteMonoBehaviour>();
            return;
        }

        _monoBehaviour = monoBehaviour;
    }

    public Sprite GetCurrentSprite() => _myUrlImage.avatarSprite;

    public void GetUrlSprite(string url, Action<Sprite> onGetAvatar)
    {
        if (_myUrlImage.avatarUrl == url)
        {
            onGetAvatar.Invoke(_myUrlImage.avatarSprite);
            return;
        }
        
        _monoBehaviour.StartCoroutine(UploadUrlSprite(url, onGetAvatar));
    }
    
    private IEnumerator UploadUrlSprite(string url, Action<Sprite> onGetAvatar)
    {
        using (var request = UnityWebRequestTexture.GetTexture(PokerAPI.BaseUrl + url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
                yield break;
            }

            var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 
                new Vector2(texture.width / 2, texture.height / 2));

            _myUrlImage.avatarUrl = url;
            _myUrlImage.avatarSprite = sprite;
            
            onGetAvatar(sprite);
            request.Abort();
        }
    }
}

public class UrlSpriteMonoBehaviour : MonoBehaviour {}

public class UrlImage
{
    public Sprite avatarSprite;
    public string avatarUrl;
}
