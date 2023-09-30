# HelloAvalonia

- Linux 環境向け GUI アプリケーション開発のベースとして、Avalonia UI を試す。

# 何ができるか

- .NET が使える環境であれば、WPF アプリケーションをクロスプラットフォームで動作させることができる。

# 手始めに実施

- 以下を参考に、Pages 配下のページを ComboBox を切り替えることで画面遷移できることを確認。
  - [Avalonia.Samples](https://github.com/AvaloniaUI/Avalonia.Samples/tree/main/src/Avalonia.Samples/Routing/BasicViewLocatorSample)
- ポイントは、TransitioningContentControl と、ComboBox の属性。
  - ItemsSource と SelectedIndex を使った。(SelectedItem や SelectedValue では成功していない。)
  - Windows の xaml だと、ComboBox に DisplayMemberPath や SelectedValuePath があるが、Avalonia の xaml には存在しない。
  - PageInfo クラスを使って、ComboBox.ItemTemplate 以下の表示を Name にする。
  - SelectedIndex の set プロパティにて、CurrentPage: ransitioningContentControl を更新してやることにした。
- Windows 上では、2 番目のページの「こんにちは」が表示されたが、Ubuntu ではどうか?(未確認)
