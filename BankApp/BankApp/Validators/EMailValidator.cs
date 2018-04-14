using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Other;
using Xamarin.Forms;

namespace BankApp.Validators
{
    public class EMailValidator : Behavior<Entry>
    {
        private RegexUtilities _regexUtils = new RegexUtilities();

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(PasswordValidator), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty CompareToProperty = BindableProperty.Create("CompareTo", typeof(Entry), typeof(PasswordValidator), null);

        public Entry CompareToEntry
        {
            get => (Entry)GetValue(CompareToProperty);
            set => SetValue(CompareToProperty, value);
        }
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidPropertyKey, value);
        }
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            ((Entry)sender).TextColor = _regexUtils.IsValidEmail(e.NewTextValue) ? Color.Green : Color.Red;

        }
    }


}
