using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    public Image[] hearts;
    public int healthPerHeart = 10;

    public void UpdateHearts(int currentHealth)
    {
        int heartsToShow = Mathf.CeilToInt(currentHealth / (float)healthPerHeart);

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < heartsToShow;
        }
    }
}
