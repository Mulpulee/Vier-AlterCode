using UnityEngine;
using Entity.Interface;
using Entity;
public class TutorialBotDeath : IDeath {
    public void OnDeath(EntityBehaviour entity) {
        GameObject.Destroy(entity.gameObject);
    }

    public void Destroy() { }
}