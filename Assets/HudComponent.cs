using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using AcidOcean.UI;
using AcidOcean.Game;

[RequireComponent(typeof(CharStatsManagerPlayer))]
public class HudComponent : MonoBehaviour
{

    private CharStatsManagerPlayer _charStatsManagerPlayer;

    public UIDocument Hud;
    private VisualElement root;
    private HealthBar healthBar;
    public float currentHealth = 100;
    public float maxHealth = 100;
    [Range(0f, 1f)]
    public float healthPercent = 1f;
    private void Awake()
    {
        _charStatsManagerPlayer = GetComponent<CharStatsManagerPlayer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GlobalEventManager.PlayerRecieveDamage += GlobalEventManager_PlayerRecieveDamage;
        root = Hud.rootVisualElement;
        healthBar = root.Q<HealthBar>();
        healthBar.value = currentHealth/maxHealth;
    }

    private void GlobalEventManager_PlayerRecieveDamage(float damAmount)
    {
        currentHealth -= damAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthPercent = currentHealth/maxHealth;
        healthBar.value = healthPercent;
    }

    void Heal(int val)
    {
        currentHealth += val;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthPercent = (float)currentHealth / maxHealth;
        healthBar.value = healthPercent;
    }

    private void OnValidate()
    {
        if (healthBar != null)
        {
            healthBar.value = healthPercent;
            currentHealth = (int)(healthPercent * maxHealth);
        }
    }
}
