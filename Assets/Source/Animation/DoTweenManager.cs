using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Animation
{
    public class DoTweenManager : MonoBehaviour
    {
        public static DoTweenManager Instance;
        
        private Sequence _animationSequence;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        public async UniTask PlayFadeOutAnimation(Graphic graphic, Ease ease = Ease.Linear)
        {
            _animationSequence.Kill();
            _animationSequence = DOTween.Sequence();
            _animationSequence.Append(graphic.DOFade(0, 0.5f)).SetEase(ease);
            _animationSequence.Play();
            await _animationSequence.AsyncWaitForCompletion();
        }

        public async UniTask PlayFadeOutAnimation(List<Graphic> graphics, Ease ease = Ease.Linear)
        {
            _animationSequence.Kill();
            _animationSequence = DOTween.Sequence();
            foreach (var graphic in graphics)
            {
                var tween = graphic.DOFade(0, 0.5f).SetEase(ease);
                _animationSequence.Join(tween);
            }
            _animationSequence.Play();
            await _animationSequence.AsyncWaitForCompletion();
        }
        
        public async UniTask PlayFadeInAnimation(List<Graphic> graphics, Ease ease = Ease.Linear)
        {
            _animationSequence.Kill();
            _animationSequence = DOTween.Sequence();
            foreach (var graphic in graphics)
            {
                _animationSequence.Join(graphic.DOFade(1, 0.5f)).SetEase(ease);
            }

            _animationSequence.Play();
            await _animationSequence.AsyncWaitForCompletion();
        }

        public async UniTask PlayFadeInAnimation(Graphic graphic, Ease ease = Ease.Linear)
        {
            _animationSequence.Kill();
            _animationSequence = DOTween.Sequence();
            _animationSequence.Append(graphic.DOFade(1, 0.5f)).SetEase(ease);

            _animationSequence.Play();
            await _animationSequence.AsyncWaitForCompletion();
        }

        public async UniTask PlayMoveToPointWithResizeAnimation(RectTransform startTransform, RectTransform endTransform, Ease ease = Ease.Linear)
        {
            _animationSequence.Kill();
            _animationSequence = DOTween.Sequence();
            _animationSequence.Join(startTransform.DOMove(endTransform.position, 3f)).SetEase(ease);
            _animationSequence.Join(startTransform.DOSizeDelta(endTransform.sizeDelta, 3f)).SetEase(ease);
            _animationSequence.Play();
            await _animationSequence.AsyncWaitForCompletion();
        }
        
        public async UniTask PlayMoveToPointAnimation(RectTransform startTransform, RectTransform endTransform, Ease ease = Ease.Linear)
        {
            _animationSequence.Kill();
            _animationSequence = DOTween.Sequence();
            _animationSequence.Join(startTransform.DOAnchorPos(endTransform.anchoredPosition, 1f).SetEase(Ease.OutCirc));
            _animationSequence.Play();
            await _animationSequence.AsyncWaitForCompletion();
        }
        
        private void OnDestroy()
        {
            _animationSequence?.Kill();
        }
    }
}
