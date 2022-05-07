using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] [Range(1, 100)] int minHP = 1;
    [SerializeField] [Range(1, 100)] int maxHP = 100;
    [SerializeField] int criticalHP = 20;


    public CharacterDatabase characterDB;

    public SpriteRenderer artworkSprite;

    private int selectedOption = 0;

    private string name;
    private int hp;

    public float hpDecreaseRate = 0.4f;


    private void Awake()
    {
        if (minHP > maxHP)
        {
            Debug.LogError("Player: maxHP cannot be greater than minHP");
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);

        hp = maxHP;
        InvokeRepeating("DecreaseHP", 1f, hpDecreaseRate);
    }

    // GETTERS AND SETTERS //

    public string GetName()
    {
        return name;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public int GetHp()
    {
        return hp;
    }

    public void SetHp(int hp)
    {
        this.hp = hp;
    }



    // HP METHODS //

    public bool HealthIsCritical()
    {
        return hp <= criticalHP;
    }

    public bool HealthIsBelowMinimum()
    {
        return hp < minHP;
    }

    public void Heal(int amount)
    {
        if (hp >= 100)
        {
            hp = 100;
            return;
        }

        hp += amount;
    }

    public void DecreaseHP()
    {
        if (hp < 0)
        {
            hp = 0;
            return;
        }

        hp -= 1;
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;

    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }
}
