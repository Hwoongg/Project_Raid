﻿using UnityEngine;
using UnityEngine.UI;

public class UIChatInputFieldUpdater : MonoBehaviour
{
    ChatRule ChatRuleCache;
    InputField InputFieldCache;

    void Start()
    {
        StartCoroutine(Yielder.GetCoroutine(() =>
        {
            InputFieldCache = GetComponent<InputField>();
            ChatRuleCache = GameObject.Find("Chat Rules").GetComponent<ChatRule>();
            if (Utils.IsValid(ChatRuleCache) && Utils.IsValid(InputFieldCache))
            {
                InputFieldCache.onEndEdit.AddListener(ChatRuleCache.SendChatMessage);
            }
        }, 1.0f));
    }
}