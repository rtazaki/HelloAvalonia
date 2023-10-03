# HelloAvalonia

- Linux 環境向け GUI アプリケーション開発のベースとして、Avalonia UI を試す。

## 何ができるか

- .NET が使える環境であれば、WPF アプリケーションをクロスプラットフォームで動作させることができる。

## 手始めに実施

- 以下を参考に、Pages 配下のページを ComboBox を切り替えることで画面遷移できることを確認。
  - [Avalonia.Samples](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/Routing/BasicViewLocatorSample)
- ポイントは、TransitioningContentControl と、ComboBox の属性。
  - ItemsSource と SelectedIndex を使った。(SelectedItem や SelectedValue では成功していない。)
  - Windows の xaml だと、ComboBox に DisplayMemberPath や SelectedValuePath があるが、Avalonia の xaml には存在しない。
  - PageInfo クラスを使って、ComboBox.ItemTemplate 以下の表示を Name にする。
  - SelectedIndex の set プロパティにて、CurrentPage: ransitioningContentControl を更新してやることにした。
- Windows 上では、2 番目のページの「こんにちは」が表示されたが、Ubuntu ではどうか?
  - =>Raspberry Pi OS ではフォント埋め込みに成功したため、問題なし。

## フォント埋め込み

- Raspberry Pi OS ではデフォルトフォントが見つからない。(Default font family name can't be null or empty.)
- [得られた回答](https://github.com/AvaloniaUI/Avalonia/issues/11084)を参考に、Program.cs 側で、フォントマネージャーに対してデフォルトフォントを埋め込み。

## 単独実行アプリの作成

- PowerShell にて、以下を実行。
- `dotnet publish -c Release -r linux-arm -p:PublishSingleFile=true --self-contained true`
  - (Ubuntu の場合、linux-x64)
- bin\Release\net7.0\linux-arm\publish に、以下の 4 ファイルが出来上がった。
  - HelloAvalonia
  - HelloAvalonia.pdb
  - libHarfBuzzSharp.so
  - libSkiaSharp.so
- pdb ファイルはプロジェクトの設定で Release ビルドのときに消せば良いので、
  この 3 ファイルのみを、実行環境にコピーすれば良い。

## アプリの実行

- sudo chmod で、HelloAvalonia に対して実行権を付与する。
- Hello.sh とかを作って、HelloAvalonia を実行する。

## ファイルオープンダイアログの実装

- ボタンコマンドに、コマンドパラメータとして、Window を引き渡し。
- ViewModel 側で、引数を受け取りダイアログを開く。
- テキストボックスに値を反映させるために、プロパティを書いた。
  - このプロパティの書き方は慣れだとは思うが、分かりにくいなら、ReactiveProperty にしてもいいかも。
