using UnityEngine;
using UnityEngine.UI;
public class LivesUI : MonoBehaviour {

	public Text livesText;
	
	private void Update() 
	{
		if (PlayerStats.lives == 0 || PlayerStats.lives == 1)
		{
			livesText.text = PlayerStats.lives + " LIFE";
		}
		else
		{
			livesText.text = PlayerStats.lives + " LIVES";
		}
	}
}
