using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Anacerita
{
	public class LoaderTheScene : MonoBehaviour {

		public GameObject loadingScreen;
		public Slider sliderLoading;
		AsyncOperation operation;

		public void LoadScene2(string sceneIndex)
		{
			StartCoroutine (LoadAsynchronously(sceneIndex));
		}

		IEnumerator LoadAsynchronously(string sceneIndex)
		{
			operation = SceneManager.LoadSceneAsync (sceneIndex);
			operation.allowSceneActivation = false;

			loadingScreen.SetActive (true);

			while(!operation.isDone)
			{

				float progres = Mathf.Clamp01(operation.progress / 0.9f);

				sliderLoading.value = progres;
				operation.allowSceneActivation = true;



//				sliderLoading.value = operation.progress;
//				if (operation.progress == 0.9f) {
//					sliderLoading.value = 1f;
//					operation.allowSceneActivation = true;
//				}
				yield return null;
			}
		}
	}
}
