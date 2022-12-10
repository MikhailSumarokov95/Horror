using UnityEngine;
using System.Collections;
using System.IO;

public class GameInput : MonoBehaviour
{

	[SerializeField] private string fileName = "input.settings";
	[SerializeField] private InputComponent[] _input;

	private static GameInput gameInput;

	public static GameInput Key
	{
		get { return gameInput; }
	}

	public void Awake()
	{
		gameInput = this;
		LoadSettings();
	}

	private void OnDisable()
	{
		SaveSettings();
	}

	private KeyCode FindKey(string name)
	{
		for (int i = 0; i < _input.Length; i++)
		{
			if (name == _input[i].defaultKeyName) return _input[i].keyCode;
		}

		return KeyCode.None;
	}

	private int GetInt(string text)
	{
		int value;
		if (int.TryParse(text, out value)) return value;
		return 0;
	}

	private string Path()
	{
		return Application.dataPath + "/" + fileName;
	}

	private void SetKey(string value)
	{
		string[] result = value.Split(new char[] { '=' });

		for (int i = 0; i < _input.Length; i++)
		{
			if (result[0] == _input[i].defaultKeyName)
			{
				_input[i].keyCode = (KeyCode)GetInt(result[1]);
				_input[i].buttonText.text = _input[i].keyCode.ToString();
			}
		}
	}

	public void DefaultSettings() // возврат настроек по умолчанию
	{
		for (int i = 0; i < _input.Length; i++)
		{
			_input[i].keyCode = _input[i].defaultKeyCode;
			_input[i].buttonText.text = _input[i].defaultKeyCode.ToString();
		}
	}

	public void LoadSettings() // загрузка установок
	{
		if (!File.Exists(Path()))
		{
			DefaultSettings();
			return;
		}

		StreamReader reader = new StreamReader(Path());

		while (!reader.EndOfStream)
		{
			SetKey(reader.ReadLine());
		}

		reader.Close();
	}

	public void SaveSettings()
	{
		StreamWriter writer = new StreamWriter(Path());

		for (int i = 0; i < _input.Length; i++)
		{
			writer.WriteLine(_input[i].defaultKeyName + "=" + (int)_input[i].keyCode);
		}

		writer.Close();
		Debug.Log(this + " сохранения настроек привязки клавиш: " + Path());
	}

	public bool GetKey(string name)
	{
		return Input.GetKey(FindKey(name));
	}

	public bool GetKeyDown(string name)
	{
		return Input.GetKeyDown(FindKey(name));
	}

	public bool GetKeyUp(string name)
	{
		return Input.GetKeyUp(FindKey(name));
	}
}