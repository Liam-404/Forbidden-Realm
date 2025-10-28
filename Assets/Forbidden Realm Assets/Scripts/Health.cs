using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// A reusable health system that manages health, a UI health bar, and damage dealing.
/// </summary>
public class Health : MonoBehaviour
{
    // Health properties and events
    public UnityEvent<float> OnHealthChanged;
    public UnityEvent OnDeath;

    [Tooltip("The maximum health of the character.")]
    [SerializeField]
    private float maxHealth = 100f;

    private float currentHealth;

    public float CurrentHealth
    {
        get { return currentHealth; }
        private set { currentHealth = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        private set { maxHealth = value; }
    }

    public float HealthNormalized
    {
        get { return currentHealth / maxHealth; }
    }

    // UI health bar properties
    [Tooltip("The UI Slider or Image to update.")]
    [SerializeField]
    private Slider healthSlider;

    // Damage dealing properties
    [Tooltip("The amount of damage to deal.")]
    [SerializeField]
    private float damageAmount = 10f;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        // Initial setup for the UI health bar.
        if (healthSlider != null)
        {
            healthSlider.value = HealthNormalized;
        }

        // We can optionally add a listener here if this script is also the one being damaged
        // OnHealthChanged.AddListener(UpdateHealthBar);
    }

    /// <summary>
    /// Applies damage to the character's health.
    /// </summary>
    /// <param name="amount">The amount of damage to apply.</param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f);

        OnHealthChanged?.Invoke(HealthNormalized);
        UpdateHealthBar(HealthNormalized); // Directly update the health bar from here

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    /// <summary>
    /// Heals the character's health.
    /// </summary>
    /// <param name="amount">The amount of health to restore.</param>
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged?.Invoke(HealthNormalized);
        UpdateHealthBar(HealthNormalized); // Directly update the health bar from here
    }

    /// <summary>
    /// Updates the health bar's value.
    /// </summary>
    /// <param name="normalizedHealth">The health value between 0 and 1.</param>
    private void UpdateHealthBar(float normalizedHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = normalizedHealth;
        }
    }

    /// <summary>
    /// Handles the character's death.
    /// </summary>
    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        OnDeath?.Invoke();
        Destroy(gameObject, 1f);
    }

    /// <summary>
    /// This method is called when this object's collider hits another object.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // If this object is a "damage dealer", it will try to deal damage on collision.
        // It's a bit unconventional to have this logic in the Health script, but it combines all requested features.
        Health targetHealth = collision.gameObject.GetComponent<Health>();

        if (targetHealth != null && targetHealth != this) // Ensure we don't damage ourselves
        {
            targetHealth.TakeDamage(damageAmount);
        }
    }
}

