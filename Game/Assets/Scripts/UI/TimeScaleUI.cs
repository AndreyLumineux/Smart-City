using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class TimeScaleUI : MonoBehaviour
    {
        private Text text;
        
        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void OnGUI()
        {
            text.text = Time.timeScale.ToString(CultureInfo.CurrentCulture);
        }
    }
}
