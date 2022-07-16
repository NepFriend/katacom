using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerk : MonoBehaviour
{
    [SerializeField]
    private Perk.PerkID id;

    [SerializeField]
    private Transform shotPosition;

    private PerkHandler perkHandler = new PerkHandler();

    public Perk CurrentPerk => perkHandler.Current;

    // Start is called before the first frame update
    void Start()
    {
        var perk = Perk.Create(id);
        perk.Target = gameObject;
        perk.SetShotPosition(shotPosition);
        perkHandler.AddPerk(perk);

        perkHandler.Equip(0);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPerk?.Update(Time.deltaTime);
    }
}
