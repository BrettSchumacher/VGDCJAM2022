using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : Usable
{
    [SerializeField]
    GameObject bubble_prefab;

    [SerializeField]
    string[] dialogue;

    [SerializeField]
    float triangle_area;
    [SerializeField]
    float max_altitude;

    GameObject bubble;
    TextMeshProUGUI text;
    Vector3 bubble_scale;

    int index;

    void Setup()
    {
        bubble = Instantiate(bubble_prefab);
        text = bubble.GetComponentInChildren<TextMeshProUGUI>();
        bubble.transform.position = transform.position + transform.up * 2;
        bubble_scale = bubble.transform.localScale;

        index = 0;
    }

    void Cleanup()
    {
        Destroy(bubble);
        text = null;
    }

    public void Use()
    {
        if(bubble == null)
        {
            Setup();
        }
        else if(index < dialogue.Length-1)
        {
            index++;
        }
        else
        {
            Cleanup();
            return;
        }

        text.text = dialogue[index];
    }

    protected override void Awake()
    {
        base.Awake();
        use_event.AddListener(Use);
    }

    protected override void Update()
    {
        base.Update();

        if(bubble == null)
        {
            if(usability_progress >= 1)
            {
                Vector3 prompt_position = user_position + (transform.position - user_position) * 0.5f;
                prompt_position += Vector3.up * 0.5f;
                InputPrompt._.Draw(InputCode.CONFIRM, prompt_position);
            }

            return;
        }

        if(usability_progress > 0)
        {
            if(usability_progress < 1)
            {
                bubble.transform.localScale = bubble_scale * Mathf.SmoothStep(0, 1, usability_progress);
            }
            else
            {
                Vector3 y_corrected_pos = transform.position.ModifyAt(1, user_position.y);
                Vector3 beeline = y_corrected_pos - user_position;

                // area = 0.5 * base * height
                // height = 2 * area / base
                float height = Mathf.Clamp(2 * triangle_area / user_distance, 0, max_altitude);

                Vector3 origin = user_position;
                Vector3 midpoint = beeline * 0.5f;
                Vector3 altitude = Vector3.Cross(midpoint, Vector3.forward).normalized * height * -Mathf.Sign(signed_user_distance);
                Vector3 apex = origin + midpoint + altitude;

                bubble.transform.position = apex;
            }
        }
        else
        {
            Cleanup();
        }
    }
}
