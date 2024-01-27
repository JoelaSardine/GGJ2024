using System;
using TMPro;
using UnityEngine;

public class ConfirmationPopup : MonoBehaviour
{
	public TMP_Text Text;
	public TMP_Text ConfirmText;
	public TMP_Text CancelText;

	public Action OnConfirm;
	public Action OnCancel;

	public void Bind(string text, Action onConfirm)
	{
		this.Bind(text, "Confirm", "Cancel", onConfirm, null);
	}

	public void Bind(string text, string confirm, string cancel, Action onConfirm, Action onCancel)
	{
		this.Text.text = text;

		this.ConfirmText.gameObject.SetActive(!string.IsNullOrWhiteSpace(confirm));
		this.ConfirmText.text = confirm;

		this.CancelText.gameObject.SetActive(!string.IsNullOrWhiteSpace(cancel));
		this.CancelText.text = cancel;

		this.OnConfirm = onConfirm;
		this.OnCancel = onCancel;

		this.gameObject.SetActive(true);
	}

	public void Dismiss()
	{
		this.OnConfirm = null;
		this.OnCancel = null;

		this.gameObject.SetActive(false);
	}

	public void Button_OnConfirm()
	{
		this.OnConfirm?.Invoke();
		this.Dismiss();
	}

	public void Button_OnCancel()
	{
		this.OnCancel?.Invoke();
		this.Dismiss();
	}
}
