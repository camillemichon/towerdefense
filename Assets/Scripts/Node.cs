using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

	public Color hoverColor;
	public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

	[Header("Optional")]
	public GameObject turret;

	private Renderer rend;
	private Color startColor;

	BuildManager buildManager;

	private void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;

		buildManager = BuildManager.instance;
    }

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}

	private void OnMouseDown()
	{
		// no defense selected
		if (EventSystem.current.IsPointerOverGameObject() || !buildManager.CanBuild)
		{
			return;
		}

		// defense already exists
		if (turret != null)
		{
			Debug.Log("Impossible to build the defense there! ");
			return;
		}

		buildManager.BuildTurretOn(this);
	}

	// hover the tile
	private void OnMouseEnter() 
	{
		if (EventSystem.current.IsPointerOverGameObject() || !buildManager.CanBuild)
		{
			return;
		}

		rend.material.color = buildManager.HasMoney ? hoverColor : notEnoughMoneyColor;

	}

	// exit the tile
	private void OnMouseExit()
	{
		rend.material.color = startColor;
    }
}
