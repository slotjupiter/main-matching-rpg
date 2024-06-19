using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace slotJupiter
{
    public class BoardLayout : LayoutGroup
    {
        public GameObject baseCardSprite;
        public int rows;
        public int columns;
        public Vector2 spacing;
        public int preferredTopPadding;
        private Vector2 cardSize;

        public override void CalculateLayoutInputVertical()
        {
            if (!baseCardSprite) return;
            if (rows == 0) rows = 3;
            if (columns == 0) columns = 8;

            float _parentWidth = rectTransform.rect.width;
            float _parentHeight = rectTransform.rect.height;

            // float _cardHeight = (baseCardSprite.rect.height + _parentHeight - 2 * preferredTopPadding - spacing.y * (rows - 1)) / rows;
            float _cardHeight = baseCardSprite.GetComponent<RectTransform>().rect.height;
            float _cardWidth = baseCardSprite.GetComponent<RectTransform>().rect.width;

            cardSize = new Vector2(_cardWidth, _cardHeight);

            padding.left = Mathf.FloorToInt((_parentWidth - columns * _cardWidth - spacing.x * (columns - 1)) / 2);
            padding.top = Mathf.FloorToInt((_parentHeight - rows * _cardHeight - spacing.y * (rows - 1) + preferredTopPadding) / 2);
            padding.bottom = padding.top;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                int _rowCount = i / columns;
                int _columnCount = i % columns;

                var item = rectChildren[i];

                var xPos = padding.left + cardSize.x * _columnCount + spacing.x * _columnCount;
                var yPos = padding.top + cardSize.y * _rowCount + spacing.y * _rowCount;

                SetChildAlongAxis(item, 0, xPos, cardSize.x);
                SetChildAlongAxis(item, 1, yPos, cardSize.y);
            }
        }

        public override void SetLayoutHorizontal()
        {
            return;
        }

        public override void SetLayoutVertical()
        {
            return;

        }

    }
}
