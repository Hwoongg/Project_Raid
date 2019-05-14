using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneRule : RulePrototype
{
    /* TODO: 스플래시 씬 음원 나오면 넣을거임.
    //AudioSource SplashSceneAudio;

    //void Start()
    //{
    //    SplashSceneAudio = GetComponent<AudioSource>();

    //    if (SplashSceneAudio.clip.loadState == AudioDataLoadState.Loaded)
    //    {
    //        SplashSceneAudio.Play();
    //    }
    //}
    */
    
    void Update()
    {
        // 아무키나 눌리면 + 한번만 실행되도록 보장.
        // Wait until any key has been pressed down, Make sure this logic executes once.
        if (Input.anyKeyDown && false == IsMovingNextScene)
        {
            IsMovingNextScene = true;
            // PressAnyKey 로 메시지 전달.
            // Send message to PressAnyKey, ScreenFader.
            LogicEventListener.Invoke(eEventType.FOR_UI, eEventMessage.ON_ANYKEY_PRESSED);
            // 타이틀 씬 로드.
            // Load the title scene.
            SceneManager.LoadScene("Title");
        }
    }

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            // 로고가 완전히 나타나면 처리됨.
            // Executes after logo full appeared.
            case eEventMessage.SPLASH_FULLY_APPEARED:
                // 업데이트 중지.
                // Cancel the update.
                IsMovingNextScene = true;
                // 타이틀 씬 로드.
                // Load the title scene.
                SceneManager.LoadScene("Title");
                break;
        }
    }
};
