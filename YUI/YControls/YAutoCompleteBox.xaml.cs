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

namespace YUI.YControls
{
    /// <summary>
    /// 自动完成控件，输入时显示建议列表
    /// </summary>
    public class YAutoCompleteBox : TextBox
    {
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
            get => (IEnumerable) GetValue(SuggestsProperty);
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
            get => (ObservableCollection<object>) GetValue(CurrentSuggestsProperty);
            set => SetValue(CurrentSuggestsProperty, value);
        }

        /// <summary>
        /// 选择的建议项
        /// </summary>
        public static readonly DependencyProperty SelectSuggestProperty = DependencyProperty.Register(
            "SelectSuggest", typeof(object), typeof(YAutoCompleteBox), new PropertyMetadata(default(object),
            (o, args) =>
            {
                var newValue = args.NewValue;
                var control = o as YAutoCompleteBox;
                if (control == null || newValue == null)
                    return;

                control.SetText(newValue.ToString());
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
            get => (double) GetValue(DelayTimeProperty);
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
            get => (uint) GetValue(ThresholdProperty);
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
            get => (bool) GetValue(IsSelectFirstProperty);
            set => SetValue(IsSelectFirstProperty, value);
        }

        /// <summary>
        /// 是否在获取焦点是显示建议项
        /// </summary>
        public static readonly DependencyProperty IsShowSeggestsOnFocusProperty = DependencyProperty.Register(
            "IsShowSeggestsOnFocus", typeof(bool), typeof(YAutoCompleteBox), new PropertyMetadata(false));

        /// <summary>
        /// 是否在获取焦点是显示建议项
        /// </summary>
        public bool IsShowSeggestsOnFocus
        {
            get => (bool) GetValue(IsShowSeggestsOnFocusProperty);
            set => SetValue(IsShowSeggestsOnFocusProperty, value);
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
            get => (DataTemplate) GetValue(SuggestTemplateProperty);
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
            get => (double) GetValue(MaxDropDownHeightProperty);
            set => SetValue(MaxDropDownHeightProperty, value);
        }

        private bool IsInnerChanged { get; set; }

        private bool IsInnerSlected { get; set; }

        static YAutoCompleteBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YAutoCompleteBox),
                new FrameworkPropertyMetadata(typeof(YAutoCompleteBox)));
        }

        private Popup part_Popup;
        private ListBox part_ListBox;
        private readonly Timer delayTimer;

        private delegate void TextChangedCallback();

        /// <summary>
        /// 
        /// </summary>
        public YAutoCompleteBox()
        {
            delayTimer = new Timer { Interval = DelayTime };
            delayTimer.Elapsed += DelayTimer_Elapsed;
            TextChanged += YAutoCompleteTextbox_TextChanged;
            PreviewKeyDown += (sender, args) =>
            {
                switch (args.Key)
                {
                    case Key.Down:
                        IsInnerSlected = true;

                        if (part_ListBox.SelectedIndex == part_ListBox.Items.Count - 1)
                            part_ListBox.SelectedIndex = 0;
                        if (part_ListBox.SelectedIndex < part_ListBox.Items.Count)
                            part_ListBox.SelectedIndex++;

                        part_ListBox.ScrollIntoView(part_ListBox.SelectedItem);

                        break;
                    case Key.Up:
                        IsInnerSlected = true;

                        if (part_ListBox.SelectedIndex > 0)
                            part_ListBox.SelectedIndex--;
                        else
                            part_ListBox.SelectedIndex = part_ListBox.Items.Count - 1;

                        part_ListBox.ScrollIntoView(part_ListBox.SelectedItem);

                        break;
                    case Key.Return:
                        UpdateSelect();
                        break;
                }
            };

            GotFocus += (sender, args) =>
            {
                if (IsShowSeggestsOnFocus)
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
            if (part_Popup != null && !part_Popup.IsOpen)
                part_Popup.IsOpen = true;
        }

        private void HidePopup()
        {
            if (part_Popup != null && part_Popup.IsOpen)
                part_Popup.IsOpen = false;
        }

        private void DelayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            delayTimer.Stop();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new TextChangedCallback(CurrenTextChanged));
        }


        private void YAutoCompleteTextbox_TextChanged(object sender, TextChangedEventArgs e)
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
                delayTimer.Interval = DelayTime;
                delayTimer.Start();
            }
            else
            {
                delayTimer.Interval = 10;
                delayTimer.Start();
            }
        }

        private void AddSuggest(List<object> entries)
        {
            foreach (var entry in entries)
            {
                CurrentSuggests.Add(entry);
            }
        }

        private void CurrenTextChanged()
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
            part_Popup = (GetTemplateChild("PART_Popup") as Popup);
            part_ListBox = (GetTemplateChild("PART_PopupContent") as ListBox);

            if (part_ListBox != null)
            {
                part_ListBox.SelectionChanged += Part_ListBox_SelectionChanged;
            }
        }

        private void Part_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInnerSlected)
            {
                IsInnerSlected = false;
                return;
            }
            UpdateSelect();
        }

        private void UpdateSelect()
        {
            IsInnerChanged = true;
            HidePopup();
            var select = part_ListBox.SelectedItem;
            if (select != null)
                SelectSuggest = select;
            else
            {
                if (!IsSelectFirst) return;

                if (part_ListBox.Items.Count <= 0) return;

                SelectSuggest = part_ListBox.Items[0];

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
