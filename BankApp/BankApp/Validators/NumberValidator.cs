using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BankApp.Validators
{
    class NumberValidator : Behavior<Entry>
    {
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NumberValidator), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty MaxValProperty = BindableProperty.Create("MaxVal", typeof(int), typeof(NumberValidator), null);
        public static readonly BindableProperty MinValProperty = BindableProperty.Create("MinVal", typeof(int), typeof(NumberValidator), null);
        public static readonly BindableProperty MinLenProperty = BindableProperty.Create("MinLen", typeof(int), typeof(NumberValidator), null);
        public static readonly BindableProperty MaxLenProperty = BindableProperty.Create("MaxLen", typeof(int), typeof(NumberValidator), null);

        public int MaxValue
        {
            get => (int)GetValue(MaxValProperty);
            set => SetValue(MaxValProperty, value);
        }

        public int MinValue
        {
            get => (int)GetValue(MinValProperty);
            set => SetValue(MinValProperty, value);
        }

        public int MaxLength
        {
            get => (int)GetValue(MaxLenProperty);
            set => SetValue(MaxLenProperty, value);
        }

        public int MinLength
        {
            get => (int)GetValue(MinLenProperty);
            set => SetValue(MinLenProperty, value);
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
            var curVal = e.NewTextValue;
            bool allDigits = true;
            bool maxLenOk = true;
            bool minLenOk = true;
            bool maxValOk = true;
            bool minValOk = true;

            for (int i = 0; i < curVal.Length; i++)
                allDigits = allDigits && Char.IsDigit(curVal, i);

            if (MaxLength > 0)
                maxLenOk = curVal.Length <= MaxLength;

            if (MinLength > 0)
                minLenOk = curVal.Length >= MaxLength;

            if (MaxValue > 0)
                maxValOk = Int32.Parse(curVal) <= MaxValue;

            if (MinValue > 0)
                minValOk = Int32.Parse(curVal) >= MinValue;

            IsValid = allDigits && maxLenOk && minLenOk && maxValOk && minValOk;
            ((Entry)sender).TextColor = IsValid ? Color.Green : Color.Red;

        }
    }
}
