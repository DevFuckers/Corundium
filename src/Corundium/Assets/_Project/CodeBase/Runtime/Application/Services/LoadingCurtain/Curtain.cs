using System.Collections;
using UnityEngine;

public class Curtain : MonoBehaviour, ILoadingCurtain
{
    [SerializeField] private CanvasGroup _curtain;
    [SerializeField, Range(0.005f, 0.07f)] private float _fadeInSpeed;

	private void Awake()
	{
		transform.SetParent(null);
		DontDestroyOnLoad(this);
	}

	public void Show()
	{
	    gameObject.SetActive(true);
	    _curtain.alpha = 1;
	}
	
	public void Hide()
	{
		gameObject.SetActive(true);
		StartCoroutine(DoFadeIn());
	}
	
	private IEnumerator DoFadeIn()
	{
        while (_curtain.alpha > 0)
        {
            _curtain.alpha -= 0.03f;
			yield return new WaitForSeconds(_fadeInSpeed);
        }
        
        gameObject.SetActive(false);
	}
}
