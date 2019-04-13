using UnityEngine;
using System.Collections;

public class MainMenuFlow : IFlow
{
	public Routine Routine { get; set; }

	public IEnumerator Flow()
	{

		Debug.Log("Started!");

		yield return new WaitForSeconds(0.5f);

		Debug.Log("Finished!");
		
	}
}