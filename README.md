# vrchat-udon-tools
VRChatUdon#向けの自作ツール群です。 gitの使い方がわからないのでしばらく単発公開・個人開発となります。 更新日 2026.3/12

## Scripts /うどん
### CycleSwitch
任意数のオブジェクトをスイッチを押す毎に順番に切り替えていくスイッチです。

## Requirements
- VRChat SDK3 Worlds
- UdonSharp

## Usage /使い方
1. Add script to a switch object
 スイッチ化したいオブジェクトにUdonBehaviourを追加します。
 ProgramSourceに実装したいudonをD&Dして追加します。
 (え、scriptがD&D出来ない？コンパイルした？)
2. Assign targets in Inspector
 インスペクターから各ギミックに合わせてオブジェクトを追加したりしなかったりしてください。
3. Interact to switch
 Playモードとかでオブジェクトをインタラクト(操作)すれば動く...はず！
 動かなかったらごめんね;;

## License
MIT
