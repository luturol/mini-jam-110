using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossManagement : MonoBehaviour
{
    [Header("Debug pourposes")]
    [SerializeField] private List<BossNotification> _bossNotifications;
    [SerializeField] private List<BossEvent> _bossEvents;
    
    private DeliverProject _deliverProject;

    // Start is called before the first frame update
    void Start()
    {
        _deliverProject = GetComponent<DeliverProject>();

        _bossNotifications = GetComponentsInChildren<BossNotification>().ToList();
        _bossEvents = GetComponentsInChildren<BossEvent>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        var events = _bossEvents.Where(e => e.CanShowNotification());

        foreach(BossEvent _ in events)
        {
            Debug.Log("show notification");            
            _.Show();
            _deliverProject.SubtractDays(_.GetBossNotification().GetMinusDays());
        }

        _bossEvents.RemoveAll(e => events.Contains(e));
    }
}
