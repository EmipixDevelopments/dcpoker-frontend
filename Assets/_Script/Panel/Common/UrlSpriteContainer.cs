using System;
using System.Collections;
using System.Linq;
using Constants;
using UnityEngine;
using UnityEngine.Networking;

public class UrlSpriteContainer
{
    private UrlImage[] _urlImages;

    private int _currentIndex;
    private int _maxUrlImage;
    
    private MonoBehaviour _monoBehaviour;
    
    public UrlSpriteContainer(int maxUrlImage)
    {
        _maxUrlImage = maxUrlImage;
        _urlImages= new UrlImage[_maxUrlImage];

        _monoBehaviour = new GameObject("url_sprite_container_mono_behaviour").AddComponent<UrlSpriteMonoBehaviour>();
    }

    public void GetUrlSprite(string url, Action<Sprite> onSetAvatar)
    {
        var avatarImage = _urlImages.FirstOrDefault(avatar => avatar != null && avatar.avatarUrl == url);
        if (avatarImage != null)
        {
            onSetAvatar?.Invoke(avatarImage.avatarSprite);
            return;
        }

        _monoBehaviour.StartCoroutine(UploadAvatar(url, onSetAvatar));
    }

    private IEnumerator UploadAvatar(string url, Action<Sprite> onAvatarSet)
    {
        using (var request = UnityWebRequestTexture.GetTexture(PokerAPI.BaseUrl + url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
                yield break;
            }

            var index = _currentIndex;
            
            _currentIndex++;
            if (_currentIndex > _maxUrlImage)
            {
                _currentIndex = 0;
            }
            
            var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 
                new Vector2(texture.width / 2, texture.height / 2));

            if (_urlImages[index] == null)
                _urlImages[index] = new UrlImage();
            
            _urlImages[index].avatarUrl = url;
            _urlImages[index].avatarSprite = sprite;
            
            onAvatarSet(sprite);
            request.Abort();
        }
    }
}
