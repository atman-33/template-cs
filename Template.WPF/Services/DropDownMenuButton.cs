using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Template.WPF.Services
{
    /// <summary>
    /// ドロップ ダウン メニューを表示する為のボタン コントロール クラスです。
    /// </summary>
    public sealed class DropDownMenuButton : ToggleButton
    {
        /// <summary>
        /// ContextMenuが閉じられた後、ContextMenuClosePeriodSeconds経過しないとContextMenuは開けない。
        /// </summary>
        private const double ContextMenuClosePeriodSeconds = 0.1;

        /// <summary>
        /// 最後にIsOpenがFalse（UnChecked）になった日時  
        /// </summary>
        private DateTime _lastOnUncheckedDateTime;

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public DropDownMenuButton()
        {
            var binding = new Binding("DropDownContextMenu.IsOpen") { Source = this };
            this.SetBinding(DropDownMenuButton.IsCheckedProperty, binding);
        }

        /// <summary>
        /// ドロップ ダウンとして表示するコンテキスト メニューを取得または設定します。
        /// </summary>
        public ContextMenu DropDownContextMenu
        {
            get
            {
                return this.GetValue(DropDownContextMenuProperty) as ContextMenu;
            }
            set
            {
                this.SetValue(DropDownContextMenuProperty, value);
            }
        }

        /// <summary>
        /// コントロールがクリックされた時のイベントです。
        /// </summary>
        protected override void OnClick()
        {
            var period = (DateTime.Now - _lastOnUncheckedDateTime).TotalSeconds;
            Debug.WriteLine("period:" + period.ToString());
            if (period <= ContextMenuClosePeriodSeconds)
            {
                return;
            }

            if (this.DropDownContextMenu == null) { return; }

            this.DropDownContextMenu.PlacementTarget = this;
            this.DropDownContextMenu.Placement = PlacementMode.Bottom;
            this.DropDownContextMenu.IsOpen = !DropDownContextMenu.IsOpen;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
        }

        protected override void OnUnchecked(RoutedEventArgs e)
        {
            base.OnUnchecked(e);
            _lastOnUncheckedDateTime = DateTime.Now;
        }

        /// <summary>
        /// ドロップ ダウンとして表示するメニューを表す依存プロパティです。
        /// </summary>
        public static readonly DependencyProperty DropDownContextMenuProperty = DependencyProperty.Register("DropDownContextMenu", typeof(ContextMenu), typeof(DropDownMenuButton), new UIPropertyMetadata(null));
    }

    //// テストの覚え書き。ContextMenuを拡張する場合は下記を参考にすればよい。
    ////public sealed class DropDownContextMenu : ContextMenu
    ////{
    ////    private DateTime _lastClosedDateTime;

    ////    protected override void OnClosed(RoutedEventArgs e)
    ////    {
    ////        _lastClosedDateTime = DateTime.Now;
    ////        base.OnClosed(e);

    ////        Debug.WriteLine("Close:" + _lastClosedDateTime.ToString());
    ////    }
    ////}
}