# 同期させたいオブジェクトの追加

1. 同期させたいオブジェクトに SendObject.cs をアタッチする
1. 同期させたいオブジェクトに SyncObject.cs をアタッチした Prefub を Resources に追加する。(SendObject.cs を外しておく)
1. SendObject.cs のインスペクターで Resources に保存した Prefub の名前を設定する。
1. SendObject.cs のインスペクターで同期するパラメータを設定する
