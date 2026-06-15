using UnityEngine;

public class destro : MonoBehaviour
{
    public string _item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnChildDestroyed(GameObject destroyedChild)
    {
        string childTag = destroyedChild.tag;

        // ƒ^ƒO‚²‚Æ‚Éˆ—‚ğ•ªŠò‚³‚¹‚½‚¢ê‡‚Ì—á
        if (destroyedChild.CompareTag("Speed"))
        {
            _item = childTag;
        }
        if (destroyedChild.CompareTag("Up"))
        {
            _item = childTag;
        }
    }
}
