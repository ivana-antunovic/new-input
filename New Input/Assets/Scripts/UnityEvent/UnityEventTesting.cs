using UnityEngine;
using UnityEngine.Events;

public class UnityEventTesting : MonoBehaviour
{
    [SerializeField] UnityEvent m_MyEvent;
    [SerializeField] UnityEvent m_OurEvent;

    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();

        // m_MyEvent.AddListener(DoSomething);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && m_MyEvent != null)
        {
            m_MyEvent.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            m_OurEvent.Invoke();
        }
    }

    void DoSomething()
    {
        Debug.Log("Callback called");
    }

}
