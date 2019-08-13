using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace YUI.WPF.YControls
{
    /// <summary>
    /// 自动完成控件，输入时显示建议列表
    /// </summary>
    public class YAutoCompleteBox : TextBox
    {
        /// <summary>
        /// 选择项改变事件
        /// </summary>
        public event EventHandler<object> SuggestChanged;

        /// <summary>
        /// 存储所有建议的集合
        /// </summary>
        public static readonly DependencyProperty SuggestsProperty = DependencyProperty.Register(
            "Suggests", typeof(IEnumerable), typeof(YAutoCompleteBox), new PropertyMetadata(default(IEnumerable)));

        /// <summary>
        /// 存储所有建议的集合
        /// </summary>
        public IEnumerable Suggests
        {
            get => (IEnumerable)GetValue(SuggestsProperty);
            set => SetValue(SuggestsProperty, value);
        }

        /// <summary>
        /// 当前建议列表
        /// </summary>
        public static readonly DependencyProperty CurrentSuggestsProperty = DependencyProperty.Register(
            "CurrentSuggests", typeof(ObservableCollection<object>), typeof(YAutoCompleteBox), new PropertyMetadata(new ObservableCollection<object>()));

        /// <summary>
        /// 当前建议列表
        /// </summary>
        public ObservableCollection<object> CurrentSuggests
        {
            get => (ObservableCollection<object>)GetValue(CurrentSuggestsProperty);
            set => SetValue(CurrentSuggestsProperty, value);
        }

        /// <summary>
        /// 选择的建议项
        /// </summary>
        public static readonly DependencyProperty SelectSuggestProperty = DependencyProperty.Register(
            "SelectSuggest", typeof(object), typeof(YAutoCompleteBox), new PropertyMetadata(default,
            (o, args) =>
            {
                var newValue = args.NewValue;
                if (!(o is YAutoCompleteBox control))
                    return;

                if (newValue != null)
                    control.SetText(newValue.ToString());

                control.SuggestChanged?.Invoke(control, newValue);
            }));

        /// <summary>
        /// 选择的建议项
        /// </summary>
        public object SelectSuggest
        {
            get => GetValue(SelectSuggestProperty);
            set => SetValue(SelectSuggestProperty, value);
        }

        /// <summary>
        /// 输入内容到显示建议项的延时
        /// </summary>
        public static readonly DependencyProperty DelayTimeProperty = DependencyProperty.Register(
            "DelayTime", typeof(double), typeof(YAutoCompleteBox), new PropertyMetadata(500.0));

        /// <summary>
        /// 输入内容到显示建议项的延时
        /// </summary>
        public double DelayTime
        {
            get => (double)GetValue(DelayTimeProperty);
            set => SetValue(DelayTimeProperty, value);
        }

        /// <summary>
        /// 弹出建议项的最小输入字数
        /// </summary>
        public static readonly DependencyProperty ThresholdProperty = DependencyProperty.Register(
            "Threshold", typeof(uint), typeof(YAutoCompleteBox), new PropertyMetadata(1u));

        /// <summary>
        /// 弹出建议项的最小输入字数
        /// </summary>
        public uint Threshold
        {
            get => (uint)GetValue(ThresholdProperty);
            set => SetValue(ThresholdProperty, value);
        }

        /// <summary>
        /// 当按下回车是是否默认选择第一项
        /// </summary>
        public static readonly DependencyProperty IsSelectFirstProperty = DependencyProperty.Register(
            "IsSelectFirst", typeof(bool), typeof(YAutoCompleteBox), new PropertyMetadata(true));

        /// <summary>
        /// 当按下回车是是否默认选择第一项
        /// </summary>
        public bool IsSelectFirst
        {
            get => (bool)GetValue(IsSelectFirstProperty);
            set => SetValue(IsSelectFirstProperty, value);
        }

        /// <summary>
        /// 是否在获取焦点是显示建议项
        /// </summary>
        public static readonly DependencyProperty IsShowSuggestsOnFocusProperty = DependencyProperty.Register(
            "IsShowSuggestsOnFocus", typeof(bool), typeof(YAutoCompleteBox), new PropertyMetadata(false));

        /// <summary>
        /// 是否在获取焦点是显示建议项
        /// </summary>
        public bool IsShowSuggestsOnFocus
        {
            get => (bool)GetValue(IsShowSuggestsOnFocusProperty);
            set => SetValue(IsShowSuggestsOnFocusProperty, value);
        }

        /// <summary>
        /// 显示建议项的模板
        /// </summary>
        public static readonly DependencyProperty SuggestTemplateProperty = DependencyProperty.Register(
            "SuggestTemplate", typeof(DataTemplate), typeof(YAutoCompleteBox), new PropertyMetadata(default(ControlTemplate)));

        /// <summary>
        /// 显示建议项的模板
        /// </summary>
        public DataTemplate SuggestTemplate
        {
            get => (DataTemplate)GetValue(SuggestTemplateProperty);
            set => SetValue(SuggestTemplateProperty, value);
        }

        /// <summary>
        /// 下拉列表的最大高度
        /// </summary>
        public static readonly DependencyProperty MaxDropDownHeightProperty = DependencyProperty.Register(
            "MaxDropDownHeight", typeof(double), typeof(YAutoCompleteBox), new PropertyMetadata(200.0));

        /// <summary>
        /// 下拉列表的最大高度
        /// </summary>
        public double MaxDropDownHeight
        {
            get => (double)GetValue(MaxDropDownHeightProperty);
            set => SetValue(MaxDropDownHeightProperty, value);
        }

        private bool IsInnerChanged { get; set; }

        private bool IsInnerSelected { get; set; }

        static YAutoCompleteBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YAutoCompleteBox),
                new FrameworkPropertyMetadata(typeof(YAutoCompleteBox)));
        }

        private Popup _partPopup;
        private ListBox _partListBox;
        private readonly Timer _delayTimer;

        private delegate void TextChangedCallback();

        /// <summary>
        /// 
        /// </summary>
        public YAutoCompleteBox()
        {
            _delayTimer = new Timer { Interval = DelayTime };
            _delayTimer.Elapsed += DelayTimer_Elapsed;
            TextChanged += YAutoCompleteTextBox_TextChanged;
            PreviewKeyDown += (sender, args) =>
            {
                switch (args.Key)
                {
                    case Key.Down:
                        IsInnerSelected = true;

                        if (_partListBox.SelectedIndex == _partListBox.Items.Count - 1)
                            _partListBox.SelectedIndex = 0;
                        if (_partListBox.SelectedIndex < _partListBox.Items.Count)
                            _partListBox.SelectedIndex++;

                        _partListBox.ScrollIntoView(_partListBox.SelectedItem);

                        break;
                    case Key.Up:
                        IsInnerSelected = true;

                        if (_partListBox.SelectedIndex > 0)
                            _partListBox.SelectedIndex--;
                        else
                            _partListBox.SelectedIndex = _partListBox.Items.Count - 1;

                        _partListBox.ScrollIntoView(_partListBox.SelectedItem);

                        break;
                    case Key.Return:
                        UpdateSelect();
                        break;
                }
            };

            GotFocus += (sender, args) =>
            {
                if (IsShowSuggestsOnFocus)
                    StartChanged();
            };

            LostFocus += (sender, args) =>
            {
                HidePopup();

                if (SelectSuggest?.ToString() != Text)
                    SelectSuggest = null;
            };
        }

        private void ShowPopup()
        {
            if (_partPopup != null && !_partPopup.IsOpen)
                _partPopup.IsOpen = true;
        }

        private void HidePopup()
        {
            if (_partPopup != null && _partPopup.IsOpen)
                _partPopup.IsOpen = false;
        }

        private void DelayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _delayTimer.Stop();
            Dispatcher?.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new TextChangedCallback(CurrentTextChanged));
        }


        private void YAutoCompleteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text.Length > 0)
                StartChanged();
            else
            {
                SelectSuggest = null;
                HidePopup();
            }
        }

        private void StartChanged()
        {
            if (DelayTime > 10)
            {
                _delayTimer.Interval = DelayTime;
                _delayTimer.Start();
            }
            else
            {
                _delayTimer.Interval = 10;
                _delayTimer.Start();
            }
        }

        private void AddSuggest(List<object> entries)
        {
            foreach (var entry in entries)
            {
                CurrentSuggests.Add(entry);
            }
        }

        private void CurrentTextChanged()
        {
            try
            {
                if (!IsInnerChanged)
                    SelectSuggest = Suggests.Cast<object>().FirstOrDefault(suggest => suggest.ToString() == Text);

                if (Text.Length >= Threshold && !IsInnerChanged && IsFocused)
                {
                    CurrentSuggests.Clear();
                    var common = new List<object>();
                    var commonIgnore = new List<object>();
                    var start = new List<object>();
                    var startIgnore = new List<object>();
                    var keyCommon = new List<object>();
                    var keyCommonIgnore = new List<object>();
                    var keyStart = new List<object>();
                    var keyStartIgnore = new List<object>();
                    var contain = new List<object>();
                    var end = new List<object>();
                    var endIgnore = new List<object>();

                    foreach (var entry in Suggests)
                    {
                        var entryString = entry.ToString();
                        if (entryString == Text)
                        {
                            common.Add(entry);
                            continue;
                        }

                        if (string.Equals(entryString, Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            commonIgnore.Add(entry);
                            continue;
                        }

                        if (entryString.StartsWith(Text))
                        {
                            start.Add(entry);
                            continue;
                        }

                        if (entryString.StartsWith(Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            startIgnore.Add(entry);
                            continue;
                        }

                        if (entry is IYAutoCompleteBoxKeys keywords && keywords.Keywords?.Count() > 0)
                        {
                            if (keywords.Keywords.Contains(Text))
                            {
                                keyCommon.Add(entry);
                                continue;
                            }

                            if (keywords.Keywords.Any(keyword => string.Equals(keyword, Text, StringComparison.CurrentCultureIgnoreCase)))
                            {
                                keyCommonIgnore.Add(entry);
                                continue;
                            }

                            if (keywords.Keywords.Any(keyword => keyword.StartsWith(Text)))
                            {
                                keyStart.Add(entry);
                                continue;
                            }

                            if (keywords.Keywords.Any(keyword => keyword.StartsWith(Text, StringComparison.CurrentCultureIgnoreCase)))
                            {
                                keyStartIgnore.Add(entry);
                                continue;
                            }
                        }

                        if (entryString.Contains(Text) && !entryString.EndsWith(Text))
                        {
                            contain.Add(entry);
                            continue;
                        }

                        if (entryString.Contains(Text) && !entryString.EndsWith(Text))
                        {
                            contain.Add(entry);
                            continue;
                        }

                        if (entryString.EndsWith(Text))
                        {
                            end.Add(entry);
                            continue;
                        }

                        if (entryString.EndsWith(Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            endIgnore.Add(entry);
                        }
                    }

                    common.Sort(Comparison);
                    commonIgnore.Sort(Comparison);
                    start.Sort(Comparison);
                    startIgnore.Sort(Comparison);
                    keyCommon.Sort(Comparison);
                    keyCommonIgnore.Sort(Comparison);
                    keyStart.Sort(Comparison);
                    keyStartIgnore.Sort(Comparison);
                    contain.Sort(Comparison);
                    end.Sort(Comparison);
                    endIgnore.Sort(Comparison);

                    AddSuggest(common);
                    AddSuggest(commonIgnore);
                    AddSuggest(start);
                    AddSuggest(startIgnore);
                    AddSuggest(keyCommon);
                    AddSuggest(keyCommonIgnore);
                    AddSuggest(keyStart);
                    AddSuggest(keyStartIgnore);
                    AddSuggest(contain);
                    AddSuggest(end);
                    AddSuggest(endIgnore);

                    if (CurrentSuggests.Count == 1 && CurrentSuggests[0].ToString() == Text)
                        HidePopup();
                    else
                    {
                        if (CurrentSuggests.Count > 0)
                            ShowPopup();
                        else
                            HidePopup();
                    }
                }
                else
                {
                    HidePopup();
                }

                IsInnerChanged = false;
            }
            catch
            {
                //
            }
        }

        private int Comparison(object o1, object o2)
        {
            return string.CompareOrdinal(o1.ToString(), o2.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _partPopup = (GetTemplateChild("PART_Popup") as Popup);
            _partListBox = (GetTemplateChild("PART_PopupContent") as ListBox);

            if (_partListBox != null)
            {
                _partListBox.SelectionChanged += Part_ListBox_SelectionChanged;
            }
        }

        private void Part_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInnerSelected)
            {
                IsInnerSelected = false;
                return;
            }
            UpdateSelect();
        }

        private void UpdateSelect()
        {
            IsInnerChanged = true;
            HidePopup();
            var select = _partListBox.SelectedItem;
            if (select != null)
                SelectSuggest = select;
            else
            {
                if (!IsSelectFirst) return;

                if (_partListBox.Items.Count <= 0) return;

                SelectSuggest = _partListBox.Items[0];

                if (SelectSuggest != null)
                    SetText(SelectSuggest.ToString());
            }
        }

        private void SetText(string text)
        {
            Text = text;
            SelectionStart = Text.Length;
        }
    }
}
