using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private GameObject deathObject;
    public float currentHealth { get; private set; }
    private Animator anim;
    private PlayerMovement movement;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
        }
        else
        {
            anim.SetTrigger("Die");
            movement.enabled = false;
            StartCoroutine(waiter());
            if (canvasObject != null && deathObject != null)
            {
                TMP_Text textMeshPro = deathObject.GetComponent<TMP_Text>();
                textMeshPro.text = "You Died";
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }

    IEnumerator waiter()
    {
    yield return new WaitForSeconds(5);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
}