using UnityEngine;
using UnityEngine.UIElements;

public class HudView : MonoBehaviour
{
    private VisualElement healthBar;

    private void Start()
    {
        var document = GetComponent<UIDocument>();
        healthBar = document.rootVisualElement.Q("HealthBar");
    }

    private void Update()
    {
        float healthRatio;
        if (Player.Instance == null)
        {
            healthRatio = 0;
        }
        else
        {
            var health = Player.Instance.GetComponent<Health>();
            healthRatio = health.CurrentHealth / health.maxHealth;
        }

        healthBar.style.width = new StyleLength(Length.Percent(100 * healthRatio));
    }
}