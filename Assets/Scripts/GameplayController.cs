using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameplayController : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<Texture> textures;

    [Inject] private TableGenerator _tableGenerator;
    private List<GameObject> _cardsInstances = new List<GameObject>();
    private List<Card> _cards = new List<Card>();
    private Card _openedCard;
    private bool _openingLocked = true;

    public void InitTable(int count)
    {
        var points = _tableGenerator.GenerateSpawnPoints(count);
        points.Shuffle();
        int code = 0;
        for (int i = 0; i < points.Count; i++)
        {
            var instance = Instantiate(prefab, spawnPoint);
            instance.transform.localPosition = points[i];
            _cardsInstances.Add(instance);
            var card = instance.GetComponent<Card>();
            code = i / 2;
            card.Init(code, textures[code]);
            card.Open();
            _cards.Add(card);
        }
        StartCoroutine(CloseAll());
    }

    private IEnumerator CloseAll()
    {
        yield return new WaitForSeconds(2f);
        foreach (var card in _cards) { card.Close(); }
        _openingLocked = false;
    }

    public void OnClick(Vector2 pointerPos)
    {
        if (_openingLocked) return;
        var ray = AppController.Instance.Camera.ScreenPointToRay(pointerPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var card = hit.transform.GetComponentInParent<Card>();
            if (card == null) return;
            if (card.isOpen) return;
            card.Open();
            if (_openedCard == null)
            {
                _openedCard = card;
                return;
            }
            if (_openedCard.code != card.code)
            {
                StartCoroutine(CloseTwoCards(_openedCard, card));
            }
            else
            {
                _openedCard = null;
                CheckVictory();
            }
        }
    }

    private IEnumerator CloseTwoCards(Card card1, Card card2)
    {
        _openingLocked = true;
        yield return new WaitForSeconds(2f);
        card1.Close();
        card2.Close();
        _openedCard = null;
        _openingLocked = false;
    }

    private void CheckVictory()
    {
        if (_cards.All(x => x.isOpen))
        {
            _openingLocked = true;
            StartCoroutine(VictoryScenario());
        }
    }

    private IEnumerator VictoryScenario()
    {
        yield return new WaitForSeconds(1f);
        foreach (var card in _cardsInstances)
        {
            var rb = card.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddExplosionForce(100f, spawnPoint.position, 10f);
        }
        yield return new WaitForSeconds(2f);
    }

    public void ClearTable()
    {
        for (int i = _cardsInstances.Count - 1; i >= 0; i--)
        {
            GameObject.Destroy(_cardsInstances[i]);
        }
        _cardsInstances.Clear();

    }
}
