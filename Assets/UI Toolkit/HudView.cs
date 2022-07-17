using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HudView : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset heartTemplate;
    private VisualElement healthBar;

    private void Start()
    {
        var document = GetComponent<UIDocument>();
        healthBar = document.rootVisualElement.Q("HealthBar");
    }

    private void Update()
    {
        float currentHealth;
        if (Player.Instance == null)
        {
            currentHealth = 0;
        }
        else
        {
            var health = Player.Instance.GetComponent<Health>();
            currentHealth = health.CurrentHealth;
            FillHearts(Mathf.CeilToInt(health.maxHealth));
        }

        foreach (var (heart, index) in healthBar.Children().Select((h, i) => (h, i)))
        {
            var fullness = currentHealth - index;
            heart.Q("Full").style.display = fullness >= 1.0 ? DisplayStyle.Flex : DisplayStyle.None;
            heart.Q("Half").style.display = fullness >= 0.5 && fullness < 1.0 ? DisplayStyle.Flex : DisplayStyle.None;
            heart.Q("Empty").style.display = fullness < 0.5 ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    private void FillHearts(int hearts)
    {
        if (hearts != healthBar.childCount)
        {
            healthBar.Clear();
            foreach (var _ in Enumerable.Range(0, hearts))
            {
                healthBar.Add(heartTemplate.Instantiate());
            }
        }
    }
}