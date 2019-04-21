using System;
using TankGame.Views;
using UnityEngine;

namespace TankGame.Game
{
	public class DragAndDropArrowController
	{
		private readonly ViewController viewController;

		private DragAndDropArrow dragAndDropArrow;
		private Vector2 startPosition;

		public DragAndDropArrowController(ViewController viewController)
		{
			this.viewController = viewController;
		}

		public void StartedDrag(Vector3 position)
		{
			startPosition = position;

			if (dragAndDropArrow == null)
			{
				dragAndDropArrow = viewController.ShowViewComponent<DragAndDropArrow>();
			}

			dragAndDropArrow.gameObject.SetActive(true);
			dragAndDropArrow.UpdateArrow(position, position);
		}

		public void EndedDrag()
		{
			dragAndDropArrow.gameObject.SetActive(false);
		}

		public void UpdateDrag(Vector3 position)
		{
			dragAndDropArrow.UpdateArrow(startPosition, position);
		}
	}
}