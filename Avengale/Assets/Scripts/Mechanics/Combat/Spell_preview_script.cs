using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_preview_script : MonoBehaviour
{
    [Header("Spel properties")]
    public GameObject spell_name;
    public GameObject spell_type;
    public GameObject spell_description;
    public GameObject spell_cost;
    public GameObject spell_effect;
    public GameObject spell_icon;

    public void showSpell(int id)
    {
        GameObject.Find("Spell_preview").GetComponent<Open_button_script>().Open();
        var spell = GameObject.Find("Game manager").GetComponent<Spell_script>().spells[id];
        
        spell_name.GetComponent<Text_animation>().startAnim(spell.name, 1f);
        spell_type.GetComponent<Text_animation>().startAnim("<color=#828282>[" + spell.type + "]</color>", 0.01f);
        spell_description.GetComponent<Text_animation>().startAnim(spell.description, 0.01f);
        spell_cost.GetComponent<Text_animation>().startAnim("<color=#2E5AB3>-" + spell.resource_cost + " resource", 0.01f);
        spell_effect.GetComponent<Text_animation>().startAnim("<color=#00FF00>+" + spell.attribute + " " + spell.attribute_type + " " + spell.attribute_name, 0.01f);

        StopCoroutine("Wait");
        StartCoroutine("Wait");

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        GameObject.Find("Spell_preview").GetComponent<Close_button_script>().Close();
    }
}
