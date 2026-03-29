using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LootComponent : MonoBehaviour, IInteractable
{
    public List<InventorySlot> lootItems = new List<InventorySlot>();
    public LootUI lootUI;
    public GameObject inventoryUI;
    public Transform player;

    private SpriteRenderer spriteRenderer;
    private float interactDistance = 1f;
    private AttackMonsterAIComponent ai;

    public void OnHoverExit()
    {
        spriteRenderer.color = Color.white;
    }

    public void OnHoverEnter()
    {
        spriteRenderer.color = Color.gray;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ai = GetComponent<AttackMonsterAIComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Oninteract()
    {
        bool isInvOpen = inventoryUI.activeSelf;
        inventoryUI.SetActive(!isInvOpen);
        lootUI.Toggle(this);
    }

    public bool IsInteractable()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if (ai.GetState() != MonsterState.Dead)
        {
            return false;
        }

        if (distance > interactDistance)
        {
            Debug.Log("거리가 멀어");
            return false;
        }

        return true;
    }
}
