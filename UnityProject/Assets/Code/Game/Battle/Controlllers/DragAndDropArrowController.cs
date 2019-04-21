using System;
using System.Collections;
using TankGame.Views;
using UnityEngine;

namespace TankGame.Game
{
	public class DragAndDropArrowController
	{
		private readonly ViewController viewController;

		private DragAndDropArrow dragAndDropArrow;
		private Vector2 startPosition;

		private Routine dragAndDropRoutine = new Routine();

		public DragAndDropArrowController(ViewController viewController)
		{
			this.viewController = viewController;
		}

		public void StartDrag(Vector3 position)
		{
			startPosition = position;

			if (dragAndDropArrow == null)
			{
				dragAndDropArrow = viewController.ShowViewComponent<DragAndDropArrow>();
			}

			dragAndDropArrow.gameObject.SetActive(true);
			dragAndDropArrow.UpdateArrow(position, position);

			dragAndDropRoutine.Start(DragAndDropAbilty());
		}

		public void EndDrag()
		{
			dragAndDropArrow.gameObject.SetActive(false);

			dragAndDropRoutine.Stop();
		}

		private IEnumerator DragAndDropAbilty()
		{
			while (Input.GetMouseButton(0))
			{
				var viewCamera = viewController.GetViewCamera();
				var mouseWorldPosition = viewCamera.ScreenToWorldPoint(Input.mousePosition);
				UpdateDrag(mouseWorldPosition);

				yield return null;
			}

			EndDrag();
		}

		public void UpdateDrag(Vector3 position)
		{
			dragAndDropArrow.UpdateArrow(startPosition, position);
		}
	}
}