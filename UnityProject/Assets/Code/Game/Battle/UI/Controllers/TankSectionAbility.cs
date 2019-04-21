using System;
using UnityEngine;
using UnityEngine.UI;

namespace TankGame.Game
{
	public class TankSectionAbility : MonoBehaviour
	{
		public event Action OnPressed = delegate { };
		public event Action OnHover = delegate { };

		[SerializeField] private Text powerText;
		[SerializeField] private Text fatigueText;

		public TankAbility TankAbility { get; private set; }

		public Vector2 CenterPosition {
			get {
				return transform.position; //TODO Have center position in abstract/interface?
			}
		}

		public void OnPressedTriggered()
		{
			OnPressed();
		}

		public void OnHoverTriggered()
		{
			OnHover();
		}

		public void Initialize(TankAbility tankAbility, Action<TankSectionAbility> onPressedAbility, Action<TankSectionAbility> onHoverAbility)
		{
			TankAbility = tankAbility;

			powerText.text = tankAbility.power.GetDescription();
			fatigueText.text = string.Format("+{0} fatigue", tankAbility.fatigueDamage.ToString());

			OnPressed += () => onPressedAbility(this);
			OnHover += () => onHoverAbility(this);
		}
	}
}
