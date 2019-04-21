using UnityEngine;
using UnityEngine.UI;

namespace TankGame.Game
{
	public class DragAndDropArrow : MonoBehaviour
	{
		[SerializeField] private RectTransform pivot;
		[SerializeField] private Image arrowBase;
		[SerializeField] private Image arrowHead;

		public void UpdateArrow(Vector2 startPos, Vector2 endPos)
		{
			float length = Vector2.Distance(startPos, endPos) * 100f;

			pivot.right = endPos - startPos;
			pivot.position = startPos;

			var baseSize = arrowBase.rectTransform.sizeDelta;
			baseSize.x = length;
			arrowBase.rectTransform.sizeDelta = baseSize;

			arrowHead.rectTransform.anchoredPosition = new Vector2(length, 0);
		}
	}
}
