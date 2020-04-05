using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ButtonLoader
{
    public partial class ButtonActivityIndicatorRight
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand),
            typeof(ButtonActivityIndicatorRight));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter),
            typeof(object),
            typeof(ButtonActivityIndicatorRight));

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
            typeof(ButtonActivityIndicatorRight), null,
            propertyChanged: (bindable, oldVal, newVal) => ((ButtonActivityIndicatorRight)bindable).OnTextChange((string)newVal));

        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool),
            typeof(ButtonActivityIndicatorRight), false,
            propertyChanged: (bindable, oldVal, newVal) => ((ButtonActivityIndicatorRight)bindable).OnIsBusy((bool)newVal));

        public static readonly BindableProperty ActivityIndicatorColorProperty = BindableProperty.Create(nameof(ActivityIndicatorColor), typeof(Color),
            typeof(ButtonActivityIndicatorRight), null,
             propertyChanged: (bindable, oldVal, newVal) => ((ButtonActivityIndicatorRight)bindable).OnColorChanged((Color)newVal));


        public event EventHandler Clicked;

        public ButtonActivityIndicatorRight()
        {
            InitializeComponent();
           
            InnerButtonRight.Text = Text;
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
            InnerButtonRight.Text = value;
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
            InnerActivityIndicatorRight.Color = color;

        }

        private async void OnIsBusy(bool value)
        {
            if (value)
            {
                InnerButtonRight.IsEnabled = false;
                //InnerButton.Text = "";
                InnerActivityIndicatorRight.IsVisible = true;
                await InnerActivityIndicatorRight.FadeTo(1);
            }
            else
            {
                InnerButtonRight.IsEnabled = true;
                await InnerActivityIndicatorRight.FadeTo(0);
                //InnerButton.Text = Text;
                InnerActivityIndicatorRight.IsVisible = false;

            }

            InnerActivityIndicatorRight.IsRunning = value;
        }

        
    }
}
