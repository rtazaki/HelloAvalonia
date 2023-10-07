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

- 本ソフトウェアは、デフォルトフォントとして[HackGen](https://github.com/yuru7/HackGen)を使用することにした。
  - Copyright (c) 2019, Yuko OTAWARA. with Reserved Font Name "白源", "HackGen"
  - SIL OPEN FONT LICENSE Version 1.1
- Raspberry Pi OS ではデフォルトフォントが見つからない。(Default font family name can't be null or empty.)
- [得られた回答](https://github.com/AvaloniaUI/Avalonia/issues/11084)を参考に、Program.cs 側で、フォントマネージャーに対してデフォルトフォントを埋め込み。
- フォントフォールバックの設定は、無くても動くかもしれない。

### UDEV Gothic 使ってみたかったがダメだった。

- DefaultFamilyName = "avares://HelloAvalonia/Assets/UDEVGothic-Regular.ttf#UDEVGothic",
  - `System.InvalidOperationException: 'Could not create glyphTypeface.'`

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

## 画面の大きさと配置、オートスクロールについて

### サイズ

- なるべく最後まで固定しないように、デフォルトの 800\*450 を削除

### 配置

- WindowStartupLocation="CenterOwner"とする。(親に合わせる)
  - この場合の親ウィンドウが何を指すのかは今の所わからない。
  - App.axaml.cs から MainWindow を呼んでいるけど、アプリケーションで画面サイズを決めることはないような...
- "CenterScreen"だと、何が何でもスクリーンの真ん中に表示させる?
- 典型的日本人の感覚で、なんとなく協調性を大切にした。

### オートスクロール

- とりあえず、先頭の DockPanel の外から ScrollViewer を設定してみた。
- これによって、オープンダイアログから、テキストボックスに何かを表示した際に
- 見切れずオートスクロールするようになった。
- 最善はサイズ超過しそうなコントロールにのみ限定して張るべき?

### その他設定

- RequestedThemeVariant を Default→Light に統一。(開発環境ではダーク、配布先ではライトだと困るため。)
- FluentTheme→SimpleTheme に変更したほうが、チープな環境で軽快動作させられる?
- FontSize="18"を MainWindow に設定して、うまく適用できた模様。
