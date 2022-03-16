using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class APIAndDetail : MonoBehaviour
{
	string databasePath = "http://thuglife.blogfa.com/page/";
	public string buttonPath = "page_0";
	string[] allButtonsInformation;
	public GameObject[] startupLogos;
	public GameObject appButton;
	Transform canvas;

	void Start()
	{
		canvas = GameObject.Find("Canvas").transform;
		DataChanger();
	}

	public void DataChanger()
	{
		StartCoroutine(GetValue(databasePath + buttonPath));
	}

	public IEnumerator GetValue(string url)
	{
		using (UnityWebRequest buttonsData = UnityWebRequest.Get(url))
		{
			//Request and wait for the desired page.
			yield return buttonsData.SendWebRequest();

			if (buttonsData.isNetworkError)
			{
				Debug.Log("Error: " + buttonsData.error);
			}
			else
			{
				Startedup();
				string c = buttonsData.downloadHandler.text.Substring(2672);
				string[] d = c.Split('?');
				allButtonsInformation = d[0].Split(',');
				DeleteAllExistButtons();
				SpawnNewButtons();
			}
		}
	}

	void Startedup()
	{
		foreach (GameObject m in startupLogos)
		{
			m.SetActive(false);
		}
	}

	void DeleteAllExistButtons()
	{
		GameObject[] allExistButton = GameObject.FindGameObjectsWithTag("Button");
		foreach (GameObject g in allExistButton)
		{
			Destroy(g);
		}
	}

	void SpawnNewButtons()
	{
		for (int i = 0; i < allButtonsInformation.Length / 3; i++)
		{
			var spawnedButton = Instantiate(appButton, new Vector3(0, 1450 - (i * 450), 0), Quaternion.identity);
			spawnedButton.transform.SetParent(canvas.transform, false);
			spawnedButton.GetComponent<ButtonInfo>().buttonName = allButtonsInformation[i * 3];
			spawnedButton.GetComponent<ButtonInfo>().buttonDescription = allButtonsInformation[(i * 3) + 1];
			spawnedButton.GetComponent<ButtonInfo>().buttonLink = allButtonsInformation[(i * 3) + 2];
		}
	}
}
