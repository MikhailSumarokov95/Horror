using UnityEngine;
using UnityEngine.UI;

public class WaitForButtonClick : CustomYieldInstruction
{
    private bool _keepWaiting;
    public override bool keepWaiting { get { return !_keepWaiting; } }

    public WaitForButtonClick(Button button)
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton() => _keepWaiting = true;
}
