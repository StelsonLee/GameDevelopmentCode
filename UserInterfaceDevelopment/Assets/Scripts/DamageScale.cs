using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageScale : MonoBehaviour {

    [SerializeField]
    private GameObject damageSource;
    [SerializeField]
    private Image imagesource;
    [SerializeField]
    private GameObject HAlert;

    private Damageable damage;
    private float totalHealth;
    private float totalSheild;
    private float health;
    private float sheild;
    public bool _Health = true;

    void Awake()
    {
        damage = damageSource.GetComponent<Damageable>();
        totalHealth = damage.health;
        totalSheild = damage.sheild;
        Debug.Log("TotalHEalth = " + totalHealth);
        Debug.Log("TotalS = " + totalSheild);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_Health)
        {
            health = damage.health;
            transform.localScale = new Vector3(health / totalHealth, 1, 1);

            if (health < totalHealth / 2)
            {
                transform.GetComponent<Image>().color = Color.red;
                HAlert.SetActive(true);
            }
        }
        else if(!_Health)
        {
            sheild = damage.sheild;
            transform.localScale = new Vector3(sheild / totalSheild, 1, 1);
        }
        
	}
}
