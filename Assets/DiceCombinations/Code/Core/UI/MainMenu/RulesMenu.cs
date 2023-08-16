using DG.Tweening;
using DiceCombinations.Code.Services.EntityContainer;
using UnityEngine;

namespace DiceCombinations.Code.Core.UI.MainMenu
{
    public class RulesMenu : IFactoryEntity
    {
        private readonly RulesView _view;
        private float[] _rulesDefaultPositionsX;
        private Sequence _textSequence;

        public RulesMenu(RulesView view)
        {
            _view = view;
            CacheRulesTextsPos();
        }

        public void ShowView()
        {
            _view.Show();
            AnimateRulesMenu();
        }

        public void HideView()
        {
            _textSequence?.Kill();
            _view.Hide();
            for (int i = 0; i < _view.RulesRects.Length; i++)
            {
                Vector2 pos = _view.RulesRects[i].anchoredPosition;
                _view.RulesRects[i].anchoredPosition = i % 2 == 0
                    ? new Vector2(_rulesDefaultPositionsX[i] + Screen.width * 2, pos.y)
                    : new Vector2(_rulesDefaultPositionsX[i] - Screen.width * 2, pos.y);
            }
        }

        private void AnimateRulesMenu()
        {
            _textSequence = DOTween.Sequence();
            for (int i = 0; i < _view.RulesRects.Length; i++)
                _textSequence.Append(MoveTextX(_view.RulesRects[i], _rulesDefaultPositionsX[i], 1f));
            _textSequence.OnComplete(() => _view.ShowAnimation());
        }

        private void CacheRulesTextsPos()
        {
            _rulesDefaultPositionsX = new float[_view.RulesRects.Length];
            for (var i = 0; i < _view.RulesRects.Length; i++)
                _rulesDefaultPositionsX[i] = _view.RulesRects[i].anchoredPosition.x;
        }

        private static Tween MoveTextX(RectTransform rect, float xEndPos, float animationTime) => 
            rect.DOLocalMoveX(xEndPos, animationTime);
    }
}