using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HangManGame : MonoBehaviour
{
	public string answerWord;
	public TMP_InputField inputField;
	public Image hangImage;
	public List<Sprite> hangSprite;
	public List<TextMeshProUGUI> words;

	private int count = 0;
	private int check = 0;

	private void Start()
	{
		count = 0;
		check = 0;
		inputField.ActivateInputField();
		inputField.characterLimit = 1; // �ѱ��ڸ� �� �� �ֵ���
		answerWord = answerWord.ToUpper();
	}

	public void WrongWord()
	{
		if(count >= hangSprite.Count)
		{
			return;
		}
		count++;
		hangImage.sprite = hangSprite[count];
	}

	public void CheckWord(string input)
	{
		bool isSuccess = false;
		input = input.ToUpper(); // �빮�ڷ� �ٲ㼭 ��
		for (int i = 0; i < answerWord.Length; i++)
		{
			if (input[0] == answerWord[i])
			{
				words[i].text = input;
				check++;
				isSuccess = true;
			}
		}

		if(check == answerWord.Length)
			print("����");

		if (!isSuccess)
			WrongWord();

		inputField.text = string.Empty;
	}
}
