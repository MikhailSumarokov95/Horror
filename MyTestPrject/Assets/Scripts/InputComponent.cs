using UnityEngine;
using System.Collections;
using TMPro;

public class InputComponent : MonoBehaviour
{
	[SerializeField] private TMP_Text _buttonText;
	[SerializeField] private string _defaultKeyName;
	[SerializeField] private KeyCode _defaultKeyCode;

	public KeyCode keyCode { get; set; }

	private IEnumerator coroutine;
	private string tmpKey;

	public TMP_Text buttonText
	{
		get { return _buttonText; }
	}

	public string defaultKeyName
	{
		get { return _defaultKeyName; }
	}

	public KeyCode defaultKeyCode
	{
		get { return _defaultKeyCode; }
	}

	public void ButtonSetKey()
	{
		tmpKey = _buttonText.text;
		_buttonText.text = "???";
		coroutine = Wait();
		StartCoroutine(coroutine);
	}

	private IEnumerator Wait()
	{
		while (true)
		{
			yield return null;

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				_buttonText.text = tmpKey;
				yield break;
				//StopCoroutine(coroutine);
			}

			foreach (KeyCode k in KeyCode.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKeyDown(k) && !Input.GetKeyDown(KeyCode.Escape))
				{
					keyCode = k;
					_buttonText.text = k.ToString();
					yield break;
					//StopCoroutine(coroutine);
				}
			}
		}
	}
}