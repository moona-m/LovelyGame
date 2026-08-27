using UnityEngine;
using UnityEngine.EventSystems;


public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] float _hoverScaleIncrease = 1.1f;
    [SerializeField] float _clickScaleIncrease = 1.13f;
    [SerializeField] float _tweenEffectDuration = 0.1f;
    [SerializeField] AudioClip _hoverSound;
    [SerializeField] AudioClip _clickSound;

    private void OnClickSound()
    {
        AudioManager.Instance.PlayAudio(_clickSound, AudioManager.SoundType.SFX, 0.4f, false);
    }

    private void OnHoverSound()
    {
        AudioManager.Instance.PlayAudio(_hoverSound, AudioManager.SoundType.SFX, 0.4f, false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector2.one * _clickScaleIncrease;
        LeanTween.scale(gameObject, Vector2.one, _tweenEffectDuration).setIgnoreTimeScale(true);

        OnClickSound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector2.one * _hoverScaleIncrease, _tweenEffectDuration).setIgnoreTimeScale(true);

        OnHoverSound();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector2.one, _tweenEffectDuration).setIgnoreTimeScale(true);
    }

}
