using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ButtonLoader
{
    public partial class ButtonWithActivityIndicator{
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand),
            typeof(ButtonWithActivityIndicator));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
            typeof(object),
            typeof(ButtonWithActivityIndicator));

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
            typeof(ButtonWithActivityIndicator), null,
            propertyChanged: (bindable, oldVal, newVal) => ((ButtonWithActivityIndicator)bindable).OnTextChange((string)newVal));

        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool),
            typeof(ButtonWithActivityIndicator), false,
            propertyChanged: (bindable, oldVal, newVal) => ((ButtonWithActivityIndicator)bindable).OnIsBusy((bool)newVal));

        public static readonly BindableProperty ActivityIndicatorColorProperty = BindableProperty.Create(nameof(ActivityIndicatorColor), typeof(Color),
            typeof(ButtonWithActivityIndicator), null,
             propertyChanged: (bindable, oldVal, newVal) => ((ButtonWithActivityIndicator)bindable).OnColorChanged((Color)newVal));
            

        public event EventHandler Clicked;

        public ButtonWithActivityIndicator()
        {
            InitializeComponent();
            
            InnerButton.Text = Text;
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        public Color ActivityIndicatorColor
        {
            get => (Color)GetValue(ActivityIndicatorColorProperty);
            set => SetValue(ActivityIndicatorColorProperty, value);
        }

        private void OnTextChange(string value)
        {
            InnerButton.Text = value;
        }

        private void OnClicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(this, EventArgs.Empty);

            if (Command == null || !Command.CanExecute(CommandParameter))
                return;

            Command.Execute(CommandParameter);
        }

        private async void OnColorChanged(Color color)
        {
            InnerActivityIndicator.Color = color;

        }

        private async void OnIsBusy(bool value)
        {
            if (value)
            {
                InnerButton.IsEnabled = false;
                InnerButton.Text = "";
                InnerActivityIndicator.IsVisible = true;
                await InnerActivityIndicator.FadeTo(1);
            }
            else
            {
                InnerButton.IsEnabled = true;
                await InnerActivityIndicator.FadeTo(0);
                InnerButton.Text = Text;
                InnerActivityIndicator.IsVisible = false;

            }

            InnerActivityIndicator.IsRunning = value;
        }

       
    }
}
