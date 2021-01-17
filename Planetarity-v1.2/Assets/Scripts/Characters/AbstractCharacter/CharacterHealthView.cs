namespace Planetarity
{
    using System;
    using TMPro;

    public class CharacterHealthView
    {
        private readonly TMP_Text healthText;

        public CharacterHealthView(TMP_Text text)
        {
            healthText = text;
        }

        public void DisplayHealth(float value)
        {
            healthText.text = $"{value:0.0}";
        }
    }
}