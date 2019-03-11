・シーンファイルをBuildSettingに追加するの忘れるな
・一応VR対応してる
・sceneSystemのWait Time Safe Marginにアニメーション後にそのままでいる秒数を入力する
・sceneSystemのNext Sceneに次につなげたいシーンファイルをそのままドロップ
・拾い先　http://baba-s.hatenablog.com/entry/2017/11/14/110000
・public SceneObject m_nextScene;
　SceneManager.LoadScene( m_nextScene );
みたいな使い方ができる