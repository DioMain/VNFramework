using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    [SerializeField]
    private GameObject TempObject;

    [SerializeField]
    private Transform containerTransform;

    [SerializeReference]
    private Dictionary<string, CharacterObject> characters = new Dictionary<string, CharacterObject>();

    public void AddCharacter(string key, Sprite sprite, Vector2 position, Vector2 size, bool show = true)
    {
        if (characters.ContainsKey(key))
        {
            Debug.LogError("Такой ключ уже содержится");
            return;
        }

        Vector2 nsize = size == Vector2.zero ? TempObject.GetComponent<RectTransform>().localScale : size;

        GameObject n = Instantiate(TempObject, position, Quaternion.identity, containerTransform);

        n.transform.localScale = nsize;

        CharacterObject character = n.GetComponent<CharacterObject>();

        character.SetImage(sprite);

        characters.Add(key, character);

        if (show)
            character.Show();
    }

    public void RemoveCharacter(string key, bool instance = false)
    {
        if (!characters.ContainsKey(key))
        {
            Debug.LogError("Такой ключ не содержится");
            return;
        }

        StartCoroutine(DeleteCoroutine(key, instance));
    }

    public void SetCharacterState(string key, CharacterObject.SelectionState state)
    {
        if (!characters.ContainsKey(key))
        {
            Debug.LogError("Такой ключ не содержится");
            return;
        }

        characters[key].SetByState(state);
    }

    private IEnumerator DeleteCoroutine(string key, bool instance)
    {
        CharacterObject character = characters[key];

        characters.Remove(key);

        if (instance)
        {
            Destroy(character);
            yield return null;
        }
        else
        {
            if (character.State == CharacterObject.SelectionState.Select)
            {
                character.Unselect();
                yield return new WaitForSeconds(0.2f);
            }

            character.Hide();

            yield return new WaitForSeconds(0.5f);

            Destroy(character);
        }
    }
}
