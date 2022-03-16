using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
	public Text nameText;
	public Text descriptionText;
	public string buttonName;
	public string buttonDescription;
	public string buttonLink;

	void Start()
    {
		gameObject.name = nameText.text = buttonName;
		descriptionText.text = buttonDescription;
    }

	public void OnButtonDownWorks()
	{
		APIAndDetail API = GameObject.Find("AppController").GetComponent<APIAndDetail>();
		API.buttonPath = "Page_" + buttonName;
		API.DataChanger();
	}
}
