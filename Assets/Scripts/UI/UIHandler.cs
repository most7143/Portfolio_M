using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public static class UIHandler
    {
        public static void UpdateGauge(Image gaugeBar, float maxGauge, float currentGauge, TextMeshProUGUI text = null)
        {
            if (gaugeBar != null)
            {
                gaugeBar.fillAmount = currentGauge / maxGauge;  // 게이지의 fillAmount를 전달받은 값으로 설정
            }

            if (text != null)
            {
                text.SetText(currentGauge + " / " + maxGauge);
            }
        }

        public static void FadeOut()
        {
            UIFade fade = ResourcesManager.Instance.Load(UINames.UIFade).GetComponent<UIFade>();

            if (fade != null)
            {
                fade.FadeIn();
            }
        }
    }
}