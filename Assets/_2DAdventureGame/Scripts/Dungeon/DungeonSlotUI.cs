using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private DungeonSelectUI dungeonSelectUI;

    [SerializeField]
    private DungeonData dungeonData;

    [SerializeField]
    private Image thumbnail;

    [SerializeField]
    private TMP_Text nameText;

    [SerializeField]
    private TMP_Text difficultyText;

    private IDungeonPresenter dungeonPresenter;

    private void Start()
    {
        thumbnail.sprite = dungeonData.thumbnail;
        nameText.text = dungeonData.name;
        difficultyText.text = dungeonData.difficulty;
    }

    public void Init(IDungeonPresenter presenter)
    {
        dungeonPresenter = presenter;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        dungeonSelectUI.ShowDungeonInfo(dungeonData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dungeonSelectUI.HideDungeonInfo();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            dungeonPresenter.TryEnterDungeon(dungeonData);
        }
    }
}
